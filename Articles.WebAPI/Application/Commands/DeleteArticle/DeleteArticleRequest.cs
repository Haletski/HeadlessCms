using MediatR;

namespace Articles.WebAPI.Application.Commands.DeleteArticle
{
    public class DeleteArticleRequest : IRequest<bool>
    {
        public DeleteArticleRequest(int id)
        {
            ArticleId = id;
        }

        public int ArticleId { get; set; }
    }
}