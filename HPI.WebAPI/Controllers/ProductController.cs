using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HPI.BusinessEntities;
using HPI.BusinessServices;
using HPI.ValidationRuleEngine;
namespace HPI.WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductServices _productServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        #endregion
        public HttpResponseMessage Get()
        {
            var products = _productServices.GetAllProducts();
            if (products != null)
            {
                var productEntities = products as List<ProductModel> ?? products.ToList();
                if (productEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        // GET api/product/5
        public HttpResponseMessage Get(string productCode)
        {
            var product = _productServices.GetProductByCode(productCode);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found for this Code");
        }

        // POST api/product
        public HttpResponseMessage Post([FromBody] ProductModel productModel)
        {
            ValidationProcessor validationProc = new ValidationProcessor();
            ValidationModel validModel = validationProc.ValidateProduct(productModel);
            if(validModel.Status == ValidationStatus.OK)
            {
                bool status = _productServices.CreateProduct(productModel);
                if (status)
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error occurred in creating a product");
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, validModel.ValidationMessage);
        }

        private List<Rule> GetValidationRules(ProductModel productModel)
        {
            List<Rule> ruleInputs = new List<Rule>()
            {
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.AndAlso.ToString("g"),
                    RuleIdentifier="A",
                    Rules = new List<Rule>()
                    {
                        new Rule(){ MemberName = "ProductCode",Inputs = new List<object> { "A" }, Operator = "StartsWith"},
                        new Rule(){
                            Operator = "And",
                            Rules = new List<Rule>(){
                                new Rule(){ MemberName = "Price", TargetValue = "0", Operator = System.Linq.Expressions.ExpressionType.GreaterThan.ToString("g")},
                                new Rule(){ MemberName = "Price", TargetValue = "100",Operator= System.Linq.Expressions.ExpressionType.LessThan.ToString("g")}
                            }
                        }
                    }
                },
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.AndAlso.ToString("g"),
                    RuleIdentifier="B",
                    Rules = new List<Rule>()
                    {
                        new Rule(){ MemberName = "ProductCode",Inputs = new List<object> { "B" }, Operator = "StartsWith"},
                        new Rule(){
                            Operator = "And",
                            Rules = new List<Rule>(){
                                new Rule(){ MemberName = "Price", TargetValue = "100", Operator = System.Linq.Expressions.ExpressionType.GreaterThanOrEqual.ToString("g")},
                                new Rule(){ MemberName = "Price", TargetValue = "1000",Operator= System.Linq.Expressions.ExpressionType.LessThan.ToString("g")}
                            }
                        }
                    }
                },
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.AndAlso.ToString("g"),
                    RuleIdentifier="C",
                    Rules = new List<Rule>()
                    {
                        new Rule(){ MemberName = "ProductCode",Inputs = new List<object> { "C" }, Operator = "StartsWith"},
                        new Rule(){
                            Operator = "And",
                            Rules = new List<Rule>(){
                                new Rule(){ MemberName = "Price", TargetValue = "1000", Operator = System.Linq.Expressions.ExpressionType.GreaterThanOrEqual.ToString("g")}
                            }
                        }
                    }
                },
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.NotEqual.ToString("g"),
                    RuleIdentifier="Blank",
                    MemberName = "ProductCode",
                    TargetValue = string.Empty
                }
            };

            string[] ruleIentifier = { productModel.ProductCode.Substring(0, 1).ToUpper(), "Blank" };
            var validationRules = ruleInputs.Where(p => ruleIentifier.Contains(p.RuleIdentifier)).ToList();
            return validationRules;
        }
    }
}