namespace Domain;

public class Commentary
{
    public int Id { get; set; }
    public short Note { get; set; }
    public string Nom { get; set; }
    public string Message { get; set; }
    public int IdUser { get; set; }
    
    public bool IsPositive()
    {
        return Note > 4;
    }
    
}