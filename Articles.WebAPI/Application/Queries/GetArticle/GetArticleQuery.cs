using Articles.WebAPI.Application.Resources.Arcticles;
using MediatR;

namespace Articles.WebAPI.Application.Queries
{
    public class GetArticleQuery : IRequest<ArticleResource>
    {
        public GetArticleQuery(int id, string format)
        {
            ArticleId = id;
            Format = format;
        }

        public int ArticleId { get; set; }

        public string Format { get; set; }
    }
}
