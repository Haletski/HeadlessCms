using Articles.WebAPI.Application.Resources;
using Articles.WebAPI.Application.Resources.Arcticles;
using Articles.WebAPI.Integration.Tests.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Articles.WebAPI.Integration.Tests
{
    [TestFixture]
    class ArticleControllerTests : BaseScenario
    {
        private TestServer _testServer;

        [OneTimeSetUp]
        public void SetUp()
        {
            _testServer = CreateServer();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _testServer.Dispose();
        }

        [Test]
        public async Task Get_Articles_Without_Any_Query_Parameters_Returns_Articles()
        {
            // Arrange
            var expectedResult = new List<ArticleResource>
            {
                new ArticleResource
                {
                    ArticleId = 1,
                    AddedDate = new DateTime(2021, 09, 09),
                    Title = "Elon Musk says Tesla owners 'don't seem to listen to me' because they ignore an NDA and share lots of videos of the company's 'full self-driving' tech",
                    Description = "\"I don't know why there's an NDA\" for Tesla's Full Self-Driving beta,\" Elon Musk said. \"We probably don't need it.\" VICE first reported on the NDA."
                },
                new ArticleResource
                {
                    ArticleId = 2,
                    AddedDate = new DateTime(2021, 09, 10),
                    Title = "Deals on Machines, Beans, and More for National Coffee Day",
                    Description = "This gear will upgrade your at-home café. Plus, the scoop on where to get a free cup of joe today."
                },
                new ArticleResource
                {
                    ArticleId = 3,
                    AddedDate = new DateTime(2021, 09, 11),
                    Title = "Links 9/29/2021",
                    Description = "Our popular links: bird migration, desalination, food scarcity, fading AZ, kid vaccines, Zoom forever? UK petrol, moonsoon shift, New Cold War, budget staredown, judiciary in crisis? Tesla hits cops, crypto, yacht macho"
                }
            };

            // Act
            var response = await _testServer.CreateRequest("api/v1/articles").SendAsync(HttpMethods.Get);

            var actualResult = await response.Content.GetContentAsync<List<ArticleResource>>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                actualResult.Should().BeEquivalentTo(expectedResult);
            });
        }

        [Test]
        public async Task Get_Articles_With_Query_Parameters_Returns_Articles()
        {
            // Arrange
            var expectedResult = new List<ArticleResource>
            {
                new ArticleResource
                {
                    ArticleId = 1,
                    AddedDate = new DateTime(2021, 09, 09),
                    Title = "Elon Musk says Tesla owners 'don't seem to listen to me' because they ignore an NDA and share lots of videos of the company's 'full self-driving' tech",
                    Description = "\"I don't know why there's an NDA\" for Tesla's Full Self-Driving beta,\" Elon Musk said. \"We probably don't need it.\" VICE first reported on the NDA."
                },
                new ArticleResource
                {
                    ArticleId = 2,
                    AddedDate = new DateTime(2021, 09, 10),
                    Title = "Deals on Machines, Beans, and More for National Coffee Day",
                    Description = "This gear will upgrade your at-home café. Plus, the scoop on where to get a free cup of joe today."
                }
            };

            // Act
            var response = await _testServer.CreateRequest("api/v1/articles?page=1&pageSize=2&orderBy=articleId&orderType=asc&format=json").SendAsync(HttpMethods.Get);

            var actualResult = await response.Content.GetContentAsync<List<ArticleResource>>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                actualResult.Should().BeEquivalentTo(expectedResult);
            });
        }

        [Test]
        public async Task Get_Articles_When_Query_Parameters_Invalid_Returns_ErrorDetailsResource()
        {
            // Arrange
            var expectedResult = new ErrorDetailsResource
            {
                Messages = new List<string>
                {
                    "Page value should be greater than 0",
                    "Page size should be greater than 0",
                    "Order by is invalid. Expected values: articleId,title,description,addedDate",
                    "Order type is invalid. Expected values: asc,desc"
                }
            };

            // Act
            var response = await _testServer.CreateRequest("api/v1/articles?page=-1&pageSize=-1&orderBy=test&orderType=test").SendAsync(HttpMethods.Get);

            var actualResult = await response.Content.GetContentAsync<ErrorDetailsResource>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

                actualResult.Should().BeEquivalentTo(expectedResult);
            });
        }
    }
}
