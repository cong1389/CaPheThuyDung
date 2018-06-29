using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Extensions;
using App.Core.Infrastructure;
using App.Core.Utilities;
using NuGet;

namespace App.Core.Packaging
{
    public sealed class AppUpdater : DisposableObject
    {
        private const string UpdatePackagePath = "~/App_Data/Update";

        #region Package update

        [SuppressMessage("ReSharper", "RedundantAssignment")]
        public bool InstallablePackageExists()
        {
            string packagePath = null;
            var package = FindPackage(false, out packagePath);

            if (package == null)
                return false;

            if (!ValidatePackage(package))
                return false;

            if (!CheckEnvironment())
                return false;

            return true;
        }

        private IPackage FindPackage(bool createLogger, out string path)
        {
            path = null;
            var dir = CommonHelper.MapPath(UpdatePackagePath, false);

            if (!Directory.Exists(dir))
                return null;

            var files = Directory.GetFiles(dir, "SmartStore.*.nupkg", SearchOption.TopDirectoryOnly);

            // TODO: allow more than one package in folder and return newest
            if (files.Length == 0 || files.Length > 1)
                return null;

            try
            {
                path = files[0];
                IPackage package = new ZipPackage(files[0]);
                if (createLogger)
                {
                    //_logger = CreateLogger(package);
                    //_logger.Info("Found update package '{0}'".FormatInvariant(package.GetFullName()));
                }
                return package;
            }
            catch { }

            return null;
        }

        private bool ValidatePackage(IPackage package)
        {
            if (package.Id != "SmartStore")
                return false;

            var currentVersion = new SemanticVersion(SmartStoreVersion.Version);
            return package.Version > currentVersion;
        }

        private bool CheckEnvironment()
        {
            // TODO: Check it :-)
            return true;
        }

        private void Backup()
        {
            var source = new DirectoryInfo(CommonHelper.MapPath("~/"));

            var tempPath = CommonHelper.MapPath("~/App_Data/_Backup/App/SmartStore");
            string localTempPath = null;
            for (int i = 0; i < 50; i++)
            {
                localTempPath = tempPath + (i == 0 ? "" : "." + i.ToString());
                if (!Directory.Exists(localTempPath))
                {
                    Directory.CreateDirectory(localTempPath);
                    break;
                }
                localTempPath = null;
            }

            if (localTempPath == null)
            {
                //throw exception;
            }

            var backupFolder = new DirectoryInfo(localTempPath);
            var folderUpdater = new FolderUpdater(null);
            folderUpdater.Backup(source, backupFolder, "App_Data", "Media");
        }

        #endregion


        #region Migrations

        internal void ExecuteMigrations()
        {
            //TryMigrateDefaultTenant();

            //if (!DataSettings.DatabaseIsInstalled())
            //    return;

            //var currentVersion = SmartStoreVersion.Version;
            //var prevVersion = DataSettings.Current.AppVersion ?? new Version(1, 0);

            //if (prevVersion >= currentVersion)
            //    return;

            //if (prevVersion < new Version(2, 1))
            //{
            //    // we introduced app migrations in V2.1. So any version prior 2.1
            //    // has to perform the initial migration
            //    MigrateInitial();
            //}

            //DataSettings.Current.AppVersion = currentVersion;
            //DataSettings.Current.Save();
        }

        private bool TryMigrateDefaultTenant()
        {
            // We introduced basic multi-tenancy in V3 [...]

            if (!IsPreTenancyVersion())
            {
                return false;
            }

            var tenantDir = Directory.CreateDirectory(CommonHelper.MapPath("~/App_Data/Tenants/Default"));
            var tenantTempDir = tenantDir.CreateSubdirectory("_temp");

            var appDataDir = CommonHelper.MapPath("~/App_Data");

            // Move Settings.txt
            File.Move(Path.Combine(appDataDir, "Settings.txt"), Path.Combine(tenantDir.FullName, "Settings.txt"));

            // Move InstalledPlugins.txt
            File.Move(Path.Combine(appDataDir, "InstalledPlugins.txt"), Path.Combine(tenantDir.FullName, "InstalledPlugins.txt"));

            // Move SmartStore.db.sdf
            var path = Path.Combine(appDataDir, "SmartStore.db.sdf");
            if (File.Exists(path))
            {
                File.Move(path, Path.Combine(tenantDir.FullName, "SmartStore.db.sdf"));
            }

            Func<string, string, bool> moveTenantFolder = (sourceFolder, targetFolder) =>
            {
                var sourcePath = Path.Combine(appDataDir, sourceFolder);

                if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(tenantDir.FullName, targetFolder ?? sourceFolder));
                    return true;
                }

                return false;
            };

            // Move tenant specific Folders
            moveTenantFolder("ImportProfiles", null);
            moveTenantFolder("ExportProfiles", null);
            moveTenantFolder("Indexing", null);
            moveTenantFolder("Lucene", null);
            moveTenantFolder("_temp\\BizBackups", null);
            moveTenantFolder("_temp\\ShopConnector", null);

            // Move all media files and folders to new subfolder "Default"
            var mediaInfos = (new DirectoryInfo(CommonHelper.MapPath("~/Media"))).EnumerateFileSystemInfos().Where(x => !x.Name.IsCaseInsensitiveEqual("Default"));
            var mediaFiles = mediaInfos.OfType<FileInfo>();
            var mediaDirs = mediaInfos.OfType<DirectoryInfo>().ToArray();
            var tenantMediaDir = new DirectoryInfo(CommonHelper.MapPath("~/Media/Default"));
            if (!tenantMediaDir.Exists)
            {
                tenantMediaDir.Create();
            }

            foreach (var dir in mediaDirs)
            {
                dir.MoveTo(Path.Combine(tenantMediaDir.FullName, dir.Name));
            }

            foreach (var file in mediaFiles)
            {
                file.MoveTo(Path.Combine(tenantMediaDir.FullName, file.Name));
            }

            return true;
        }

        private bool IsPreTenancyVersion()
        {
            var appDataDir = CommonHelper.MapPath("~/App_Data");

            return File.Exists(Path.Combine(appDataDir, "Settings.txt"))
                && File.Exists(Path.Combine(appDataDir, "InstalledPlugins.txt"))
                && !Directory.Exists(Path.Combine(appDataDir, "Tenants\\Default"));
        }

        #endregion


        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                //if (_logger != null)
                //{
                //    _logger.Dispose();
                //    _logger = null;
                //}
            }
        }
    }
}
