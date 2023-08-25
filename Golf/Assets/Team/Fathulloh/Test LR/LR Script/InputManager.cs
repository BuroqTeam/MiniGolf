using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer2
{
    public class InputManager : MonoBehaviour
    {
        public GameObject MainBall;
        [SerializeField] private LineRenderer _trajectoryLine;

        void Start()
        {

        }


        void Update()
        {
            ShowTrajectoryLine();
        }


        private void ShowTrajectoryLine()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition1);
                Vector3 mainBallPosition = MainBall.transform.position;
                _trajectoryLine.enabled = true;
                _trajectoryLine.positionCount = 2;
                _trajectoryLine.SetPosition(0, new Vector3(mousePosition.x, mainBallPosition.y, mousePosition.z));
                _trajectoryLine.SetPosition(1, mainBallPosition);
                //Debug.Log("mousePosition.z = " + mousePosition.z);
            }
        }


        Vector3 mousePosition1;

        private void OnMouseDown()
        {
            mousePosition1 = Input.mousePosition - GetMousePos();
        }


        private Vector3 GetMousePos()
        {
            return Camera.main.WorldToScreenPoint(transform.position);
        }


    }
}
