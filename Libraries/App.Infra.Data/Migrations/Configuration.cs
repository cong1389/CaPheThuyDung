using App.Core.Extensions;
using App.Domain.Common;
using App.Domain.Entities.Account;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using AppContext = App.Infra.Data.Context.AppContext;

namespace App.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppContext context)
        {
            var constantsValues = typeof(ApplicationRoles).GetConstantsValues<string>();
            if (constantsValues != null && constantsValues.Any())
            {
                foreach (var constantsValue in constantsValues)
                {
                    if (context.Roles.FirstOrDefault(x => x.Name == constantsValue) == null)
                    {
                        context.Roles.AddOrUpdate(new Role
                        {
                            Id = Guid.NewGuid(),
                            Name = constantsValue
                        });
                    }
                }
            }
        }
    }
}