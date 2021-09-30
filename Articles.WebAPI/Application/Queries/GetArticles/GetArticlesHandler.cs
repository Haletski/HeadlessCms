using AutoMapper;
using AutoMapper.QueryableExtensions;
using Articles.WebAPI.Application.Resources.Arcticles;
using Articles.WebAPI.Domain.Specifications;
using Articles.WebAPI.Infrastructure.DataLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Articles.WebAPI.Application.Queries
{
    public class GetArticlesHandler : IRequestHandler<GetArticlesQuery, List<ArticleResource>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetArticlesHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ArticleResource>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Articles
                .AsNoTracking()
                .SpecificationQuery(new ArticlesSpecification(
                    request.Page, 
                    request.PageSize, 
                    request.OrderBy, 
                    request.OrderType))
                .ProjectTo<ArticleResource>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
