using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsingRequiredAttributes.Support
{
    public interface IModelValidator
    {
        public List<ValidationResult> ValidateModel(object obj);
    }
}
