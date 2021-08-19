using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data
{

    public class UniqueValidator : ValidationAttribute
    {
        public string PropName { get; }

        public string GetErrorMessage() =>
            $"{PropName} already exists";
        public ProductDbContext ProductDbContext { get; set; }
        public UniqueValidator(string propName)
        {
            PropName = propName;
        }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {

            if (ProductDbContext.Products.Where<Product>(r => LambdaFunc(r, (int)value)) != null)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
        private bool LambdaFunc(Product P, int value)
        {
            if (PropName == "SerialNo")
            {
                return P.SerialNumber == value;
            } else if (PropName == "BatchNo")
            {
                return P.BatchNumber == value;

            }
            else if (PropName == "ShippingNo")
            {
                return P.SerialNumber == value;

            }
            return false;
        }
    }
}



