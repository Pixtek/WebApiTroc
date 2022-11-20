using Application.UseCases.Users.Dto;
using AutoMapper;
using Infrastructure.EF.User;

namespace Application.UseCases.Users;

public class UseCaseFetchById
{
    private readonly IUsers _userRepository;


    public UseCaseFetchById(IUsers userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoOutputUser Execute(int id)
    {
        var dbUser = _userRepository.FetchById(id);
        return Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
    }
    
}