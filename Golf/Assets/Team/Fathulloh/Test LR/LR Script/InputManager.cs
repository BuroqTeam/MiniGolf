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

        Color greenColor = new(0.27f, 0.85f, 0.2f, 1);
        Color yellowColor = new(1, 1, 0, 1);
        Color redColor = new(0.83f, 0.16f, 0.05f, 1);

        [HideInInspector] public float greenLineLength = 0.11f;  // 4  0.28f
        [HideInInspector] public float yellowLineLength = 0.22f; // 7  0.52f
        [HideInInspector] public float redLineLength = 0.33f; // 9  0.72f

        readonly float distanceArrowLine = 0.035f; // 0.6f
        readonly float distanceColoredLine = 0.025f; // 0.34f


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
                    //FrontArrow.GetComponent<WhiteArrowPointer>().WhiteArrowSwitcher(2);
                }
                else if (lineLength < yellowLineLength)
                {
                    DrawArrowWay(currentCirclePos, mainBallPos, yellowColor);
                }
                else if (lineLength < redLineLength)
                {
                    DrawArrowWay(currentCirclePos, mainBallPos, redColor);;
                }

                FrontArrow.GetComponent<WhiteArrowPointer>().CheckLineColor(lineLength);

                //_trajectoryLine.SetPosition(0, CircleObj.transform.position);
                _trajectoryLine.SetPosition(0, FindPointOnLine(currentCirclePos, mainBallPos, distanceArrowLine, true)/*SetDistanceWithDots(CircleObj.transform.GetChild(1).gameObject)*/);
                _trajectoryLine.SetPosition(1, new Vector3(mainBallPos.x, mainBallPos.y + 0.001f, mainBallPos.z));
            }
            
        }        


        void DrawArrowWay(Vector3 vec1, Vector3 vec2, Color newColor)
        {
            //WhiteArrowWay.SetColors(newColor, newColor);

            Material newMat = (WhiteArrowWay.material);   // materialni rangini o‘zgartirish ham juda yaxshi ideya edi. Biroq rangi o‘zgarmadi. 
            newMat.color = newColor;
            WhiteArrowWay.material = newMat;

            CircleObj.GetComponent<SpriteRenderer>().color = newColor;
            WhiteArrowWay.positionCount = 2;

            //WhiteArrowWay.SetPosition(0, vec1);
            WhiteArrowWay.SetPosition(0, FindPointOnLine(vec1, vec2, distanceColoredLine, false)/*SetDistanceWithDots(CircleObj.transform.GetChild(0).gameObject)*/);
            WhiteArrowWay.SetPosition(1, vec2);            
        }


        /// <summary>
        /// Ikkita nuqta berilgan. Birinchi va ikkinchi nuqtalar orasida joylashgan va birinchi nuqtadan x masofada joylashgan uchinchi nuqtani topish.
        /// </summary>
        /// <param name="point1">Birinchi nuqtaning kordinatasi</param>
        /// <param name="point2">Ikkinchi nuqtaning kordinatasi</param>
        /// <param name="distance">Birinchi nuqtadan maksimal masofa</param>
        /// <returns></returns>
        Vector3 FindPointOnLine(Vector3 point1, Vector3 point2, float distance, bool _isArrowTrue)
        {
            float totalDistance = Vector3.Distance(point1, point2);
            float ratio = distance / totalDistance;

            float newX = point1.x + ratio * (point2.x - point1.x);
            float newY = point1.y + ratio * (point2.y - point1.y);
            float newZ = point1.z + ratio * (point2.z - point1.z);

            if (_isArrowTrue)
            {
                return new Vector3(newX, newY + 0.001f, newZ);
            }
            else
            {
                return new Vector3(newX, newY, newZ);
            }
            
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
