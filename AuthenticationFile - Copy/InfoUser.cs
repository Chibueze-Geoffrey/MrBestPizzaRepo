namespace MrBestPizza.AuthenticationFile
{
    public class InfoUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public InfoUser(int userId, string username,
            string firstName, string lastName)
        {
            UserId = userId;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
