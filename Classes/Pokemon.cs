namespace Pokedex___Fredag_aflevering;

public class Pokemon
{

    //Properties for the pokemon
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Power { get; set; }

    //Constructor for the pokemon
    public Pokemon(string id, string name, string type, int power)
    {
        Id = id;
        Name = name;
        Type = type;
        Power = power;
    }
}
