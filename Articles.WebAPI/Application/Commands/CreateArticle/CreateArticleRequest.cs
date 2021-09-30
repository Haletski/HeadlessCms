using MediatR;
using System.Xml.Serialization;

namespace Articles.WebAPI.Application.Commands.CreateArticle
{
    public class CreateArticleRequest : IRequest<int>
    {
        /// <summary>
        /// Title of the article
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the article
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }
    }
}
