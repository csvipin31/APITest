using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Threading.Tasks;

namespace APITestWithRestSharp.Utilities
{
    public static class RestHelp
    {
        public static async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(this IRestClient client, IRestRequest request) where T : class, new()
        {
            var taskCompletion = new TaskCompletionSource<IRestResponse<T>>();
            client.ExecuteAsync<T>(request, (response) =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    taskCompletion.SetResult(response);
                }
                else
                {
                    if (response.ErrorException != null)
                    {
                        Console.WriteLine("{0} {1}", response.StatusCode, response.ErrorMessage);
                        const string message = "Error in fetching response.";
                        throw new ApplicationException(message, response.ErrorException);
                    }
                }
            });
            return await taskCompletion.Task;
        }
        public static T DeserializeReponse<T>(this IRestResponse restResponse)
        {
            var deserial = new JsonDeserializer();
            return deserial.Deserialize<T>(restResponse);

        }

    }
}
