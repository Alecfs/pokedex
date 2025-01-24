namespace Pokedex___Fredag_aflevering;

class Program
{
    //Create instances of user and loginMangager that uses the user
    public static User user = new User(false);
    public static LoginManager loginManager = new LoginManager(user);

    static void Main(string[] args)
    {
        FileManager.CreateDataFilesIfNotExists();
        List<char> validKeysLoggedIn = new List<char> { '1', '2', '3', '4', '5', '6', '7' };
        List<char> validKeysLoggedOut = new List<char> { '1', '2', '3', '4' };

        //Keep the application running until the user decides to exit
        do
        {
            //Display different menu options depending on if the user is logged in or not
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


    //All functions used in the program are defined here
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

    //Function for handling login
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

    //Function for handling logout
    public static void HandleLogOut()
    {
        //Log out the user
        loginManager.HandleLogOut();
    }

    //Function for showing all pokemon
    public static void HandleShowAllPokemon()
    {
        Console.Clear();
        //Read all lines from the pokemon file and use the HandleShowPokemon function to display them
        string[] lines = FileManager.ReadPokemon();
        PokedexManager.HandleShowPokemon(lines);

    }

    //Function for searching for pokemon
    public static void HandleSearchForPokemon()
    {
        Console.Clear();
        PokedexManager.HandleSearchForPokemon();
    }

    //Function for adding a pokemon
    public static void HandleAddPokemon()
    {
        Console.Clear();
        PokedexManager.HandleAddPokemon();
    }

    //Function for editing a pokemon
    public static void HandleEditPokemon()
    {
        PokedexManager.HandleEditPokemon();
    }

    //Function for deleting a pokemon
    public static void HandleDeletePokemon()
    {
        Console.Clear();
        PokedexManager.HandleDeletePokemon();
    }
}