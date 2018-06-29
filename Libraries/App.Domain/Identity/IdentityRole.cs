using Microsoft.AspNet.Identity;
using System;

namespace App.Domain.Entities.Identity
{
    public class IdentityRole : IRole<Guid>
    {
        public string Description
        {
            get;
            set;
        }

        public Guid Id
        {
            get => JustDecompileGenerated_get_Id();
            set => JustDecompileGenerated_set_Id(value);
        }

        private Guid JustDecompileGenerated_Id_k__BackingField;

        public Guid JustDecompileGenerated_get_Id()
        {
            return JustDecompileGenerated_Id_k__BackingField;
        }

        public void JustDecompileGenerated_set_Id(Guid value)
        {
            JustDecompileGenerated_Id_k__BackingField = value;
        }

        public string Name
        {
            get;
            set;
        }

        public IdentityRole()
        {
            Id = Guid.NewGuid();
        }

        public IdentityRole(string name) : this()
        {
            Name = name;
        }

        public IdentityRole(string name, string description, Guid id)
        {
            Name = name;
            Description = description;
            Id = id;
        }

        public bool Seleted { get; set; }
    }
}