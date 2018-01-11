using HPI.Resolver;
using System.ComponentModel.Composition;
using HPI.DataAccessLayer.UnitOfWork;
namespace HPI.DataAccessLayer
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
