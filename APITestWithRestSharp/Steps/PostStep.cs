using APITestWithRestSharp.Models;
using APITestWithRestSharp.Utilities;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace APITestWithRestSharp.Steps
{
    [Binding]
    public sealed class PostStep
    {
        public RestClient client = new RestClient("http://localhost:3000/");
        public RestRequest request = new RestRequest();
        public IRestResponse<List<Books>> response = new RestResponse <List<Books>>();

        [Given(@"I perform a Get operation on ""(.*)""")]
        public void GivenIPerformAGetOperationOn(string url)
        {
            request = new RestRequest(url, Method.GET);            
        }

        [Given(@"I call the the ""(.*)"" post")]
        public void GivenICallTheThePost(int postId)
        {
            request.AddUrlSegment("postid", postId.ToString());
            response = client.ExecuteAsyncRequest<List<Books>>(request).GetAwaiter().GetResult();
        }

        [Then(@"the result should be the ""(.*)"" as ""(.*)""")]
        public void ThenTheResultShouldBeTheAs(string p0, string p1)
        {
            Assert.That(response.Data.FirstOrDefault().author.Equals("Vipin singh"), "did not match :FAILED");
        }
    }
}
