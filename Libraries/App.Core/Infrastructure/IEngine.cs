using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Infrastructure.DependencyManagement;

namespace App.Core.Infrastructure
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }
        void Initialize();
    }
}
