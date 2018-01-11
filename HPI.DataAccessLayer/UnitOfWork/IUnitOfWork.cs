using HPI.DataAccessLayer.DataModels;
using HPI.DataAccessLayer.GenericRepository;

namespace HPI.DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
       
        GenericRepository<Product> ProductRepository { get; }
       

        
        /// <summary>
        /// Save method.
        /// </summary>
        void Save();
        
    }
}
