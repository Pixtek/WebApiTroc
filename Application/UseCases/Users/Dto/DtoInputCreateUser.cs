using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Users.Dto;

public class DtoInputCreateUser
{

    [Required] public string Email {get;set;}
    [Required] public string Pseudo {get;set;}
    [Required] public string Localite {get;set;}
    [Required] public string Mdp {get;set;}
    [Required] public bool admin { get; set; }
}