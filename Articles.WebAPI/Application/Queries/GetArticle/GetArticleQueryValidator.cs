using FluentValidation;
using Articles.WebAPI.Domain.Entities;
using System.Linq;
using System.Text.RegularExpressions;

namespace Articles.WebAPI.Application.Queries
{
    public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
    {
        public GetArticleQueryValidator()
        {
            When(x => !string.IsNullOrEmpty(x.Format), () =>
            {
                RuleFor(x => x.Format)
                    .Must(x => Regex.IsMatch(x, "^(json|xml)$", RegexOptions.IgnoreCase))
                    .WithMessage("Format is invalid. Expected values: json,xml");
            });

            RuleFor(x => x.ArticleId)
              .Must(x => x > 0)
              .WithMessage("ArticleId should be greater than 0");
        }
    }
}
