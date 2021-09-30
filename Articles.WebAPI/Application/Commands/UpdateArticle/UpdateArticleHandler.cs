using Articles.WebAPI.Application.Commands.DeleteArticle;
using Articles.WebAPI.Application.Common.Exceptions;
using Articles.WebAPI.Domain.Specifications;
using Articles.WebAPI.Infrastructure.DataLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Articles.WebAPI.Application.Commands.UpdateArticle
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleRequest, bool>
    {
        private readonly ApplicationDbContext _context;

        public UpdateArticleHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .SpecificationQuery(new ArticleByIdSpecification(request.ArticleId))
                .FirstOrDefaultAsync();

            if (article == null)
            {
                throw new NotFoundException("Article entity is not found", request.ArticleId);
            }

            // can be done in more afficient way to update values that are not null 
            article.Title = request.UpdatedArticle.Title;
            article.Description = request.UpdatedArticle.Description;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
