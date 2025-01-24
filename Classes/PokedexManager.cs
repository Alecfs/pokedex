namespace Pokedex___Fredag_aflevering;

public class PokedexManager
{
    //Function for showing Pokemon with pagination
    public static bool HandleShowPokemon(string[] lines, bool isSearching = false)
    {
        //If there are less than 10 pokemon just show them all
        if (lines.Length <= 10)
        {
            Console.WriteLine("Resultat(er):");
            Console.WriteLine("-------------");
            foreach (var line in lines)
            {
                //split into id, name, type, power
                string[] values = line.Split(',');
                Console.WriteLine($"ID: {values[0]} Name: {values[1]} Type: {values[2]} Power: {values[3]}");
            }
            //If we are searching show a different message and allow the user to search again
            if (isSearching)
            {
                Console.WriteLine();
                Console.WriteLine("Tryk en tast for at returnere til menuen eller tryk [S] for at søge igen");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Tryk en tast for at returnere til menuen ");
            }
            char inputKey = Console.ReadKey().KeyChar;
            if (inputKey == 's' || inputKey == 'S')
            {
                Console.Clear();
                return true;
            }
            else
            {
                Console.Clear();
                return false;
            }
        }
        //If there are no pokemon show a message and return to menu
        else if (lines.Length == 0)
        {
            Console.WriteLine("Der er ingen Pokemon i systemet.");
            Console.WriteLine("Tryk en tast for at returnere til menuen");
            Console.ReadKey();
            return false;
        }
        //If there are more than 10 pokemon show them with pagination
        else
        {
            //Control Pagination
            int page = 1;
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling((double)lines.Length / pageSize);
            do
            {
                //Show the current page and total pages as well as the list of pokemon
                Console.Clear();
                Console.WriteLine($"Side {page}/{totalPages}");
                for (int i = (page - 1) * pageSize; i < page * pageSize; i++)
                {
                    if (i < lines.Length)
                    {
                        string[] values = lines[i].Split(',');
                        Console.WriteLine($"ID: {values[0]} Name: {values[1]} Type: {values[2]} Power: {values[3]}");
                    }
                }
                //Allow the user to naviate between pages or return to menu
                Console.WriteLine();
                Console.WriteLine("Tryk på <- for at gå til forrige side");
                Console.WriteLine("Tryk på -> for at gå til næste side");
                Console.WriteLine("Tryk på q for at gå tilbage til menuen");
                //If we are searching allow the user to search again
                if (isSearching)
                {
                    Console.WriteLine("Tryk på s for at søge igen");
                }
                ConsoleKey key = Console.ReadKey().Key;
                //Handle the key presses
                if (key == ConsoleKey.RightArrow && page < totalPages)
                {
                    page++;
                }
                else if (key == ConsoleKey.LeftArrow && page > 1)
                {
                    page--;
                }
                else if (key == ConsoleKey.Q)
                {
                    Console.Clear();
                    return false;
                }
                //If the user wishes to keep searching, return true to indicate that we should keep searching
                else if (key == ConsoleKey.S && isSearching)
                {
                    Console.Clear();
                    return true;
                }
            } while (true);
        }
    }

    //Function for searching for a pokemon, uses the handleshowpokemon function to display the results
    public static void HandleSearchForPokemon()
    {
        bool searchAgain;
        char inputKey;
        do
        {
            //Ask the user for a search term
            Console.Write("Indtast navn eller type på Pokemon: ");
            string search = Console.ReadLine() ?? "";
            search = search.ToLower();

            //Read all pokemon from the file and create a new list of the found pokemon based on the search term
            string[] lines = FileManager.ReadPokemon();
            List<string> foundPokemon = new List<string>();
            foreach (var line in lines)
            {
                string[] values = line.Split(',');
                if (values[1].ToLower().Contains(search.ToLower()) || values[2].ToLower().Contains(search.ToLower()))
                {
                    foundPokemon.Add(line);
                }
            }
            //If we found any pokemon, show them and allow the user to search again
            if (foundPokemon.Count > 0)
            {
                searchAgain = HandleShowPokemon(foundPokemon.ToArray(), true);
            }
            //If we didn't find any pokemon, show a message and allow the user to search again
            else
            {
                Console.WriteLine("Ingen Pokemon fundet");
                Console.WriteLine("Tryk en tast for at gå tilbage til menuen, eller tryk [S] for at søge igen");
                inputKey = Console.ReadKey().KeyChar;
                if (inputKey == 's' || inputKey == 'S')
                {
                    searchAgain = true;
                }
                else
                {
                    searchAgain = false;
                }
                Console.Clear();
            }
        } while (searchAgain);

    }

    //Function for adding a pokemon
    public static void HandleAddPokemon()
    {
        do
        {
            //Gather information about the new pokemon
            Console.Clear();
            Console.Write("Indtast navn på Pokemon: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Indtast type på Pokemon: ");
            string type = Console.ReadLine() ?? "";
            Console.Write("Indtast styrkeniveau på Pokemon: ");
            string power = Console.ReadLine() ?? "";
            string[] lines = FileManager.ReadPokemon();

            //If we have any pokemon, get the last id and add 1 to it to get the new id
            if (lines.Length > 0)
            {
                string lastLine = lines.Last();
                string[] values = lastLine.Split(',');
                int newId = int.Parse(values[0]) + 1;
                FileManager.AppendToPokemon(new Pokemon(newId.ToString(), name, type, int.Parse(power)));
            }
            //If we don't have any pokemon, set the id to 1
            else
            {
                FileManager.AppendToPokemon(new Pokemon("1", name, type, int.Parse(power)));
            }
            //Allow the user to return to menu or add another pokemon
            Console.WriteLine("Pokemon tilføjet");
            Console.WriteLine("Tryk en tast for at gå tilbage til menuen, eller tryk [T] for at tilføje en ny Pokemon");
            char inputKey = Console.ReadKey().KeyChar;
            if (inputKey != 't' && inputKey != 'T')
            {
                Console.Clear();
                break;
            }
        } while (true);
    }

    //F Unction for deleting a pokemon
    public static void HandleDeletePokemon()
    {
        do
        {
            //Gather the id of the pokemon to delete
            Console.Clear();
            bool pokemonDeleted = false;
            string[] lines = FileManager.ReadPokemon();
            Console.Write("Indtast ID på Pokemon der skal slettes: ");
            string id = Console.ReadLine() ?? "";

            //Create a new list of pokemon without the pokemon to delete
            List<string> newLines = new List<string>() { "ID,Navn,Type,Styrkeniveau" };
            foreach (var line in lines)
            {
                string[] values = line.Split(',');
                if (values[0] != id)
                {
                    newLines.Add(line);
                }
                else if (values[0] == id)
                {
                    pokemonDeleted = true;
                }
            }

            //Write the new list of pokemon to the file
            FileManager.PokemonWriteAllLines(newLines);

            //Allow the user to return to menu or delete another pokemon
            if (pokemonDeleted)
            {
                Console.WriteLine("Pokemon slettet");
            }
            else
            {
                Console.WriteLine("Ingen Pokemon med det ID fundet");
            }
            Console.WriteLine("Tryk en tast for at gå tilbage til menuen, eller tryk [D] for at slette en ny Pokemon");
            char inputKey = Console.ReadKey().KeyChar;
            if (inputKey != 'd' && inputKey != 'D')
            {
                Console.Clear();
                break;
            }
        } while (true);
    }

    //Function for editing a Pokemon
    public static void HandleEditPokemon()
    {
        do
        {
            //Gather the id of the pokemon to edit
            Console.Clear();
            bool pokemonEdited = false;
            string[] lines = FileManager.ReadPokemon();
            Console.Write("Indtast ID på Pokemon der skal redigeres: ");
            string id = Console.ReadLine() ?? "";

            //Create a new list of pokemon with the edited pokemon
            List<string> newLines = new List<string>() { "ID,Navn,Type,Styrkeniveau" };
            foreach (var line in lines)
            {
                string[] values = line.Split(',');
                if (values[0] == id)
                {
                    Console.Write("Indtast nyt navn på Pokemon: ");
                    string name = Console.ReadLine() ?? "";
                    Console.Write("Indtast ny type på Pokemon: ");
                    string type = Console.ReadLine() ?? "";
                    Console.Write("Indtast nyt styrkeniveau på Pokemon: ");
                    string power = Console.ReadLine() ?? "";
                    newLines.Add($"{id},{name},{type},{power}");
                    pokemonEdited = true;
                }
                else
                {
                    newLines.Add(line);
                }
            }

            //Write the new list of pokemon to the file, if a pokemon was edited show a message
            FileManager.PokemonWriteAllLines(newLines);
            if (pokemonEdited)
            {
                Console.WriteLine("Pokemon redigeret");
            }
            else
            {
                Console.WriteLine("Ingen Pokemon med det ID fundet");
            }

            //Allow the user to edit another pokemon or return to menu
            Console.WriteLine("Tryk en tast for at gå tilbage til menuen, eller tryk [E] for at redigere en ny Pokemon");
            char inputKey = Console.ReadKey().KeyChar;
            if (inputKey != 'e' && inputKey != 'E')
            {
                Console.Clear();
                break;
            }
        } while (true);
    }
}
