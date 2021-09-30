using FluentValidation;
using Articles.WebAPI.Application.Commands.DeleteArticle;

namespace Articles.WebAPI.Application.Commands.UpdateArticle
{
    public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
    {
        public UpdateArticleRequestValidator()
        {
            RuleFor(x => x.ArticleId)
                .Must(x => x > 0)
                .WithMessage("ArticleId should be greater than 0");

            RuleFor(x => x.UpdatedArticle)
                .NotNull()
                .WithMessage("CreateArticleRequest is required and can't be null")
                .SetValidator(new CreateArticleRequestValidator());
        }
    }
}
