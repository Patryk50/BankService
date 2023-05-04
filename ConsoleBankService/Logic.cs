namespace ConsoleBankService
{
    internal class Logic
    {
        //LISTA Z KONTAMI UŻYTKOWNIKA 
        public static List<User> Users = new()
        {
            new User(firstName: "Jan", lastName: "Kowalski", email: "jan.kowalski@example.com", password: "password123", phoneNumber: "111222333", accountBalance: 5500),
            new User(firstName: "Anna", lastName: "Nowak", email: "anna.nowak@example.com", password: "qwerty123", phoneNumber: "333222111", accountBalance: 1500)
        };

        //AKTUALIZATOR NUMERÓW ID
        internal static void UpdateUserID(List<User> Users)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                Users[i].Id = i + 1;
            }
        }

        //METODY KOMUNIKACYJNE
        internal static void Found(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        //FORMULARZ LOGOWANIA
        internal static void LoginScreen()
        {
            Console.Clear();
            Console.Write("Podaj email: ");
            string email = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();

            User user = Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null) 
            {
                Error("\nPodano błędny e-mail lub hasło");
                Console.WriteLine("Wciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey();
                Interface.Menu();
            }
            else
            {
                Found("\nZalogowano pomyślnie");
                Console.WriteLine("Wciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey();
                Interface.Service(user);
            }
        }

        //FORMULARZ REJESTRACJI
        internal static void AccountRegistration()
        {
            Console.Clear();

            Console.Write("Wprowadź imię: ");
            string firstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 20) 
            {
                Console.Clear();
                Console.WriteLine("Imię nie może być puste ani przekraczać 20 znaków");
                Console.Write("Wprowadź imię ponownie: ");
                firstName = Console.ReadLine();
            }

            Console.Write("Wprowadź nazwisko: ");
            string lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 20)
            {
                Console.Clear();
                Console.WriteLine("Nazwisko nie może być puste ani przekraczać 20 znaków");
                Console.Write("Wprowadź nazwisko ponownie: ");
                lastName = Console.ReadLine();
            }

            Console.Write("Wprowadź email: ");
            string email = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(email) || email.Length > 30)
            {
                Console.Clear();
                Console.WriteLine("Email nie może być pusty ani przekraczać 20 znaków");
                Console.Write("Wprowadź email ponownie: ");
                lastName = Console.ReadLine();
            }

            Console.Write("Wprowadź hasło: ");
            string password = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(password) || password.Length > 20)
            {
                Console.Clear();
                Console.WriteLine("Hasło nie może być puste ani przekraczać 20 znaków");
                Console.Write("Wprowadź hasło ponownie: ");
                lastName = Console.ReadLine();
            }

            Console.Write("Wprowadź numer telefonu: ");
            string phoneNumber = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(password) || password.Length < 3 && password.Length > 7)
            {
                Console.Clear();
                Console.WriteLine("Numer telefonu nie może być pusty, być mniejszy niż 3 znaki ani przekraczać 7 znaków");
                Console.Write("Wprowadź numer ponownie: ");
                lastName = Console.ReadLine();
            }

            int accountBalance = 0;

            User newUser = new(firstName, lastName, email, password, phoneNumber, accountBalance);

            Users.Add(newUser);

            UpdateUserID(Users);
            Interface.Menu();
        }

        //KONTO: METODY
        internal static void SendMoney(User user)
        {
            Console.Clear();

            try
            {
                Console.Write("Podaj imię lub nazwisko adresata: ");
                string userInput = Console.ReadLine();
                User foundUser = Users.Find(user => user.FirstName == userInput || user.LastName == userInput);
                if (foundUser == null)
                {
                    Error("\nNie znaleziono użytkownika o wskazanych danych");
                    Console.ReadKey();
                    Interface.Account(user);
                }
                else
                {
                    Found("\nZnaleziono wskazanego użytkownika");
                    Console.Write("Podaj ilość środków które chcesz przelać na jego konto: ");
                    uint moneyTransferred = uint.Parse(Console.ReadLine());

                    if (moneyTransferred > user.AccountBalance)
                    {
                        Error("Nie posiadasz takich środków");
                        Console.ReadKey();
                        Interface.Account(user);
                    }
                    else
                    {
                        user.AccountBalance -= (int)moneyTransferred;
                        foundUser.AccountBalance += (int)moneyTransferred;

                        Found("\nOperacja zakończona powodzeniem");
                        Console.WriteLine($"Aktualny stan konta wynosi {user.AccountBalance} zł");

                        Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
                        Console.ReadKey();
                        Interface.Account(user);
                    }
                }
            }
            catch (System.FormatException)
            {
                Error("Wprowadzono błędne dane");
                Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
                Console.ReadKey();
                SendMoney(user);
            }
        }

        internal static void DepositMoney(User user)
        {
            Console.Clear();

            try
            {
                Console.Write("Podaj ilość środków jakie chcesz wpłacić na swoje konto: ");
                uint moneyTransferred = uint.Parse(Console.ReadLine());

                if (moneyTransferred > 3000)
                {
                    Error("\nPrzekroczono limit przesyłu");

                    Console.ReadKey();
                    Console.Clear();
                    Interface.Account(user);
                }
                else
                {
                    user.AccountBalance += (int)moneyTransferred;
                    Found("\nŚrodki zostałe wpłacone na konto");

                    Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
                    Console.ReadKey();
                    Interface.Account(user);
                }
            }
            catch (System.FormatException)
            {
                Error("Wprowadzono błędne dane");
                Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
                Console.ReadKey();
                DepositMoney(user);
            }
        }

        internal static void WithdrawMoney(User user)
        { 
            Console.Clear();

            try
            {
                Console.Write("Podaj ilość środków jakie chcesz wypłacić: ");
                uint moneyTransferred = uint.Parse(Console.ReadLine());

                if (moneyTransferred > 3000)
                {
                    Error("\nPrzekroczono limit przesyłu");
                    Console.Clear();
                    Interface.Account(user);
                }
                else
                {
                    user.AccountBalance -= (int)moneyTransferred;
                    Found("\nŚrodki zostały wypłacone z konta");

                    Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
                    Console.ReadKey();
                    Interface.Account(user);
                }
            }
            catch (System.FormatException)
            {
                Error("Wprowadzono błędne dane");
                Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
                Console.ReadKey();
                WithdrawMoney(user);
            }
        }

        internal static void OperationHistory(User user)
        { 
            Console.Clear();
        }

        internal static void RemoveAccount(User user)
        {
            Console.Clear();
            Console.Write("Podaj Email: ");
            string input = Console.ReadLine();
            User userFound = Users.Find(user => user.Email == input);
            input = Console.ReadLine();
            userFound = Users.Find(user => user.Password == input);

            if (userFound == null) 
            {
                Error("Podano błędne dane");
                Interface.Account(user);
            }
            else
            {
                Users.Remove(userFound);
                UpdateUserID(Users);
                Found("Konto usunięto pomyślnie");

                Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
                Console.ReadKey();
                Interface.Menu();
            }
        }

        //USTAWIENIA: METODY
        internal static void ShowAccountInformation(User user)
        {
            Console.Clear();

            Console.WriteLine("Dane na temat twojego konta\n");

            Console.WriteLine($"Imię i nazwisko: {user.FirstName} {user.LastName}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Hasło: {user.Password}");
            Console.WriteLine($"Telefon: {user.PhoneNumber}");

            Console.WriteLine("Wciśnij dowolny klawisz aby wrócić");
            Console.ReadKey();
        }

        internal static void ModifyAccountInformation(User user)
        {
            Console.Clear();
            Console.WriteLine("[1] Zmień imię");
            Console.WriteLine("[2] Zmień nazwisko");
            Console.WriteLine("[3] Zmień email");
            Console.WriteLine("[4] Zmień hasło");
            Console.WriteLine("[5] Zmień numer telefonu");
            Console.WriteLine("[6] Powrót do ustawień");

            Console.Write("\nWybrana opcja: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("\nNowe imię: ");
                    user.FirstName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(user.FirstName))
                    {
                        Error("Pole nie może być puste");
                        Console.ReadKey();
                        ModifyAccountInformation(user);
                    }
                    ModifyAccountInformation(user);
                    break;
                case "2":
                    Console.Write("\nNowe nazwisko: ");
                    user.LastName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(user.LastName))
                    {
                        Error("Pole nie może być puste");
                        Console.ReadKey();
                        ModifyAccountInformation(user);
                    }
                    ModifyAccountInformation(user);
                    break;
                case "3":
                    Console.Write("\nNowy email: ");
                    user.Email = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(user.Email))
                    {
                        Error("Pole nie może być puste");
                        Console.ReadKey();
                        ModifyAccountInformation(user);
                    }
                    ModifyAccountInformation(user);
                    break;
                case "4":
                    Console.Write("\nNowe hasło: ");
                    user.Password = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(user.Password))
                    {
                        Error("Pole nie może być puste");
                        Console.ReadKey();
                        ModifyAccountInformation(user);
                    }
                    ModifyAccountInformation(user);
                    break;
                case "5":
                    Console.Write("\nNowy numer telefonu: ");
                    user.PhoneNumber = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(user.PhoneNumber))
                    {
                        Error("Pole nie może być puste");
                        Console.ReadKey();
                        ModifyAccountInformation(user);
                    }
                    ModifyAccountInformation(user);
                    break;
                case "6":
                    Interface.Settings(user);
                    break;
                default:
                    Error("\nWprowadzono nieprawidłowe dane...");
                    Console.ReadKey();
                    ModifyAccountInformation(user);
                    break;
            }
        }
    }
}
