using FluentValidation;

namespace Articles.WebAPI.Application.Commands.DeleteArticle
{
    public class UpdateArticleRequestValidator : AbstractValidator<DeleteArticleRequest>
    {
        public UpdateArticleRequestValidator()
        {
            RuleFor(x => x.ArticleId)
                .Must(x => x > 0)
                .WithMessage("ArticleId should be greater than 0");
        }
    }
}
