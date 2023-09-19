using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGolf
{
    public class PauseButton : MonoBehaviour
    {
        private bool isPaused = false;

        public void PushPauseButton()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }


    }
}
