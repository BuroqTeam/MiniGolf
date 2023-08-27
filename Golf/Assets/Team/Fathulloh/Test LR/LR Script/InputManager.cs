using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer2
{
    public class InputManager : MonoBehaviour
    {
        public GameObject MainBall;
        public GameObject CircleObj;  //F+
        [SerializeField] private LineRenderer _trajectoryLine;
        public LineRenderer WhiteArrowWay;
        

        public void ShowTrajectoryLine()
        {
            if (Input.GetMouseButton(0))
            {                
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MainBall.GetComponent<DragDrop>().MousePos);
                Vector3 mainBallPos = MainBall.transform.position;
                _trajectoryLine.enabled = true;
                _trajectoryLine.positionCount = 2;

                float lineLength = Vector3.Distance(CircleObj.transform.position, mainBallPos);   //F+
                if (lineLength < redLineLength)
                {
                    if (lineLength < greenLineLength)                    {
                        //Debug.Log("green");
                        DrawWhiteArrowWay(CircleObj.transform.position, mainBallPos, greenColor);
                    }
                    else if (lineLength < yellowLineLength)                    {
                        //Debug.Log("yellow");
                        DrawWhiteArrowWay(CircleObj.transform.position, mainBallPos, yellowColor);
                    }
                    else if (lineLength < redLineLength)                    {
                        //Debug.Log("red");
                        DrawWhiteArrowWay(CircleObj.transform.position, mainBallPos, redColor);
                    }

                    _trajectoryLine.SetPosition(0, CircleObj.transform.position);
                    _trajectoryLine.SetPosition(1, mainBallPos); ;
                }
                
            }
        }


        Color greenColor = new(0.27f, 0.85f, 0.2f);
        Color yellowColor = new(1, 1, 0);
        Color redColor = new(0.83f, 0.16f, 0.05f);

        readonly float greenLineLength = 4.0f;
        readonly float yellowLineLength = 7.0f;
        [HideInInspector] public readonly float redLineLength = 10.0f;


        void DrawWhiteArrowWay(Vector3 vec1, Vector3 vec2, Color newColor)
        {
            WhiteArrowWay.SetColors(newColor, newColor);

            //Material newMat = new(WhiteArrowWay.material);   // materialni rangini o‘zgartirish ham juda yaxshi ideya edi. Biroq rangi o‘zgarmadi. 
            //newMat.color = newColor;

            float lineLength = Vector3.Distance(vec1, vec2);

            //Debug.Log("lineLength = " + lineLength);
            CircleObj.GetComponent<SpriteRenderer>().color = newColor;

            WhiteArrowWay.positionCount = 2;
            WhiteArrowWay.SetPosition(0, vec1);
            WhiteArrowWay.SetPosition(1, vec2);           
        }


    }
}
