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
        public static GameManager Instance;
        public GameState State;

        public static event Action<GameState> OnGameStateChanged;

        private ResultInfo _data;

        //public TMP_Text ServerText1;
        //public TMP_Text ServerText2;
        //public TMP_Text ServerText3;
        //public TMP_Text ServerText4;

        private void Awake()
        {
            Instance = this;
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
            //_data.roundNo = "1";
            //_data.memberId = "1";
            //_data.gameId = "1";
            //_data.eventSq = "1";
            _data.holeNo = level;
            _data.hole = numberOfHits;
            _data.star = coin;
            WriteUserScoreToServer();
        }

        //public void GetUserID(string eventSq)
        //{
        //    _data.eventSq = eventSq;  
        //    string str = eventSq;
        //    ServerText.text = " = " + str + " = ";           
        //}

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

        public async void WriteUserScoreToServer()
        {
            ResponseResultInfo responce = await HttpService.SaveResultInfo(_data);            
            Debug.Log(responce.result);
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

