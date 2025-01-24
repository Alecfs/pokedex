namespace Pokedex___Fredag_aflevering;

public class PokedexManager
{

    public static bool HandleShowPokemon(string[] lines, bool isSearching = false)
    {
        if (lines.Length <= 10)
        {
            foreach (var line in lines)
            {
                //split into id, name, type, power
                string[] values = line.Split(',');
                Console.WriteLine($"ID: {values[0]} Name: {values[1]} Type: {values[2]} Power: {values[3]}");
            }

            Console.WriteLine("Tryk en tast for at returnere til menuen");
            Console.ReadKey();
            return false;
        }
        else if (lines.Length == 0)
        {
            Console.WriteLine("Der er ingen Pokemon i systemet.");
            Console.WriteLine("Tryk en tast for at returnere til menuen");
            Console.ReadKey();
            return false;
        }
        else
        {
            //Implement pagination so it only shows 10 at a time
            int page = 1;
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling((double)lines.Length / pageSize);
            do
            {
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
                Console.WriteLine();
                Console.WriteLine("Tryk på <- for at gå til forrige side");
                Console.WriteLine("Tryk på -> for at gå til næste side");
                Console.WriteLine("Tryk på q for at gå tilbage til menuen");
                if (isSearching)
                {
                    Console.WriteLine("Tryk på s for at søge igen");
                }
                ConsoleKey key = Console.ReadKey().Key;
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
                else if (key == ConsoleKey.S && isSearching)
                {
                    Console.Clear();
                    return true;
                }
            } while (true);
        }
    }
    public static void HandleSearchForPokemon()
    {
        //char inputKey;
        bool searchAgain = false;
        char inputKey;

        do
        {
            Console.Write("Indtast navn eller type på Pokemon: ");
            string search = Console.ReadLine() ?? "";
            search = search.ToLower();

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
            if (foundPokemon.Count > 0)
            {
                searchAgain = HandleShowPokemon(foundPokemon.ToArray(), true);
            }
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

    public static void HandleAddPokemon()
    {
        do
        {
            Console.Clear();
            Console.Write("Indtast navn på Pokemon: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Indtast type på Pokemon: ");
            string type = Console.ReadLine() ?? "";
            Console.Write("Indtast styrkeniveau på Pokemon: ");
            string power = Console.ReadLine() ?? "";
            string[] lines = FileManager.ReadPokemon();
            if (lines.Length > 0)
            {
                string lastLine = lines.Last();
                string[] values = lastLine.Split(',');
                int newId = int.Parse(values[0]) + 1;
                FileManager.AppendToPokemon(new Pokemon(newId.ToString(), name, type, int.Parse(power)));
            }
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

    public static void HandleDeletePokemon()
    {
        do
        {
            Console.Clear();
            string[] lines = FileManager.ReadPokemon();
            Console.Write("Indtast ID på Pokemon der skal slettes: ");
            string id = Console.ReadLine() ?? "";
            List<string> newLines = new List<string>() { "ID,Navn,Type,Styrkeniveau" };
            foreach (var line in lines)
            {
                string[] values = line.Split(',');
                if (values[0] != id)
                {
                    newLines.Add(line);
                }
            }
            File.WriteAllLines("data/pokedata.csv", newLines);
            Console.WriteLine("Pokemon slettet");
            Console.WriteLine("Tryk en tast for at gå tilbage til menuen, eller tryk [D] for at slette en ny Pokemon");
            char inputKey = Console.ReadKey().KeyChar;
            if (inputKey != 'd' && inputKey != 'D')
            {
                Console.Clear();
                break;
            }
        } while (true);
    }

    public static void HandleEditPokemon()
    {
        do
        {
            Console.Clear();
            string[] lines = FileManager.ReadPokemon();
            Console.Write("Indtast ID på Pokemon der skal redigeres: ");
            string id = Console.ReadLine() ?? "";
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
                }
                else
                {
                    newLines.Add(line);
                }
            }
            File.WriteAllLines("data/pokedata.csv", newLines);
            Console.WriteLine("Pokemon redigeret");
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
