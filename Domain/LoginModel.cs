namespace Domain;

public class LoginModel
{
    public string Email { get; set; }
    public string Mdp { get; set; }
    
    
    public bool IsEmailValid()
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(Email);
            return addr.Address == Email;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    public bool IsValid()
    {
        return IsEmailValid() && !string.IsNullOrWhiteSpace(Mdp);
    }
}

