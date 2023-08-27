using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer2
{
    public class InputManager : MonoBehaviour
    {
        //public static InputManager Instance;
        public GameObject MainBall;
        [SerializeField] private LineRenderer _trajectoryLine;
        public GameObject CircleObj;  //F+


        void Update()
        {
            //ShowTrajectoryLine();
        }


        public void ShowTrajectoryLine()
        {
            if (Input.GetMouseButton(0))
            {                
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MainBall.GetComponent<DragDrop>().MousePos /*CircleObj.GetComponent<CircleMove>().mousePos*/);
                Vector3 mainBallPos = MainBall.transform.position;
                _trajectoryLine.enabled = true;
                _trajectoryLine.positionCount = 2;

                //CircleObj.transform.position = new Vector3(mousePos.x, mainBallPos.y, mousePos.z); //F+

                _trajectoryLine.SetPosition(0, CircleObj.transform.position);
                _trajectoryLine.SetPosition(1, mainBallPos);;
            }
        }
        
    }
}
