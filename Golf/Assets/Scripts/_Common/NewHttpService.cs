using ApiTest;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;

namespace NewApiTest
{
    public class NewHttpService : MonoBehaviour // I don't use this script in game. 
    {
        #region Url
        public static string SERVER_URL = "https://api.unbizgolf.kr/event/mini/game/save?";
        #endregion

        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        //public static IEnumerator SaveResultInfo(ResultInfo data)
        //{
        //    ResponseResultInfo response = new ResponseResultInfo();
        //    try
        //    {
        //        string strUrl = $"{SERVER_URL}eventSq={data.eventSq}&roundNo={data.roundNo}&memberId={data.memberId}&gameId={data.gameId}&holeNo={data.holeNo}&hole={data.hole}&star={data.star}";

        //        using (UnityWebRequest request = UnityWebRequest.Get(strUrl))
        //        {
        //             return request.SendWebRequest()

        //            if (request.result != UnityWebRequest.Result.Success)
        //            {
        //                Debug.LogError(request.error);
        //                response.result = "0"; // Set failure result
        //            }
        //            else
        //            {
        //                response = JsonConvert.DeserializeObject<ResponseResultInfo>(request.downloadHandler.text, settings);
        //            }
        //        }
        //    }
        //    catch (JsonReaderException)
        //    {
        //        response.result = "0"; // Set failure result
        //    }
        //    catch (HttpRequestException)
        //    {
        //        response.result = "0"; // Set failure result
        //    }

        //    // Handle the response as needed
        //}

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


