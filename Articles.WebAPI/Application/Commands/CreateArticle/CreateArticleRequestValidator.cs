using FluentValidation;
using Articles.WebAPI.Application.Commands.CreateArticle;

namespace Articles.WebAPI.Application.Commands.DeleteArticle
{
    public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
    {
        public CreateArticleRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required and can't be empty")
                .NotNull()
                .WithMessage("Description is required and can't be null");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required and can't be empty")
                .NotNull()
                .WithMessage("Title is required and can't be null");
        }
    }
}
