//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(HPI.DataAccessLayer.DalDbContext),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySetsb509d7d2739e907bb54196d96558cdee02bc96113ca4162be7a99d6b20062e20))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework 6 Power Tools", "0.9.2.0")]
    internal sealed class ViewsForBaseEntitySetsb509d7d2739e907bb54196d96558cdee02bc96113ca4162be7a99d6b20062e20 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "b509d7d2739e907bb54196d96558cdee02bc96113ca4162be7a99d6b20062e20"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "CodeFirstDatabase.Product")
            {
                return GetView0();
            }

            if (extentName == "DalDbContext.Products")
            {
                return GetView1();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Product.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Product
        [CodeFirstDatabaseSchema.Product](T1.Product_ProductCode, T1.Product_Price)
    FROM (
        SELECT 
            T.ProductCode AS Product_ProductCode, 
            T.Price AS Product_Price, 
            True AS _from0
        FROM DalDbContext.Products AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for DalDbContext.Products.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Products
        [HPI.DataAccessLayer.Product](T1.Product_ProductCode, T1.Product_Price)
    FROM (
        SELECT 
            T.ProductCode AS Product_ProductCode, 
            T.Price AS Product_Price, 
            True AS _from0
        FROM CodeFirstDatabase.Product AS T
    ) AS T1");
        }
    }
}
