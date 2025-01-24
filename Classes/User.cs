namespace Pokedex___Fredag_aflevering;

public class User
{
    public bool IsLoggedIn { get; set; }
    public char InputKey { get; set; }

    public User(bool isLoggedIn)
    {
        this.IsLoggedIn = isLoggedIn;
    }
}
