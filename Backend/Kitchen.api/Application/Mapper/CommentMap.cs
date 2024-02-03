using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;

namespace Kitchen.api.Application.Mapper
{
    public class CommentMap : Profile
    {
        public CommentMap()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<AddCommentDTO, Comment>();
        }
    }
}