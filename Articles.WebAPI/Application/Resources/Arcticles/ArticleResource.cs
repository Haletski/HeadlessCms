using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Articles.WebAPI.Application.Resources.Arcticles
{
    public class ArticleResource
    {
        /// <summary>
        /// Unique identifier of the article
        /// </summary>
        [XmlElement("articleId")]
        public int ArticleId { get; set; }

        /// <summary>
        /// Title of the article
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Date when article is added
        /// </summary>
        [XmlElement("addedDate")]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Description of the article
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }
    }
}
