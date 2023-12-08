using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

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

        
        public async static Task<ResponseResultInfo> SaveResultInfo(ResultInfo data)
        {
            ResponseResultInfo response = new ResponseResultInfo();
            try
            {
                string strUrl = $"{SERVER_URL}eventSq={data.eventSq}&roundNo={data.roundNo}&memberId={data.memberId}&gameId={data.gameId}&holeNo={data.holeNo}&hole={data.hole}&star={data.star}";
                //response.result = await RequestGetMethod(strUrl);
            }
            catch (JsonReaderException) { return CreateResponseObj<ResponseResultInfo>(); }
            catch (HttpRequestException) { return CreateResponseObj<ResponseResultInfo>(); }
            //catch (Exception e)
            //{                
            //    response.result = "Error: " + e.Message;
            //}

            return response;
        }

        public static IEnumerator NewSaveResultInfo(ResultInfo data)
        {
            ResponseResultInfo response = new ResponseResultInfo();
            string strUrl = $"{SERVER_URL}eventSq={data.eventSq}&roundNo={data.roundNo}&memberId={data.memberId}&gameId={data.gameId}&holeNo={data.holeNo}&hole={data.hole}&star={data.star}";

            using (UnityWebRequest request = UnityWebRequest.Get(strUrl))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    response.result =0; // Set failure result
                }
                else
                {

                    //response = JsonConvert.DeserializeObject<ResponseResultInfo>(request.downloadHandler.text, settings);
                    if (request.downloadHandler.text.IsUnityNull())
                    {
                        response.result = request.downloadHandler.text.Equals("1")? 1 : 0;
                    } 
                }
            }

         
        }




        private static async Task<string> RequestGetMethod(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

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
        public int result { get; set; } = 0;
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