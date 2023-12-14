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
        public TMP_Text ButtonText;
        public static GameManager Instance;
        public GameState State;

        public static event Action<GameState> OnGameStateChanged;

        private ResultInfo _data;
        [SerializeField] private LockManager _lockManager;

        private void Awake()
        {
            Instance = this;
            //Debug.Log(PlayerPrefs.GetString("eventSq") + " " + PlayerPrefs.GetString("roundNo") + " " + PlayerPrefs.GetString("memberId") + " " + PlayerPrefs.GetString("gameId"));
            
            //ResultInfo info = new ResultInfo();
            //info.eventSq = PlayerPrefs.GetString("eventSq") /*"3"*/;
            //info.roundNo = PlayerPrefs.GetString("roundNo") /*"3"*/;
            //info.memberId = PlayerPrefs.GetString("memberId") /*"3"*/;
            //info.gameId = PlayerPrefs.GetString("gameId") /*"3"*/;
            //info.holeNo = "1";
            //info.hole = "1";
            //info.star = "1";

            //if (ErrorException != null)
            //{
            //    string str = ErrorException.text;
            //    ErrorException.text = str + "Awake";
            //}
            //else
            //    Debug.Log("Null");

            //GameInfo response = new GameInfo();
            //StartCoroutine(HttpService.GetGameInfo(info, HandleGameInfoResponse));            
        }

        private void HandleGameInfoResponse(GameInfo response)
        {
            // Use the 'response' data here in your GameManager
            //Debug.Log($"Received GameInfo: {response}");

            if (_lockManager != null) 
            {
                //Debug.Log(response.holeNo);
                _lockManager.ActivateButtons(response.holeNo);
                //ButtonText.text = response.holeNo.ToString();
            }
            
        }


        private void Start()
        {
            //if (ErrorException != null)
            //{
            //    string str = ErrorException.text;
            //    ErrorException.text = str + "Start";
            //}

            _data = new ResultInfo();
            _data.eventSq = PlayerPrefs.GetString("eventSq");
            _data.roundNo = PlayerPrefs.GetString("roundNo");
            _data.memberId = PlayerPrefs.GetString("memberId");
            _data.gameId = PlayerPrefs.GetString("gameId");

            //_data.eventSq = "4";
            //_data.roundNo = "4";
            //_data.memberId = "4";
            //_data.gameId = "4";

            GameInfo response = new GameInfo();
            StartCoroutine(HttpService.GetGameInfo(_data, HandleGameInfoResponse));

            
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
            
            //_data.eventSq = PlayerPrefs.GetString("eventSq");
            //_data.roundNo = PlayerPrefs.GetString("roundNo");
            //_data.memberId = PlayerPrefs.GetString("memberId");
            //_data.gameId = PlayerPrefs.GetString("gameId");
            
            _data.holeNo = level;
            _data.hole = numberOfHits;
            _data.star = coin;            
            
            WriteUserScoreToServer();
        }

        
        public void GetEventSq(string eventSq)
        {
            _data = new ResultInfo();
            _data.eventSq = eventSq;
            
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

