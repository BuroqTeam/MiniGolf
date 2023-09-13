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
            //switch (other.tag)
            //{
            //    case "Coin":
            //        Debug.Log("Coin");
            //        gameEventsSO[0].Raise();
            //        break;
            //    case "Wall":
            //        Debug.Log("Wall");
            //        gameEventsSO[3].Raise();
            //        break;
            //    case "Water":
            //        Debug.Log("Water");
            //        gameEventsSO[5].Raise();
            //        break;
            //    case "Object":
            //        Debug.Log("Object");
            //        gameEventsSO[6].Raise();
            //        break;
            //    case "Sand":
            //        Debug.Log("Sand");
            //        gameEventsSO[4].Raise();
            //        break;
            //    case "Finish":
            //        Debug.Log("Finish");
            //        gameEventsSO[1].Raise();
            //        break;
            //    case "Out":
            //        Debug.Log("Out");
            //        gameEventsSO[7].Raise();
            //        break;
            //    case "Land":
            //        Debug.Log("Land");
            //        gameEventsSO[8].Raise();
            //        break;

            //}
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                //Debug.Log("Wall");
                gameEventsSO[3].Raise();

                Vector3 oppositeDirection = -collision.contacts[0].normal;
                float rotationAngle = Vector3.SignedAngle(oppositeDirection, GetComponent<Rigidbody>().velocity, Vector3.up);
                GetComponent<Rigidbody>().velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity, oppositeDirection); // bu kodlar Ball scriptini ichida bo'lishi kerak edi.
                //Debug.Log("Rotation Angle: " + rotationAngle + " oppositeDirection = " + oppositeDirection);
                //Debug.Log("GetComponent<Rigidbody>().velocity = " + GetComponent<Rigidbody>().velocity);
                //Debug.Log("Vector3.Reflect(rb.velocity, oppositeDirection) = " + Vector3.Reflect(GetComponent<Rigidbody>().velocity, oppositeDirection));

            }

        }



    }


}
