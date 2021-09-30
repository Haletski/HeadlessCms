using AutoMapper;
using Articles.WebAPI.Domain.Entities;
using Articles.WebAPI.Infrastructure.DataLayer;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Articles.WebAPI.Application.Commands.CreateArticle
{
    public class UpdateArticleHandler : IRequestHandler<CreateArticleRequest, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateArticleHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Articles.AddAsync(_mapper.Map<ArticleEntity>(request));

            await _context.SaveChangesAsync();

            return result.Entity.ArticleId;
        }
    }
}
