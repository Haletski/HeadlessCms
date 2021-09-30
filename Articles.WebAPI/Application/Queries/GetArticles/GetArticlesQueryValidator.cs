using FluentValidation;
using Articles.WebAPI.Domain.Entities;
using System.Linq;
using System.Text.RegularExpressions;

namespace Articles.WebAPI.Application.Queries
{
    public class DeleteArticleRequestValidator : AbstractValidator<GetArticlesQuery>
    {
        public DeleteArticleRequestValidator()
        {
            When(x => x.Page.HasValue, () =>
            {
                RuleFor(x => x.Page)
                    .Must(x => x.Value > 0)
                    .WithMessage("Page value should be greater than 0");
            });

            When(x => x.PageSize.HasValue, () =>
            {
                RuleFor(x => x.PageSize)
                    .Must(x => x.Value > 0)
                    .WithMessage("Page size should be greater than 0");
            });

            var articleProperties = typeof(ArticleEntity).GetProperties().Select(x => $"{char.ToLowerInvariant(x.Name[0])}{x.Name.Substring(1)}"); 

            When(x => !string.IsNullOrEmpty(x.OrderBy), () =>
            {
                RuleFor(x => x.OrderBy)
                    .Must(x => Regex.IsMatch(x, $"^({string.Join("|", articleProperties)})$", RegexOptions.IgnoreCase))
                    .WithMessage($"Order by is invalid. Expected values: {string.Join(",", articleProperties)}");
            });

            When(x => !string.IsNullOrEmpty(x.OrderType), () =>
            {
                RuleFor(x => x.OrderType)
                    .Must(x => Regex.IsMatch(x, "^(asc|desc)$", RegexOptions.IgnoreCase))
                    .WithMessage("Order type is invalid. Expected values: asc,desc");
            });

            When(x => !string.IsNullOrEmpty(x.Format), () =>
            {
                RuleFor(x => x.Format)
                    .Must(x => Regex.IsMatch(x, "^(json|xml)$", RegexOptions.IgnoreCase))
                    .WithMessage("Format is invalid. Expected values: json,xml");
            });
        }
    }
}
