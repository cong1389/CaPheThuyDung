using App.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Language
{
    public class Language : AuditableEntity<int>
    {
        [MaxLength(500)]
        public string Flag
        {
            get;
            set;
        }

        [MaxLength(50)]
        [Required]
        public string LanguageCode
        {
            get;
            set;
        }

        [MaxLength(250)]
        [Required]
        public string LanguageName
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public Language()
        {
        }
    }
}