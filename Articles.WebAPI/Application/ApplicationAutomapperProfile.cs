using AutoMapper;
using Articles.WebAPI.Application.Commands.CreateArticle;
using Articles.WebAPI.Application.Resources.Arcticles;
using Articles.WebAPI.Domain.Entities;

namespace Articles.WebAPI.Infrastructure
{
    public class ApplicationAutomapperProfile : Profile
    {
        public ApplicationAutomapperProfile()
        {
            CreateMap<ArticleEntity, ArticleResource>().ReverseMap();

            CreateMap<CreateArticleRequest, ArticleEntity>();
        }
    }
}