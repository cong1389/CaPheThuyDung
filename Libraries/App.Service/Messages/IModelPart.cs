using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.ComponentModel;

namespace App.Service.Messages
{
    /// <summary>
    /// Used to transfer miscellaneous data to the template engine
    /// which is merged with the generic "Bag" part.
    /// </summary>
    public interface IModelPart : IDictionary<string, object>
    {
    }

    public interface INamedModelPart : IDictionary<string, object>
    {
        string ModelPartName { get; }
    }


    #region Impl

    public class ModelPart : HybridExpando, IModelPart
    {
    }

    public class NamedModelPart : HybridExpando, INamedModelPart
    {
        public NamedModelPart(string modelPartName)
            : base(true)
        {
            ModelPartName = modelPartName;
        }

        public string ModelPartName
        {
            get;
            private set;
        }
    }

    #endregion
}
