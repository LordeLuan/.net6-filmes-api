﻿using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfile : Profile
{
    // Metodo de mapeamento/conversao de um dto para filme
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
    }
}
