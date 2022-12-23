namespace Domain;

public class Users
{
    public int Id {get;set;}
    public string Email {get;set;}
    public string Pseudo {get;set;}
    public string Localite {get;set;}
    public string Mdp {get;set;}
    
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


    public bool IsPseudoValid()
    {
        const int MIN_LENGTH = 3;
        const int MAX_LENGTH = 20;
        if (Pseudo.Length < MIN_LENGTH || Pseudo.Length > MAX_LENGTH)
        {
            return false;
        }

        return true;
    }

    public bool IsPasswordStrong()
    {
        const int MIN_LENGTH = 8;
        const int MAX_LENGTH = 20;
        const int MIN_UPPER = 1;
        const int MIN_LOWER = 1;
        const int MIN_DIGITS = 1;
        const int MIN_SYMBOLS = 1;

        if (Mdp.Length < MIN_LENGTH || Mdp.Length > MAX_LENGTH)
            return false;

        int upper = 0, lower = 0, digits = 0, symbols = 0;

        foreach (char c in Mdp)
        {
            if (char.IsUpper(c)) upper++;
            else if (char.IsLower(c)) lower++;
            else if (char.IsDigit(c)) digits++;
            else symbols++;
        }

        return upper >= MIN_UPPER && lower >= MIN_LOWER && digits >= MIN_DIGITS && symbols >= MIN_SYMBOLS;
    }
    
}