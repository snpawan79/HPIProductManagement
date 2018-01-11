using HPI.BusinessEntities;
using HPI.ValidationRuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPI.BusinessServices
{
    public class ValidationProcessor
    {
        public ValidationModel ValidateProduct(ProductModel prodModel)
        {
            ValidationModel validModel = new ValidationModel { Status = ValidationStatus.OK, ValidationMessage = string.Empty };
            List<Rule> prodRules = GetValidationRules(prodModel);
            if (prodRules.Count > 0)
            {
                RuleEngine engine = new RuleEngine();
                var childPropCheck = engine.CompileRules<ProductModel>(prodRules);
                if (childPropCheck(prodModel))
                {
                    return validModel;
                }
                else
                {
                    validModel.Status = ValidationStatus.ValidationError;
                    validModel.ValidationMessage = GenerateErrorMessage().ToString();
                }
            }
            else
            {
                validModel.Status = ValidationStatus.ValidationError;
                validModel.ValidationMessage = GenerateErrorMessage().ToString();
            }


            return validModel;
        }

        private StringBuilder GenerateErrorMessage()
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            sb.AppendLine("If product code starts with ‘A’ – price should be higher than 0 and lower than 100");
            sb.AppendLine("If product code starts with ‘B’ – price should be equal or higher than 100 and lower than 1000");
            sb.AppendLine("If product code starts with ‘C’ – price should be equal or higher than 1000");
            sb.AppendLine("Product codes starting from any other character will be invalidated.");
            return sb;
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
                }
            };
            if(productModel.ProductCode.Trim().Length > 0)
            {
                string[] ruleIentifier = { productModel.ProductCode.Substring(0, 1).ToUpper()};
                var validationRules = ruleInputs.Where(p => ruleIentifier.Contains(p.RuleIdentifier)).ToList();
                return validationRules;
            }
            else
            {
                return new List<Rule>() {
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.NotEqual.ToString("g"),
                    RuleIdentifier="Blank",
                    MemberName = "ProductCode",
                    TargetValue = string.Empty
                }};
            }
           
        }
    }
}
