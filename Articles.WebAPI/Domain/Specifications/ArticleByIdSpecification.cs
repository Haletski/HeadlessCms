using Articles.WebAPI.Domain.Entities;

namespace Articles.WebAPI.Domain.Specifications
{
    public class ArticleByIdSpecification : BaseSpecification<ArticleEntity>
    {
        public ArticleByIdSpecification(int articleId)
            : base(x => x.ArticleId == articleId)
        {
        }
    }
}
