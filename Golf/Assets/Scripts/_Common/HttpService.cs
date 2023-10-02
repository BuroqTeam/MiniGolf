 
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer
{
    public static class StreamHelpers
    {
        public static byte[] ReadFully(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

    public class HttpService
    {
        #region Url  
        public static string SERVER_URL = "http://192.168.219.102:5000/api/";
        public static string URL_SAVE_RESULT_INFO = SERVER_URL + ""; 
        #endregion

        private static HttpStatusCode StatusCode;
        private static readonly WebClient webClient = new WebClient();
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public async static Task<ResponseResultInfo> SaveResultInfo(ResultInfo data)
        {
            ResponseResultInfo response = new ResponseResultInfo();
            try
            {
                var receivedData = await RequestPostMethod(URL_SAVE_RESULT_INFO, data);
                response = JsonConvert.DeserializeObject<ResponseResultInfo>(receivedData, settings);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseResultInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseResultInfo>(); }

            return response;
        }

        /// <summary>
        /// Save data to the server.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static async Task<string> RequestPostMethod(string url, Object obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(obj), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }
         
        private static async Task<string> RequestDeleteMethod(string url, Object obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(obj), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static async Task<string> RequestDeleteMethod(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static async Task<string> RequestPutMethod(string url, Object obj)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(obj), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        /// <summary>
        /// Get data from server.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<string> RequestGetMethod(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static async Task<string> RequestPostMethod(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.POST);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        private static TResponse CheckObj<TResponse>(object response) where TResponse : new()
        {
            if (response == null)
                return new TResponse();

            return (TResponse)response;
        }

        private static T CreateResponseObj<T>() where T : IResponse, new()
        {
            T t = new T();
            //t.Check();
            return t;
        }

        private static T ConvertResponseObj<T>(Response response) where T : IResponse, new()
        {
            T t = (T)Convert.ChangeType(response, typeof(T));
            if (t == null)
                t = new T();
            //t.Check();

            return t;
        }
    } 

    public interface IResponse
    {

    }

    public class Response
    {

    }

    public class ResponseResultInfo : Response, IResponse
    {

    }

    public class ResultInfo
    {

    }
}