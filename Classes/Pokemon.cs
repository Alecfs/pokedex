namespace Pokedex___Fredag_aflevering;

public class Pokemon
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Power { get; set; }

    public Pokemon(string id, string name, string type, int power)
    {
        Id = id;
        Name = name;
        Type = type;
        Power = power;
    }
}
