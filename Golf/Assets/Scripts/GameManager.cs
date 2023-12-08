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

            //ResultInfo info = new ResultInfo();
            //info.eventSq = "11";
            //info.roundNo = "11";
            //info.memberId = "11";
            //info.gameId = "11";
            //info.holeNo = "1";
            //info.hole = "1";
            //info.star = "1";
            //StartCoroutine(HttpService.NewSaveResultInfo(info));
        }

        private void Start()
        {
            UpdateGameState(GameState.EntryAnimation);
            _data = new ResultInfo();
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
            _data.holeNo = level;
            _data.hole = numberOfHits;
            _data.star = coin;
            
            
            WriteUserScoreToServer();
        }

        
        public void GetEventSq(string eventSq)
        {
            _data.eventSq = eventSq;
            //string str = eventSq;
            //ServerText1.text = " = " + str + " = ";
        }

        public void GetRoundNo(string roundNo)
        {
            _data.roundNo = roundNo;
            //string str = roundNo;
            //ServerText2.text = " = " + str + " = ";
        }

        public void GetMemberId(string memberId)
        {
            _data.memberId = memberId;
            //string str = memberId;
            //ServerText3.text = " = " + str + " = ";
        }

        public void GetGameId(string gameId)
        {
            _data.gameId = gameId;
            //string str = gameId;
            //ServerText4.text = " = " + str + " = ";
        }

        public void WriteUserScoreToServer()
        {

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
                ErrorException.text = e.Message;
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

