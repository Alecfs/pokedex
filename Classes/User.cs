namespace Pokedex___Fredag_aflevering;

public class User
{
    //Properties for checking if the user is logged in and storing an input key
    public bool IsLoggedIn { get; set; }
    public char InputKey { get; set; }

    //Constructor for creating a new user with a login status
    public User(bool isLoggedIn)
    {
        this.IsLoggedIn = isLoggedIn;
    }
}
