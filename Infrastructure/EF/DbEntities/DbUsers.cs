namespace Infrastructure.EF.DbEntities;

public class DbUser
{
    public int Id {get;set;}
    public string Email {get;set;}
    public string Pseudo {get;set;}
    public string Localite {get;set;}
    public string Mdp {get;set;}
    public bool admin { get; set; }
}