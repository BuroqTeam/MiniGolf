using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGolf
{
    public class TriggerController : MonoBehaviour
    {

        public GameEvent[] gameEventsSO;


        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Coin":
                    gameEventsSO[0].Raise();
                    break;
                case "Wall":
                    gameEventsSO[3].Raise();
                    break;
                case "Water":
                    gameEventsSO[5].Raise();
                    break;
                case "Object":
                    gameEventsSO[6].Raise();
                    break;
                case "Sand":
                    gameEventsSO[4].Raise();
                    break;
                case "Finish":
                    gameEventsSO[1].Raise();
                    break;
                case "Out":
                    gameEventsSO[7].Raise();
                    break;
                case "Land":                    
                    gameEventsSO[8].Raise();
                    break;

            }
        }







    }


}
