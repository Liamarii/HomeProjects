using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsingRequiredAttributes.Support
{
    public class ModelValidator : IModelValidator
    {
        public List<ValidationResult> ValidateModel(object obj)
        {
            ValidationContext context = new(obj, null, null);
            List<ValidationResult> results = new();
            Validator.TryValidateObject(obj, context, results);
            return results;
        }
    }
}
