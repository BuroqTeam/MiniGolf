using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGolf
{
    public class TriggerController : MonoBehaviour
    {

        public GameEvent[] gameEventsSO;
        

        bool _IsFinishTrue = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                Debug.Log("Finish Trigger " + other.name);
                if (_IsFinishTrue)
                {
                    gameEventsSO[1].Raise();
                    _IsFinishTrue = false;
                }
                
            }          
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                //Debug.Log("Wall");
                gameEventsSO[3].Raise();
            }
            //else if(collision.gameObject.CompareTag("Coin"))
            //{
            //    //Debug.Log("Coin");
            //    gameEventsSO[0].Raise();
            //}
            else if (collision.gameObject.CompareTag("Water"))
            {
                //Debug.Log("Water");
                gameEventsSO[5].Raise();
            }
            else if (collision.gameObject.CompareTag("Object"))
            {
                //Debug.Log("Object");
                gameEventsSO[6].Raise();
            }
            else if (collision.gameObject.CompareTag("Sand"))
            {
                //Debug.Log("Sand");
                gameEventsSO[4].Raise();
            }
            //else if (collision.gameObject.CompareTag("Finish"))
            //{
            //    //Debug.Log("Finish");
            //    gameEventsSO[1].Raise();
            //}
            else if (collision.gameObject.CompareTag("Out"))
            {
                Debug.Log("Out");
                gameEventsSO[7].Raise();
            }
            else if (collision.gameObject.CompareTag("Land"))
            {
                //Debug.Log("Land");
                gameEventsSO[8].Raise();
            }
            //else if ((gameObject.transform.position.x > PlusX) || (gameObject.transform.position.x < MinusX) || ())
            //{

            //}

        }





    }

}
