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


        Color greenColor = new(0.27f, 0.85f, 0.2f);
        Color yellowColor = new(1, 1, 0);
        Color redColor = new(0.83f, 0.16f, 0.05f);

        readonly float greenLineLength = 4.0f;
        readonly float yellowLineLength = 7.0f;
        [HideInInspector] public readonly float redLineLength = 10.0f;


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
                        DrawWhiteArrowWay(CircleObj.transform.position, mainBallPos, greenColor);
                    }
                    else if (lineLength < yellowLineLength)                    {
                        DrawWhiteArrowWay(CircleObj.transform.position, mainBallPos, yellowColor);
                    }
                    else if (lineLength < redLineLength)                    {
                        DrawWhiteArrowWay(CircleObj.transform.position, mainBallPos, redColor);
                    }

                    _trajectoryLine.SetPosition(0, NewPosForCircle(CircleObj.transform.position, mainBallPos));
                    //_trajectoryLine.SetPosition(0, CircleObj.transform.position);
                    _trajectoryLine.SetPosition(1, mainBallPos);
                }
                
            }
        }
        


        void DrawWhiteArrowWay(Vector3 vec1, Vector3 vec2, Color newColor)
        {
            WhiteArrowWay.SetColors(newColor, newColor);

            //Material newMat = new(WhiteArrowWay.material);   // materialni rangini o‘zgartirish ham juda yaxshi ideya edi. Biroq rangi o‘zgarmadi. 
            //newMat.color = newColor;
            //WhiteArrowWay.material = newMat;

            CircleObj.GetComponent<SpriteRenderer>().color = newColor;

            WhiteArrowWay.positionCount = 2;
            WhiteArrowWay.SetPosition(0, NewPosForCircle(vec1, vec2));
            //WhiteArrowWay.SetPosition(0, vec1);
            WhiteArrowWay.SetPosition(1, vec2);            
        }


        /// <summary>
        /// Circle uchun yangi pozitsiyani belgilab beruvchi metod.
        /// </summary>
        /// <param name="vecCircle">Aylananing pozitsiyasi</param>
        /// <param name="vecBall">Golf koptogining pozitsiyasi</param>
        /// <returns></returns>
        Vector3 NewPosForCircle(Vector3 vecCircle, Vector3 vecBall)
        {
            return SetNewDistance();   // F++

            //float moveX = 0.64f;
            //float moveZ = 0.64f;

            //if ((vecCircle.x > vecBall.x) && (vecCircle.z == vecBall.z))
            //{
            //    Debug.Log("1");
            //    return new Vector3(vecCircle.x - moveX, vecCircle.y, vecCircle.z);
            //}
            //else if ((vecCircle.x < vecBall.x) && (vecCircle.z == vecBall.z))
            //{
            //    Debug.Log("2");
            //    return new Vector3(vecCircle.x + moveX, vecCircle.y, vecCircle.z);
            //}
            //else if ((vecCircle.z > vecBall.z) && (vecCircle.x == vecBall.x))
            //{
            //    Debug.Log("3");
            //    return new Vector3(vecCircle.x, vecCircle.y, vecCircle.z - moveZ);
            //}
            //else if ((vecCircle.z < vecBall.z) && (vecCircle.x == vecBall.x))
            //{
            //    Debug.Log("4");
            //    return new Vector3(vecCircle.x, vecCircle.y, vecCircle.z + moveZ);
            //}
            //else if ((vecCircle.x > vecBall.x) && (vecCircle.z > vecBall.z))
            //{
            //    Debug.Log("5");
            //    return new Vector3(vecCircle.x - moveX / 3, vecCircle.y, vecCircle.z - moveZ / 3);
            //}
            //else if ((vecCircle.x < vecBall.x) && (vecCircle.z > vecBall.z))
            //{
            //    Debug.Log("6");
            //    return new Vector3(vecCircle.x + moveX / 3, vecCircle.y, vecCircle.z - moveZ / 3);
            //}
            //else if ((vecCircle.x < vecBall.x) && (vecCircle.z < vecBall.z))
            //{
            //    Debug.Log("7");
            //    return new Vector3(vecCircle.x + moveX / 3, vecCircle.y, vecCircle.z + moveZ / 3);
            //}
            //else if ((vecCircle.x > vecBall.x) && (vecCircle.z < vecBall.z))
            //{
            //    Debug.Log("8");
            //    return new Vector3(vecCircle.x - moveX / 3, vecCircle.y, vecCircle.z + moveZ / 3);
            //}
            //else
            //{
            //    return new Vector3(vecCircle.x, vecCircle.y, vecCircle.z);
            //}
        }


        public List<float> distances;

        Vector3 SetNewDistance()
        {
            for (int i = 0; i < CircleObj.transform.childCount; i++)
            {
                GameObject newObj = CircleObj.transform.GetChild(i).gameObject;
                float dis = Vector3.Distance(MainBall.transform.position, newObj.transform.position);
                distances.Add(dis);
            }

            float minValue = distances[0];
            int minIndex = 0;

            for (int i = 0; i < distances.Count; i++)
            {
                if (distances[i] < minValue)
                {
                    minValue = distances[i];
                    minIndex = i;
                }
            }
            Debug.Log(" minIndex = " + minIndex);

            //Debug.Log( CircleObj.transform.GetChild(minIndex).name);
            distances.Clear(); // F+
            return CircleObj.transform.GetChild(minIndex).transform.position;
        }
        
    }
}
