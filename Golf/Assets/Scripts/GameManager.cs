using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApiTest;

namespace MiniGolf
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;
        public GameState State;

        public static event Action<GameState> OnGameStateChanged;

        private ResultInfo _data;

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

            _data.holeNo = level.ToString();
            _data.hole = numberOfHits;
            _data.star = coin;
            WriteUserScoreToServer();
        }

        public void GetUserID(string eventSq, string roundNo, string userID, string gameId)
        {
            _data.eventSq = eventSq;//F+
            _data.roundNo = roundNo;// F++
            _data.memberId = userID;
            _data.gameId = gameId; //F++
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

