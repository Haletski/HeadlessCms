using Articles.WebAPI.Application.Attributes;
using Articles.WebAPI.Application.Commands.CreateArticle;
using Articles.WebAPI.Application.Commands.DeleteArticle;
using Articles.WebAPI.Application.Commands.UpdateArticle;
using Articles.WebAPI.Application.Queries;
using Articles.WebAPI.Application.Resources;
using Articles.WebAPI.Application.Resources.Arcticles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Articles.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/articles")]
    [FormatFilter]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets all articles
        /// </summary>
        /// <response code="200">Returns all articles</response>
        /// <response code="400">Returns bad request if request doesn't pass validation</response>
        /// <response code="500">Returns internal server error with error message when unexpected error occurred</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        [ProducesResponseType(typeof(List<ArticleResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetArticlesQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Gets single article
        /// </summary>
        /// <param name="id" example="1">Unique identifier of the article</param>
        /// <param name="format" example="json">Format of the response. Accepted values: json,xml</param>
        /// <response code="200">Returns article</response>
        /// <response code="400">Returns bad request status code if request doesn't pass validation</response>
        /// <response code="404">Returns not found status code if article is not found</response>
        /// <response code="500">Returns internal server error if any unexpected error occured</response>
        [HttpGet("{id:int}", Name = "Get")]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        [ProducesResponseType(typeof(ArticleResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id, [FromQuery] string format)
        {
            var response = await _mediator.Send(new GetArticleQuery(id, format));

            return Ok(response);
        }

        /// <summary>
        /// Updates article
        /// </summary>
        /// <remarks>
        /// PUT /api/v1/articles
        /// 
        ///     {
        ///        "title": "Title of the article",
        ///        "description": "Description of the article"
        ///     }
        ///     
        /// </remarks>
        /// <response code="204">Returns no content result</response>
        /// <response code="400">Returns bad request status code if request doesn't pass validation</response>
        /// <response code="401">Returns unauthorized status code if client is not authenticated</response>
        /// <response code="404">Returns not found status code if article is not found</response>
        /// <response code="500">Returns internal server error if any unexpected error occured</response>
        [ApiKey]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] CreateArticleRequest article, int id)
        {
            await _mediator.Send(new UpdateArticleRequest(article, id));

            return NoContent();
        }

        /// <summary>
        /// Deletes article
        /// </summary>
        /// <response code="200">Returns ok status code if article is deleted</response>
        /// <response code="400">Returns bad request status code if request doesn't pass validation</response>
        /// <response code="401">Returns unauthorized status code if client is not authenticated</response>
        /// <response code="404">Returns not found status code if article is not found</response>
        /// <response code="500">Returns internal server error if any unexpected error occured</response>
        [ApiKey]
        [HttpDelete("{id:int}")]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteArticleRequest(id));

            return Ok();
        }

        /// <summary>
        /// Creates new article
        /// </summary>
        /// <remarks>
        /// POST /api/v1/articles
        /// 
        ///     {
        ///        "title": "Title of the article",
        ///        "description": "Description of the article"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Returns newly created article</response>
        /// <response code="400">Returns bad request status code if request doesn't pass validation</response>
        /// <response code="401">Returns unauthorized status code if client is not authenticated</response>
        /// <response code="500">Returns internal server error if any unexpected error occured</response>
        [ApiKey]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetailsResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateArticleRequest request)
        {
            var result = await _mediator.Send(request);

            return CreatedAtRoute(nameof(Get), new { id = result }, null);
        }
    }
}
