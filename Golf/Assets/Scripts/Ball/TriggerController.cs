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
                    Debug.Log("Coin");
                    gameEventsSO[0].Raise();
                    break;
                case "Wall":
                    Debug.Log("Wall");
                    gameEventsSO[3].Raise();
                    break;
                case "Water":
                    Debug.Log("Water");
                    gameEventsSO[5].Raise();
                    break;
                case "Object":
                    Debug.Log("Object");
                    gameEventsSO[6].Raise();
                    break;
                case "Sand":
                    Debug.Log("Sand");
                    gameEventsSO[4].Raise();
                    break;
                case "Finish":
                    Debug.Log("Finish");
                    gameEventsSO[1].Raise();
                    break;
                case "Out":
                    Debug.Log("Out");
                    gameEventsSO[7].Raise();
                    break;
                case "Land":
                    Debug.Log("Land");
                    gameEventsSO[8].Raise();
                    break;

            }
        }







    }


}
