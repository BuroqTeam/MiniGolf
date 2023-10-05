 
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest
{ 
    public class HttpService
    {
        #region Url  
        public static string SERVER_URL = "https://api.unbizgolf.kr/event/mini/game/save?";  
        #endregion
         
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static ResponseResultInfo SaveResultInfo(ResultInfo data)
        {
            ResponseResultInfo response = new ResponseResultInfo();
            try
            {
                string strUrl = $"{SERVER_URL}eventSq={data.eventSq}&roundNo={data.roundNo}&memberId={data.memberId}&gameId={data.gameId}&holeNo={data.holeNo}&hole={data.hole}&star={data.star}";
                response.result = RequestGetMethod(strUrl);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseResultInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseResultInfo>(); }

            return response;
        }
         
        /// <summary>
        /// Get data from server.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string RequestGetMethod(string url)
        {
            var client = new RestClient(url); 
            var request = new RestRequest(url, Method.Get);
            RestResponse response = client.Execute(request);

            return response.Content;
        }
          
        private static T CreateResponseObj<T>() where T : IResponse, new()
        {
            T t = new T();
            //t.Check();
            return t;
        }
         
    } 

    public interface IResponse
    {

    }

    public class Response
    {
        /// <summary>
        /// 1 = success, otherwise failed
        /// </summary>
        public string result { get; set; } = "1";
    }

    public class ResponseResultInfo : Response, IResponse
    {

    }

    public class ResultInfo
    {
        public string eventSq { get; set; }
        public string roundNo { get; set; }
        public string memberId { get; set; }
        public string gameId { get; set; }
        public string holeNo { get; set; }
        public string hole { get; set; }
        public string star { get; set; }
    }
}