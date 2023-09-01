using UnityEngine;

namespace Golf_LineRenderer2
{
    /// <summary>
    /// LineRendererlarga positionlarni kirituvchi asosiy script. 
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public GameObject MainBall;
        public GameObject CircleObj;  //F+
        public GameObject FrontArrow; 

        [SerializeField] private LineRenderer _trajectoryLine;
        public LineRenderer WhiteArrowWay;

        Color greenColor = new(0.27f, 0.85f, 0.2f);
        Color yellowColor = new(1, 1, 0);
        Color redColor = new(0.83f, 0.16f, 0.05f);

        readonly float greenLineLength = 4.0f;
        readonly float yellowLineLength = 7.0f;
        [HideInInspector] public readonly float redLineLength = 10.0f;

        readonly float distanceArrowLine = 0.6f;
        readonly float distanceColoredLine = 0.34f;


        public void ShowTrajectoryLine()
        {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MainBall.GetComponent<DragDrop>().MousePos);
            Vector3 mainBallPos = MainBall.transform.position;
            _trajectoryLine.enabled = true;
            _trajectoryLine.positionCount = 2;

            float lineLength = Vector3.Distance(CircleObj.transform.position, mainBallPos);   //F+
            if (lineLength < redLineLength)
            {
                Vector3 currentCirclePos = CircleObj.transform.position;
                if (lineLength < greenLineLength)
                {
                    DrawArrowWay(currentCirclePos, mainBallPos, greenColor);
                    FrontArrow.GetComponent<WhiteArrowPointer>().WhiteArrowSwitcher(2);
                }
                else if (lineLength < yellowLineLength)
                {
                    DrawArrowWay(currentCirclePos, mainBallPos, yellowColor);
                    FrontArrow.GetComponent<WhiteArrowPointer>().WhiteArrowSwitcher(4);
                }
                else if (lineLength < redLineLength)
                {
                    DrawArrowWay(currentCirclePos, mainBallPos, redColor);
                    FrontArrow.GetComponent<WhiteArrowPointer>().WhiteArrowSwitcher(6);
                }

                //_trajectoryLine.SetPosition(0, CircleObj.transform.position);
                _trajectoryLine.SetPosition(0, FindPointOnLine(currentCirclePos, mainBallPos, distanceArrowLine)/*SetDistanceWithDots(CircleObj.transform.GetChild(1).gameObject)*/);
                _trajectoryLine.SetPosition(1, mainBallPos);
            }
            
        }        


        void DrawArrowWay(Vector3 vec1, Vector3 vec2, Color newColor)
        {
            WhiteArrowWay.SetColors(newColor, newColor);

            //Material newMat = new(WhiteArrowWay.material);   // materialni rangini o‘zgartirish ham juda yaxshi ideya edi. Biroq rangi o‘zgarmadi. 
            //newMat.color = newColor;
            //WhiteArrowWay.material = newMat;

            CircleObj.GetComponent<SpriteRenderer>().color = newColor;
            WhiteArrowWay.positionCount = 2;

            //WhiteArrowWay.SetPosition(0, vec1);
            WhiteArrowWay.SetPosition(0, FindPointOnLine(vec1, vec2, distanceColoredLine)/*SetDistanceWithDots(CircleObj.transform.GetChild(0).gameObject)*/);
            WhiteArrowWay.SetPosition(1, vec2);            
        }


        /// <summary>
        /// Ikkita nuqta berilgan. Birinchi va ikkinchi nuqtalar orasida joylashgan va birinchi nuqtadan x masofada joylashgan uchinchi nuqtani topish.
        /// </summary>
        /// <param name="point1">Birinchi nuqtaning kordinatasi</param>
        /// <param name="point2">Ikkinchi nuqtaning kordinatasi</param>
        /// <param name="distance">Birinchi nuqtadan maksimal masofa</param>
        /// <returns></returns>
        Vector3 FindPointOnLine(Vector3 point1, Vector3 point2, float distance)
        {
            float totalDistance = Vector3.Distance(point1, point2);
            float ratio = distance / totalDistance;

            float newX = point1.x + ratio * (point2.x - point1.x);
            float newY = point1.y + ratio * (point2.y - point1.y);
            float newZ = point1.z + ratio * (point2.z - point1.z);

            return new Vector3(newX, newY, newZ);
        }



        //[HideInInspector] public List<float> Distances;
        //[HideInInspector] public List<float> DistancesForArrow;

        /// <summary>
        /// MainSpherani Circleni ichidagi eng yaqin childi bilan bog‘lab beruvchi funksiya.
        /// </summary>
        /// <param name="parenObj"></param>
        /// <returns></returns>
        //Vector3 SetDistanceWithDots(GameObject parenObj)
        //{
        //    for (int i = 0; i < parenObj.transform.childCount; i++)
        //    {
        //        GameObject newObj = parenObj.transform.GetChild(i).gameObject;
        //        float dis = Vector3.Distance(MainBall.transform.position, newObj.transform.position);
        //        Distances.Add(dis);
        //    }

        //    float minValue = Distances[0];
        //    int minIndex = 0;

        //    for (int i = 0; i < Distances.Count; i++)
        //    {
        //        if (Distances[i] < minValue)
        //        {
        //            minValue = Distances[i];
        //            minIndex = i;
        //        }
        //    }

        //    //Debug.Log(" minIndex = " + minIndex);
        //    Distances.Clear(); // F+
        //    return parenObj.transform.GetChild(minIndex).transform.position;
        //}       

    }
}
