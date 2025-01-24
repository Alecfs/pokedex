namespace Pokedex___Fredag_aflevering;

class Program
{
    public static User user = new User(false);
    public static LoginManager loginManager = new LoginManager(user);

    static void Main(string[] args)
    {
        List<char> validKeysLoggedIn = new List<char> { '1', '2', '3', '4', '5', '6', '7' };
        List<char> validKeysLoggedOut = new List<char> { '1', '2', '3', '4' };

        do
        {
            if (!user.IsLoggedIn)
            {
                do
                {
                    DisplayMenu();
                    user.InputKey = Console.ReadKey().KeyChar;
                    switch (user.InputKey)
                    {
                        case '1':
                            HandleLogin();
                            break;
                        case '2':
                            HandleShowAllPokemon();
                            break;
                        case '3':
                            HandleSearchForPokemon();
                            break;
                        case '4':
                            break;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("Ugyldigt valg");
                            Console.WriteLine("Tryk en tast for at vende tilbage til menuen");
                            Console.ReadKey();
                            Console.WriteLine();
                            Console.Clear();
                            break;
                    }
                } while (!validKeysLoggedOut.Contains(user.InputKey));
            }
            else
            {
                do
                {
                    DisplayMenu();
                    user.InputKey = Console.ReadKey().KeyChar;
                    switch (user.InputKey)
                    {
                        case '1':
                            HandleLogOut();
                            break;
                        case '2':
                            HandleAddPokemon();
                            break;
                        case '3':
                            HandleEditPokemon();
                            break;
                        case '4':
                            HandleDeletePokemon();
                            break;
                        case '5':
                            HandleShowAllPokemon();
                            break;
                        case '6':
                            HandleSearchForPokemon();
                            break;
                        case '7':
                            break;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("Ugyldigt valg");
                            Console.WriteLine("Tryk en tast for at vende tilbage til menuen");
                            Console.ReadKey();
                            Console.WriteLine();
                            Console.Clear();
                            break;
                    }
                } while (!validKeysLoggedIn.Contains(user.InputKey));
            }
        } while ((user.IsLoggedIn && user.InputKey != '7') || (!user.IsLoggedIn && user.InputKey != '4'));
    }

    public static void DisplayMenu()
    {
        if (!user.IsLoggedIn)
        {
            Console.WriteLine("1. Log ind");
            Console.WriteLine("2. Se alle Pokemon");
            Console.WriteLine("3. Søg efter Pokemon");
            Console.WriteLine("4. Afslut");
        }
        else
        {
            Console.WriteLine("1. Log ud");
            Console.WriteLine("2. Tilføj Pokemon");
            Console.WriteLine("3. Rediger Pokemon");
            Console.WriteLine("4. Slet Pokemon");
            Console.WriteLine("5. Se alle Pokemon");
            Console.WriteLine("6. Søg efter Pokemon");
            Console.WriteLine("7. Afslut");
        }
    }

    public static void HandleLogin()
    {
        Console.Clear();
        do
        {
            //Gather user input login
            loginManager.HandleLogin();
            //Check if login is successful
            loginManager.CheckLogin();

        } while (!loginManager.LoginSuccess && !loginManager.GoNext);

    }

    public static void HandleLogOut()
    {
        loginManager.HandleLogOut();
    }

    public static void HandleShowAllPokemon()
    {
        Console.Clear();
        string[] lines = FileManager.ReadPokemon();
        PokedexManager.HandleShowPokemon(lines);

    }

    public static void HandleSearchForPokemon()
    {
        Console.Clear();
        PokedexManager.HandleSearchForPokemon();
    }

    public static void HandleAddPokemon()
    {
        Console.Clear();
        PokedexManager.HandleAddPokemon();
    }

    public static void HandleEditPokemon()
    {
        PokedexManager.HandleEditPokemon();
    }

    public static void HandleDeletePokemon()
    {
        Console.Clear();
        PokedexManager.HandleDeletePokemon();
    }
}