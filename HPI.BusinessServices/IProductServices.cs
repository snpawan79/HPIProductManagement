using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPI.BusinessEntities;
namespace HPI.BusinessServices
{
    public interface IProductServices
    {
        ProductModel GetProductByCode(string productCode);
        IEnumerable<ProductModel> GetAllProducts();
        bool CreateProduct(ProductModel productEntity);
    }
}
