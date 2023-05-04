namespace ConsoleBankService
{
    internal class Interface
    {
        //MENU POCZĄTKOWE
        internal static void Menu()
        {
            Console.Clear();

            Console.WriteLine("Witaj w serwisie bankowym");
            Console.WriteLine("Dostępne operacje:\n");

            Console.WriteLine("[1] Zaloguj się");
            Console.WriteLine("[2] Zarejestruj się");
            Console.WriteLine("[x] Zakończ");

            Console.Write("\nWybrana opcja: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Logic.LoginScreen();
                    break;
                case "2":
                    Logic.AccountRegistration();
                    break;
                case "x":
                    Console.WriteLine("Do widzenia :)");
                    Environment.Exit(0);
                    break;
                default:
                    Logic.Error("Wprowadzono nieprawidłowe dane...");
                    Console.WriteLine("Wciśnij dowolny klawisz aby kontynuować...");
                    Console.ReadKey();
                    Menu();
                    break;
            }
        }

        //MENU SERWISU
        internal static void Service(User user)
        {
            Console.Clear();
            Console.WriteLine("Witaj w serwisie bankowym");
            Console.WriteLine("Dostępne opcje:\n");

            Console.WriteLine("[1] Konto");
            Console.WriteLine("[2] Ustawienia");
            Console.WriteLine("[x] Wyloguj");

            Console.Write("\nWybrana opcja: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Account(user);
                    break;
                case "2":
                    Settings(user);
                    break;
                case "x":
                    Menu();
                    break;
                default:
                    Logic.Error("Wprowadzono nieprawidłowe dane...");
                    Console.WriteLine("Wciśnij dowolny klawisz aby kontynuować...");
                    Console.ReadKey();
                    Service(user);
                    break;
            }
        }

        //MENU KONTA
        internal static void Account(User user)
        {
            Console.Clear();
            Console.WriteLine("KONTO\n");
            Console.WriteLine($"Twoje saldo wynosi: {user.AccountBalance} zł");
            Console.WriteLine("[1] Prześlij pieniądze");
            Console.WriteLine("[2] Wpłać pieniądze");
            Console.WriteLine("[3] Wypłać pieniądze");
            Console.WriteLine("[4] Usuń konto");
            Console.WriteLine("[5] Powrót do serwisu");

            Console.Write("\nWybrana opcja: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Logic.SendMoney(user);
                    Account(user);
                    break;
                case "2":
                    Logic.DepositMoney(user);
                    Account(user);
                    break;
                case "3":
                    Logic.WithdrawMoney(user);
                    Account(user);
                    break;
                case "4":
                    Logic.RemoveAccount(user);
                    break;
                case "5":
                    Service(user);
                    break;
                default:
                    Logic.Error("Wprowadzono błędne dane...");
                    Console.WriteLine("Wciśnij dowolny klawisz aby kontynuować...");
                    Console.ReadKey();
                    Account(user);
                    break;
            }
        }

        //MENU USTAWIEŃ
        internal static void Settings(User user)
        {
            Console.Clear();
            Console.WriteLine("USTAWIENIA\n");

            Console.WriteLine("[1] Wyświetl dane konta");
            Console.WriteLine("[2] Modyfikuj dane konta");
            Console.WriteLine("[x] Powrót do serwisu");

            Console.Write("\nWybrana opcja: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Logic.ShowAccountInformation(user);
                    Service(user);
                    break;
                case "2":
                    Logic.ModifyAccountInformation(user);
                    Service(user);
                    break;
                case "x":
                    Service(user);
                    break;
                default:
                    Logic.Error("Wprowadzono błędne dane...");
                    Console.WriteLine("Wciśnij dowolny klawisz aby kontynuować...");
                    Console.ReadKey();
                    Service(user);
                    break;
            }
        }
    }
}
