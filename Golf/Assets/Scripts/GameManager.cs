using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApiServer;

namespace MiniGolf
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;
        public GameState State;

        public static event Action<GameState> OnGameStateChanged;


        private void Awake()
        {
            Instance = this;
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


        public void WriteUserScoreToServer()
        {
            WriteScore();
        }


        async void WriteScore()
        {
            ResultInfo data = new ResultInfo();
            ResponseResultInfo responce = await HttpService.SaveResultInfo(data);
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

