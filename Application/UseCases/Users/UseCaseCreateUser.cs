using Application.UseCases.Users.Dto;
using Application.UseCases.Utils;
using Infrastructure.EF.User;

namespace Application.UseCases.Users;

public class UseCaseCreateUser: IUseCaseWriter<DtoOutputUser, DtoInputCreateUser>
{
    private readonly IUsers _users;

    public UseCaseCreateUser(IUsers users)
    {
        _users = users;
    }

    public DtoOutputUser Execute(DtoInputCreateUser input)
    {
        var dbUser = _users.Create(input.Email, input.Pseudo, input.Localite, input.Mdp);
        return Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
    }
}