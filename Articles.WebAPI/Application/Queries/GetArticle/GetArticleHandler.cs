using AutoMapper;
using AutoMapper.QueryableExtensions;
using Articles.WebAPI.Application.Common.Exceptions;
using Articles.WebAPI.Application.Resources.Arcticles;
using Articles.WebAPI.Domain.Specifications;
using Articles.WebAPI.Infrastructure.DataLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Articles.WebAPI.Application.Queries.GetArticle
{
    public class GetArcticleHandler : IRequestHandler<GetArticleQuery, ArticleResource>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetArcticleHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ArticleResource> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .AsNoTracking()
                .SpecificationQuery(new ArticleByIdSpecification(request.ArticleId))
                .ProjectTo<ArticleResource>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (article == null)
            {
                throw new NotFoundException("Article entity is not found", request.ArticleId);
            }

            return article;
        }
    }
}
