using System.ComponentModel.DataAnnotations;
namespace HPI.DataAccessLayer.DataModels
{
    public class Product
    {
        [Key]
        public string ProductCode { get; set; }
        public int Price { get; set; }
    }
}
