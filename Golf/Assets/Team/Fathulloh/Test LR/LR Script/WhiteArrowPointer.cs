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
        public InputManager Inputmanager;
        public GameObject MainBall;
        public GameObject CircleObj; 
        [HideInInspector] public Vector3 pointA;
        [HideInInspector] public Vector3 pointB;

        public float greenLength;
        public float yellowLength;
        public float redLength;

        void Start()
        {
            GetData();
            //MakeVectorWithTwoDot();
        }


        void GetData()
        {
            greenLength = Inputmanager.greenLineLength;
            yellowLength = Inputmanager.yellowLineLength;
            redLength = Inputmanager.redLineLength;
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


        public void CheckLineColor(float lineLength)
        {
            //Debug.Log("(float)yellowLength / 2 = " + (float)yellowLength / 2 + " (float)redLength / 2 = " + (float)redLength / 2);
            if (lineLength < (float)greenLength / 2)
            {
                //Debug.Log("green little = " + (float)greenLength / 2);
                WhiteArrowSwitcher(1);
            }
            else if (lineLength < greenLength)
            {
                //Debug.Log("Big green");
                WhiteArrowSwitcher(2);
            }
            else if (lineLength < yellowLength - (float)(yellowLength - greenLength) / 2)
            {
                //Debug.Log("yellow little = " + (float)yellowLength / 2);
                WhiteArrowSwitcher(3);
            }
            else if (lineLength < yellowLength)
            {
                //Debug.Log("Big yellow");
                WhiteArrowSwitcher(4);
            }
            else if (lineLength < redLength - (float)(redLength - yellowLength) / 2)
            {
                //Debug.Log("red little = " + (float)redLength / 2);
                WhiteArrowSwitcher(5);
            }
            else
            {
                //Debug.Log("Big red");
                WhiteArrowSwitcher(6);
            }
        }


        public void WhiteArrowSwitcher(int intNumber)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < intNumber; i++)
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
