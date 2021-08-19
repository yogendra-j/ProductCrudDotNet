using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data
{
    public class DateValidator : ValidationAttribute
    {



        public static string GetErrorMessage() =>
            $"Modified on can not be earlier than created on";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var CurProduct = (Product)validationContext.ObjectInstance;

            if (CurProduct.CreatedOn > CurProduct.ModifiedOn)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
