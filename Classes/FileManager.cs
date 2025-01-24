namespace Pokedex___Fredag_aflevering;

public class FileManager
{

    //Path to the files
    private static string userFilePath = "data/users.csv";
    private static string pokemonFilePath = "data/pokedata.csv";
    //function for reading and returning all users, skipping the first line which is just columns
    public static string[] ReadUsers()
    {
        if (File.Exists(userFilePath))
        {
            string[] lines = File.ReadAllLines(userFilePath);
            lines = lines.Skip(1).ToArray();
            return lines;
        }
        return [];
    }

    //function for reading and returning all pokemon, skipping the first line which is just columns
    public static string[] ReadPokemon()
    {
        if (File.Exists(pokemonFilePath))
        {
            string[] lines = File.ReadAllLines(pokemonFilePath);
            lines = lines.Skip(1).ToArray();
            return lines;
        }
        return [];
    }

    //function for appending a new pokemon to the file
    public static void AppendToPokemon(Pokemon pokemon)
    {
        string newLine = $"{pokemon.Id},{pokemon.Name},{pokemon.Type},{pokemon.Power}";
        File.AppendAllText(pokemonFilePath, Environment.NewLine + newLine);

    }

    //Function for overwriting the pokemon file with a new list of lines
    public static void PokemonWriteAllLines(List<string> newLines)
    {
        File.WriteAllLines(pokemonFilePath, newLines);
    }

    //Function for creating the data files if they do not exist
    public static void CreateDataFilesIfNotExists()
    {
        //Create user file with default user if it does not exist
        if (!File.Exists(userFilePath))
        {
            string[] defaultUser = { "username,password", $"admin,{LoginManager.GenerateHash("admin")}" };
            File.WriteAllLines(userFilePath, defaultUser);
        }

        //Create pokemon file only with columns if it does not exist
        if (!File.Exists(pokemonFilePath))
        {
            string[] defaulColumns = { "ID,Navn,Type,Styrkeniveau" };
            File.WriteAllLines(pokemonFilePath, defaulColumns);
        }
    }
}
