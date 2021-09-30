using Articles.WebAPI.Domain.Entities;

namespace Articles.WebAPI.Domain.Specifications
{
    public class ArticlesSpecification : BaseSpecification<ArticleEntity>
    {
        public ArticlesSpecification(int? page, int? pageSize, string fieldName, string sortType)
        {
            if(!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(sortType))
            {
                ApplyOrderBy(fieldName, sortType);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                ApplyPaging(page.Value, pageSize.Value);
            }
        }
    }
}
