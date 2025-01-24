namespace Pokedex___Fredag_aflevering;

public class LoginManager
{
    public bool LoginSuccess { get; set; } = false;
    public bool GoNext { get; set; } = false;
    public string? InputUsername { get; set; }
    public string? InputPassword { get; set; }

    public User User { get; set; }

    public LoginManager(User user)
    {
        this.User = user;
    }

    public void HandleLogin()
    {
        Console.Write("Indtast brugernavn: ");
        this.InputUsername = Console.ReadLine();
        Console.Write("Indtast password: ");
        this.InputPassword = Console.ReadLine();
    }

    public bool CheckLogin()
    {
        string[] lines = FileManager.ReadUsers();
        //Itterate through each line in the file
        foreach (var line in lines)
        {
            //split into username and password
            string[] values = line.Split(',');
            //Check if the input matches the values in the file
            if (values[0] == this.InputUsername && values[1] == this.InputPassword)
            {
                this.LoginSuccess = true;
                this.User.IsLoggedIn = true;
                Console.WriteLine("Login succesfuldt! Vender tilbage til menuen...");
                Thread.Sleep(2000);
                Console.Clear();
                return true;
            }
        }
        if (!this.LoginSuccess)
        {
            Console.WriteLine("Forkert brugernavn eller password. Prøv igen. Eller tryk [Q] for at gå tilbage til menuen.");
            char input = Console.ReadKey().KeyChar;
            if (input == 'Q' || input == 'q')
            {
                this.GoNext = true;
            }
            Console.Clear();
            return false;
        }

        return false;
    }

    public void HandleLogOut()
    {
        this.User.IsLoggedIn = false;
        Console.WriteLine();
        Console.WriteLine("Du er nu logget ud.");
        Console.WriteLine("Tryk en tast for at returnere til menuen");
        Console.ReadKey();
        Console.Clear();
    }
}
