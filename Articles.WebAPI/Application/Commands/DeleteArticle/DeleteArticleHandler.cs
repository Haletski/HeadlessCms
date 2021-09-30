using Articles.WebAPI.Application.Common.Exceptions;
using Articles.WebAPI.Domain.Specifications;
using Articles.WebAPI.Infrastructure.DataLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Articles.WebAPI.Application.Commands.DeleteArticle
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleRequest, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteArticleHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(DeleteArticleRequest request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .SpecificationQuery(new ArticleByIdSpecification(request.ArticleId))
                .FirstOrDefaultAsync();

            if (article == null)
            {
                throw new NotFoundException("Article entity is not found", request.ArticleId);
            }

            _context.Articles.Remove(article);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
