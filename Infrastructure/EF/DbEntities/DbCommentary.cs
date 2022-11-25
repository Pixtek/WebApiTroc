namespace Infrastructure.EF.DbEntities;

public class DbCommentary
{
    public int Id { get; set; }
    public int Note { get; set; }
    public string Nom { get; set; }
    public string Message { get; set; }
    public int Id_User { get; set; }
}