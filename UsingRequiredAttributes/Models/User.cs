using System.ComponentModel.DataAnnotations;

namespace UsingRequiredAttributes.Models
{
    public class User
    {
        [Required]
        public string? Forename { get; init; }

        public string? Surname { get; init; }

        [Required]
        public int? Age { get; init; }
    }
}
