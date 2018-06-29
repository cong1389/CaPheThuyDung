/**
                             * @version $Id:
                             * @package Digicom.NET
                             * @author Digicom Dev <dev@dgc.vn>
                             * @copyright Copyright (C) 2011 by Digicom. All rights reserved.
                             * @link http://www.dgc.vn
                            */

using App.Core.Common;

namespace App.Domain.Entities.Language
{
    public class LocaleStringResource : AuditableEntity<int>
    {
        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the resource name
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the resource value
        /// </summary>
        public string ResourceValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this resource was installed by a plugin
        /// </summary>
        public bool IsFromPlugin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this resource was modified by the user
        /// </summary>
        public bool IsTouched { get; set; }

        /// <summary>
        /// Gets or sets the language
        /// </summary>
        public virtual Language Language { get; set; }

    }
}