namespace Articles.WebAPI.Domain.Entities
{
    public class ArticleEntity : BaseEntity
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}