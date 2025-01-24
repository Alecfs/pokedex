namespace Pokedex___Fredag_aflevering;

public class FileManager
{
    public static string[] ReadUsers()
    {
        string filePath = "data/users.csv";
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            lines = lines.Skip(1).ToArray();
            return lines;
        }
        return [];
    }

    public static string[] ReadPokemon()
    {
        string filePath = "data/pokedata.csv";
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            lines = lines.Skip(1).ToArray();
            return lines;
        }
        return [];
    }

    public static void AppendToPokemon(Pokemon pokemon)
    {
        string filePath = "data/pokedata.csv";
        string newLine = $"{pokemon.Id},{pokemon.Name},{pokemon.Type},{pokemon.Power}";
        File.AppendAllText(filePath, Environment.NewLine + newLine);

    }
}
