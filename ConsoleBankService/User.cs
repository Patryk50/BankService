namespace ConsoleBankService
{
    internal class User
    {
        public User(string firstName, string lastName, string email, string password, string phoneNumber, int accountBalance)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            AccountBalance = accountBalance;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AccountBalance { get; set; }
    }
}
