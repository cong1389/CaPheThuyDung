using App.Core.Data;
using App.Core.Extensions;
using App.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App.Core.Plugins
{
    public static class PluginFileParser
    {
        internal class GroupComparer : Comparer<string>
        {
            public override int Compare(string x, string y)
            {
                return Array.FindIndex(KnownGroups, s => s == x) - Array.FindIndex(KnownGroups, s => s == y);
            }
        }

        internal static readonly string[] KnownGroups = {
            "Admin",
            "Marketing",
            "Payment",
            "Shipping",
            "Tax",
            "Analytics",
            "CMS",
            "Media",
            "SEO",
            "Data",
            "Globalization",
            "Api",
            "Mobile",
            "Social",
            "Security",
            "Developer",
            "Sales",
            "Design",
            "Performance",
            "Misc"
        };
        public static readonly IComparer<string> KnownGroupComparer = new GroupComparer();

        public static readonly string InstalledPluginsFilePath;

        static PluginFileParser()
        {
            InstalledPluginsFilePath = Path.Combine(CommonHelper.MapPath(DataSettings.Current.TenantPath), "InstalledPlugins.txt");
        }

        public static HashSet<string> ParseInstalledPluginsFile(string filePath = null)
        {
            filePath = filePath ?? InstalledPluginsFilePath;

            var lines = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Read and parse the file
            if (!File.Exists(filePath))
                return lines;

            var text = File.ReadAllText(filePath);
            if (text.IsEmpty())
            {
                return lines;
            }

            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    if (str.IsEmpty() || lines.Contains(str, StringComparer.CurrentCultureIgnoreCase))
                        continue;

                    lines.Add(str.Trim());
                }
            }

            return lines;
        }

        public static void SaveInstalledPluginsFile(ICollection<string> pluginSystemNames, string filePath = null)
        {
            filePath = filePath ?? InstalledPluginsFilePath;

            var result = "";
            foreach (var sn in pluginSystemNames)
                result += $"{sn}{Environment.NewLine}";

            File.WriteAllText(filePath, result);
        }

        public static PluginDescriptor ParsePluginDescriptionFile(string filePath)
        {
            var descriptor = new PluginDescriptor();

            var text = File.ReadAllText(filePath);
            if (String.IsNullOrEmpty(text))
                return descriptor;

            string dirName = Path.GetDirectoryName(filePath);
            descriptor.PhysicalPath = dirName;
            descriptor.FolderName = new DirectoryInfo(dirName).Name;

            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(str))
                        continue;
                    settings.Add(str.Trim());
                }
            }

            //Old way of file reading. This leads to unexpected behavior when a user's FTP program transfers these files as ASCII (\r\n becomes \n).
            //var settings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string group = null;

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(':');
                if (separatorIndex == -1)
                {
                    continue;
                }
                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                //group = null;

                switch (key)
                {
                    case "Group":
                        group = value;
                        break;
                    case "FriendlyName":
                        descriptor.FriendlyName = value;
                        break;
                    case "SystemName":
                        descriptor.SystemName = value;
                        break;
                    case "Description":
                        descriptor.Description = value;
                        break;
                    case "Version":
                        descriptor.Version = value.ToVersion();
                        break;
                    case "SupportedVersions": // compat
                    case "MinAppVersion":
                        {
                            // Parse supported min app version
                            descriptor.MinAppVersion = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim())
                                .FirstOrDefault() // since V1.2 take the first only
                                .ToVersion();
                        }
                        break;
                    case "Author":
                        descriptor.Author = value;
                        break;
                    case "Url":
                        descriptor.Url = value;
                        break;
                    case "DisplayOrder":
                        {
                            int.TryParse(value, out var displayOrder);
                            descriptor.DisplayOrder = displayOrder;
                        }
                        break;
                    case "FileName":
                        descriptor.PluginFileName = value;
                        break;
                    case "ResourceRootKey":
                        descriptor.ResourceRootKey = value;
                        break;
                }
            }

            if (IsKnownGroup(group))
            {
                descriptor.Group = group;
            }
            else
            {
                descriptor.Group = "Misc";
            }

            return descriptor;
        }

        private static bool IsKnownGroup(string group)
        {
            if (group.IsEmpty())
                return false;
            return KnownGroups.Contains(group, StringComparer.OrdinalIgnoreCase);
        }
    }
}
