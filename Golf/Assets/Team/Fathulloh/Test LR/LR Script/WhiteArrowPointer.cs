using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer2
{
    /// <summary>
    /// Main ball ni oldidan chiquvchi 6 ta oq uchburchaklarni qilish uchun script. 
    /// </summary>
    public class WhiteArrowPointer : MonoBehaviour
    {
        public GameObject MainBall;
        public GameObject CircleObj; 
        public Vector3 pointA;
        public Vector3 pointB;


        void Start()
        {
            //MakeVectorWithTwoDot();
        }


        /// <summary>
        /// GolfBall ning oldida chiqib turuvchi va yo‘nalishni ko‘rsatuvchi arrow uchun script.
        /// </summary>
        public void ArrowPointer()
        {
            pointA = MainBall.transform.position;
            pointB = -CircleObj.transform.position;
            gameObject.transform.position = pointA;

            Vector3 direction = pointB - pointA;
            Quaternion currentRotation = gameObject.transform.rotation;
            Quaternion rotation = Quaternion.LookRotation(direction); // Yo'nalish quaternioni
            
            gameObject.transform.rotation = new Quaternion(currentRotation.x, rotation.y, currentRotation.z, rotation.w);
            //Debug.Log("direction.z = " + direction + " rotation = " + rotation + " currentRotation = " + currentRotation);
        }


        public void WhiteArrowSwitcher(float floNumber)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < floNumber; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }


        //public void MakeVectorWithTwoDot()
        //{
        //    pointA = MainBall.transform.position;
        //    pointB = -CircleObj.transform.position;
        //    Vector3 lineVector = CalculateLineVector(pointA, pointB);
        //    Debug.Log("Line Vector: " + lineVector);

        //    Vector3 rotationAngle = CalculateRotationAngle(lineVector);
        //    Debug.Log("Rotation Angle: " + rotationAngle + " degrees ");

        //    Quaternion newRotation = Quaternion.LookRotation(rotationAngle);
        //    gameObject.transform.rotation = newRotation;
        //}


        //Vector3 CalculateLineVector(Vector3 start, Vector3 end)
        //{
        //    return end - start;
        //}


        //Vector3 CalculateRotationAngle(Vector3 vector)
        //{
        //    Vector3 angles = new Vector3();

        //    angles.x = Mathf.Atan2(vector.y, vector.z) * Mathf.Rad2Deg;
        //    angles.y = Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg;
        //    angles.z = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        //    return angles;
        //}        

    }
}
