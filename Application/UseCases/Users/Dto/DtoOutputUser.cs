namespace Application.UseCases.Users.Dto;

public class DtoOutputUser
{
    public int Id {get;set;}
    public string Email {get;set;}
    public string Pseudo {get;set;}
    public string Localite {get;set;}
    public bool admin { get; set; }
}