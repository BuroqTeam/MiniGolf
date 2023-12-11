using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApiTest;
using TMPro;

namespace MiniGolf
{
    public class GameManager : MonoBehaviour
    {
        public TMP_Text ErrorException;
        public static GameManager Instance;
        public GameState State;

        public static event Action<GameState> OnGameStateChanged;

        private ResultInfo _data;

        
        private void Awake()
        {
            Instance = this;

            ResultInfo info = new ResultInfo();
            info.eventSq = "1";
            info.roundNo = "1";
            info.memberId = "1";
            info.gameId = "1";
            //info.holeNo = "1";
            //info.hole = "1";
            //info.star = "1";
            //StartCoroutine(HttpService.NewSaveResultInfo(info));

            GameInfo response = new GameInfo();
            StartCoroutine(HttpService.GetGameInfo(info, HandleGameInfoResponse));
            int g = 0;
        }

        private void HandleGameInfoResponse(GameInfo response)
        {
            // Use the 'response' data here in your GameManager
            Debug.Log($"Received GameInfo: {response}");
        }

        private void Start()
        {
            UpdateGameState(GameState.EntryAnimation);
            
        }
         

        public void UpdateGameState(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.EntryAnimation:

                    break;
                case GameState.Available:
                    break;
                case GameState.Targeting:
                    break;
                case GameState.Moving:
                    break;
                case GameState.Out:
                    break;
                case GameState.Finish:
                    break;
            }
            OnGameStateChanged?.Invoke(newState);
        }

        public void UpdateResultInfo(string level, string numberOfHits, string coin)
        {
            //_data.eventSq = "1";
            //_data.roundNo = "1";
            //_data.memberId = "1";
            //_data.gameId = "1";

            _data.eventSq = PlayerPrefs.GetString("eventSq");
            _data.roundNo = PlayerPrefs.GetString("roundNo");
            _data.memberId = PlayerPrefs.GetString("memberId");
            _data.gameId = PlayerPrefs.GetString("gameId");
            
            _data.holeNo = level;
            _data.hole = numberOfHits;
            _data.star = coin;            
            
            WriteUserScoreToServer();
        }

        
        public void GetEventSq(string eventSq)
        {
            _data = new ResultInfo();
            _data.eventSq = eventSq;
            //ErrorException.text = eventSq;
            PlayerPrefs.SetString("eventSq", eventSq);
            //string str = eventSq;
            //ServerText1.text = " = " + str + " = ";
        }

        public void GetRoundNo(string roundNo)
        {
            _data.roundNo = roundNo;
            PlayerPrefs.SetString("roundNo", roundNo);
            //string str = roundNo;
            //ServerText2.text = " = " + str + " = ";
        }

        public void GetMemberId(string memberId)
        {
            _data.memberId = memberId;
            PlayerPrefs.SetString("memberId", memberId);
            //string str = memberId;
            //ServerText3.text = " = " + str + " = ";
        }

        public void GetGameId(string gameId)
        {
            _data.gameId = gameId;
            PlayerPrefs.SetString("gameId", gameId);
            //string str = gameId;
            //ServerText4.text = " = " + str + " = ";

            //_data.eventSq = "1";
            //_data.roundNo = "1";
            //_data.memberId = "1";
            //_data.gameId = "1";

            //HttpService.GetGameInfo
        }

        public void WriteUserScoreToServer()
        {
            Debug.Log("WriteUserScoreToServer");
            try
            {                
                StartCoroutine(HttpService.NewSaveResultInfo(_data));
                //ResponseResultInfo responce = await HttpService.SaveResultInfo(_data);

                //ErrorException.text = responce.result;
                //Debug.Log(responce.result);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                //ErrorException.text = e.Message;
                //ErrorException.text = HttpService.stringError;
            }


            //try
            //{
            //    StartCoroutine(HttpService.SaveResultInfo(_data));
            //}
            //catch (Exception e)
            //{
            //    Debug.LogException(e);
            //    ErrorException.text = e.Message;
            //}

            //Debug.Log(_data.eventSq + " " + _data.roundNo + " " + _data.memberId + " " + _data.gameId + " " + _data.holeNo + " " + _data.hole + " " + _data.star);
        }


        public void WriteDataFromServer()
        {
            //_data.eventSq = "1";
            //_data.roundNo = "1";
            //_data.memberId = "1";
            //_data.gameId = "1";
            ErrorException.text = "eventSq=" + _data.eventSq + " roundNo=" + _data.roundNo + " memberId=" + _data.memberId + " gameId=" + _data.gameId;
        }
        
        
    }

    public enum GameState
    {
        EntryAnimation,
        Available,
        Targeting,
        Moving,
        Out,
        Finish,
    }

}

