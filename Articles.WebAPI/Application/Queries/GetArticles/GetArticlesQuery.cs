using Articles.WebAPI.Application.Resources.Arcticles;
using MediatR;
using System.Collections.Generic;

namespace Articles.WebAPI.Application.Queries
{
    public class GetArticlesQuery : IRequest<List<ArticleResource>>
    {
        /// <summary>
        ///  Zero-based index of the first row in the response. Must be a non-negative number
        /// </summary>
        /// <example>1</example>
        public int? Page { get; set; }

        /// <summary>
        ///  Must be a non-negative number. The maximum number of rows to return. To page through results, use the page offset
        /// </summary>
        /// <example>10</example>
        public int? PageSize { get; set; }

        /// <summary>
        /// Specifies the sorting criterea by fieldName of article in camel case
        /// </summary>
        /// <example>title</example>
        public string OrderBy { get; set; }

        /// <summary>
        /// The order type for the field. Accepted values: asc,desc
        /// </summary>
        /// <example>asc</example>
        public string OrderType { get; set; }

        /// <summary>
        /// Format of the response. Accepted values: json,xml
        /// </summary>
        /// <example>json</example>
        public string Format { get; set; }
    }
}
