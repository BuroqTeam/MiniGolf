using NewApiTest;
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
        public delegate void GameInfoCallback(GameInfo response);

        #region Url  
        public static string SERVER_URL = "https://api.unbizgolf.kr/event/mini/game/save?";
        public static string GET_GAME_INFO = "https://api.unbizgolf.kr/event/mini/game/?";
        #endregion
         
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        
        //public async static Task<ResponseResultInfo> SaveResultInfo(ResultInfo data)
        //{
        //    ResponseResultInfo response = new ResponseResultInfo();
        //    try
        //    {
        //        string strUrl = $"{SERVER_URL}eventSq={data.eventSq}&roundNo={data.roundNo}&memberId={data.memberId}&gameId={data.gameId}&holeNo={data.holeNo}&hole={data.hole}&star={data.star}";
        //        //response.result = await RequestGetMethod(strUrl);
        //    }
        //    catch (JsonReaderException) { return CreateResponseObj<ResponseResultInfo>(); }
        //    catch (HttpRequestException) { return CreateResponseObj<ResponseResultInfo>(); }
        //    //catch (Exception e)
        //    //{                
        //    //    response.result = "Error: " + e.Message;
        //    //}

        //    return response;
        //}

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
                    stringError = request.error.ToString();
                    response.result =0; // Set failure result
                }
                else
                {

                    //response = JsonConvert.DeserializeObject<ResponseResultInfo>(request.downloadHandler.text, settings);
                    if (!request.downloadHandler.text.IsUnityNull())
                    {
                        response.result = request.downloadHandler.text.Equals("1")? 1 : 0;
                    } 
                }
            }

         
        }


        //https://api.unbizgolf.kr/event/mini/game/?eventSq=2&roundNo=2&memberId=2&gameId=2
        public static IEnumerator GetGameInfo(ResultInfo data, GameInfoCallback callback)
        {
            string strUrl = $"{GET_GAME_INFO}eventSq={data.eventSq}&roundNo={data.roundNo}&memberId={data.memberId}&gameId={data.gameId}";

            using (UnityWebRequest request = UnityWebRequest.Get(strUrl))
            {
                yield return request.SendWebRequest();
                
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    stringError = request.error?.ToString();
                    //response.result = 0; // Set failure result
                }
                else
                { 
                    if (!request.downloadHandler.text.IsUnityNull())
                    {
                        GameInfo response = JsonUtility.FromJson<GameInfo>(request.downloadHandler.text);
                        callback?.Invoke(response);
                    }
                } 
            }


        }


        public static string stringError;
        public string ReturnError()
        {
            return stringError;
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
        //
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


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [System.Serializable]
    public class GameInfo
    {
        public int eventSq;// { get; set; }
        public string roundNo; /*{ get; set; }*/
        public string memberId; /*{ get; set; }*/
        public string gameId;
        public int hole1;
        public int hole2;
        public int hole3;
        public int hole4;
        public int hole5;
        public int hole6;
        public int hole7;
        public int hole8;
        public int hole9;
        public int star1;
        public int star2;
        public int star3;
        public int star4;
        public int star5;
        public int star6;
        public int star7;
        public int star8;
        public int star9;
        public int holeNo;
        public int star;
        public int hole;
    }



}