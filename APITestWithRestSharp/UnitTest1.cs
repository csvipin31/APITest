using APITestWithRestSharp.Models;
using APITestWithRestSharp.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;

namespace APITestWithRestSharp
{

    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.GET);
            request.AddUrlSegment("postid", 1);
            var response = client.Execute(request);
            var jobj = response.DeserializeReponse<Books>();
            Assert.IsTrue(jobj.author.Equals("Vipin singh"), "did not match :FAILED");

        }


        [Test]
        public void TestMethod2()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.GET);
            //var request = new RestRequest("posts", Method.GET);
            request.AddUrlSegment("postid", 1);            
            var response = client.Execute(request);
            JObject ob = JObject.Parse(response.Content);
            
        }

        [Test]
        public void TestMethod3()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Books() { id =14,author ="Annika",title ="I will do it" });
            //request.AddUrlSegment("postid", 10);
            var response = client.Execute(request);
            JObject ob = JObject.Parse(response.Content);


        }

        [Test]
        public void TestMethod4()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}/profile", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new  {  name ="Rajkumar Rao"});
            request.AddUrlSegment("postid", 1);
           
            var response = client.Execute(request);
            
            JObject ob = JObject.Parse(response.Content);


        }

        [Test]
        public void TestMethod5()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.GET);
            request.AddUrlSegment("postid", 1);
            var deserial = new JsonDeserializer();
            var response = client.Execute<Books>(request);
            
            var jobj = response.DeserializeReponse<Books>();
            Assert.IsTrue(jobj.author.Equals("Vipin singh"), "did not match :FAILED");

           
            var request2 = new RestRequest("posts", Method.GET);
            var response2 = client.Execute<List<Books>>(request2);
            var respons = client.Execute<Books>(request2);

        }

        [Test]
        public void TestMethodPostAsync()
        {
            var client = new RestClient("http://localhost:3000/");
            //var request = new RestRequest("posts/{postid}", Method.GET);
            //request.AddUrlSegment("postid", 1);

            var request = new RestRequest("posts", Method.GET);
            var response = client.ExecuteAsyncRequest<List<Books>>(request).GetAwaiter().GetResult();
            //var response =  RestHelp.ExecuteAsyncRequest<List<Books>>(client, request).GetAwaiter().GetResult();
            var response2 = client.ExecutePostTaskAsync<List<Books>>(request).GetAwaiter().GetResult();

        }

        


    }
}
