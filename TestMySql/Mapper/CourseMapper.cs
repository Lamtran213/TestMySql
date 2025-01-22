using AutoMapper;
using TestMySql.DTO.request;
using TestMySql.Entities;

namespace TestMySql.Mapper;

public class CourseMapper : Profile
{
    public CourseMapper()
    {
        CreateMap<CourseCreationRequest, Course>().ReverseMap();
        CreateMap<CourseUpdateRequest, Course>().ReverseMap();
    }
}