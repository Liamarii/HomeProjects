namespace UsingMocks.Models
{
    public class User
    {
        public string? Forename { get; }

        public string? Surname { get; }

        public User(string? forename = null, string? surname = null)
        {
            Forename = forename;
            Surname = surname;
        }
    }
}
