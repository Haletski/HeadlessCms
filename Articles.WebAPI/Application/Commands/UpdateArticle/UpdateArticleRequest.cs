using Articles.WebAPI.Application.Commands.CreateArticle;
using MediatR;

namespace Articles.WebAPI.Application.Commands.UpdateArticle
{
    public class UpdateArticleRequest : IRequest<bool>
    {
        public UpdateArticleRequest(CreateArticleRequest updatedArticle, int articleId)
        {
            UpdatedArticle = updatedArticle;
            ArticleId = articleId;
        }

        public int ArticleId { get; set; }

        public CreateArticleRequest UpdatedArticle { get; set; }
    }
}
