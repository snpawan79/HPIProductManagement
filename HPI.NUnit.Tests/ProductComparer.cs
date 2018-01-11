using HPI.DataAccessLayer.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPI.NUnit.Tests
{
    public class ProductComparer : IComparer, IComparer<Product>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Product;
            var rhs = actual as Product;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Product expected, Product actual)
        {
            int temp;
            return (temp = expected.ProductCode.CompareTo(actual.ProductCode)) != 0 ? temp : expected.Price.CompareTo(actual.Price);
        }
    }
}
