using System.ComponentModel.Composition;
using HPI.Resolver;

namespace HPI.BusinessServices
{

    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IProductServices, ProductServices>();

        }
    }
}
