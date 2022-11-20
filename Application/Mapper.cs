
using Application.UseCases.Users.Dto;
using AutoMapper;
using Domain;
using Infrastructure.EF.DbEntities;


namespace Application;

public static class Mapper
{
    private static AutoMapper.Mapper _instance;

    public static AutoMapper.Mapper GetInstance()
    {
        return _instance ??= CreateMapper();
    }

    private static AutoMapper.Mapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            // Source, Destination
            // User
            cfg.CreateMap<Users, DtoOutputUser>();
            cfg.CreateMap<DbUser, DtoOutputUser>();
            cfg.CreateMap<DbUser, Users>();
            // Game

        });
        return new AutoMapper.Mapper(config);
    }
}