namespace Infrastructure.EF.DbEntities;

public class DbHisotricTransaction
{
    public int Id { get; set; }
    public DateTime Dates { get; set; }
    public int Id_User1 { get; set; }
    public int Id_User2 { get; set; }
    public string Article1 { get; set; }
    public string Article2 { get; set; }
    public bool echange { get; set; }
}