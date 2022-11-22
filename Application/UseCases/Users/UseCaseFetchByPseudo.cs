using Infrastructure.EF.User;

namespace Application.UseCases.Users.Dto;

public class UseCaseFetchByPseudo
{
    private readonly IUsers _userRepository;


    public UseCaseFetchByPseudo(IUsers userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoOutputUser Execute(string pseudo)
    {
        var dbUser = _userRepository.FetchByPseudo(pseudo);
        return Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
    }
}