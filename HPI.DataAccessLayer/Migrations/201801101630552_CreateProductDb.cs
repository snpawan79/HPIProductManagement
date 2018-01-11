namespace HPI.DataAccessLayer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductCode = c.String(nullable: false, maxLength: 128, storeType: "nvarchar",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "Index_PCode",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Index_PCode, IsUnique: True }")
                                },
                            }),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "ProductCode",
                        new Dictionary<string, object>
                        {
                            { "Index_PCode", "IndexAnnotation: { Name: Index_PCode, IsUnique: True }" },
                        }
                    },
                });
        }
    }
}
