using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using HPI.DataAccessLayer.DataModels;
namespace HPI.DataAccessLayer
{
    public class ProductMap: EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // ....
           
            Property(x => x.ProductCode).IsRequired().HasColumnAnnotation("Index_PCode", new IndexAnnotation(new[] { new IndexAttribute("Index_PCode") { IsUnique = true } }));
        }
    
    }
}
