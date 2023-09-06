using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfMap_Test
{
    /// <summary>
    /// ScreenToWorldPoint, ScreenToViewportPoint va WorldToScreenPoint larni o‘rganish uchun qo‘llanildi.
    /// </summary>
    public class LineTest : MonoBehaviour
    {
        public Camera MainCamera;
        public GameObject Obj1;
        public GameObject Obj2;

        LineRenderer lineRenderer;

        public Vector3 InitialPos1;
        public Vector3 InitialPos2;

        public Vector3 WorldToScreen1;
        public Vector3 WorldToScreen2;

        public Vector3 ScreenToWorld1;
        public Vector3 ScreenToWorld2;


        void Start()
        {
            SetLinePos();
            //Ray rey = 2;
        }        
        

        void SetLinePos()
        {
            lineRenderer = GetComponent<LineRenderer>();

            InitialPos1 = Obj1.transform.position;
            InitialPos2 = Obj2.transform.position;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, InitialPos1);
            lineRenderer.SetPosition(1, InitialPos2);

            WorldToScreen1 = MainCamera.WorldToScreenPoint(InitialPos1);
            WorldToScreen2 = MainCamera.WorldToScreenPoint(InitialPos2);

            ScreenToWorld1 = MainCamera.ScreenToWorldPoint(WorldToScreen1);
            ScreenToWorld2 = MainCamera.ScreenToWorldPoint(WorldToScreen2);
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(Input.mousePosition);
                Debug.Log("MainCamera.ScreenToViewportPoint(Input.mousePosition) = " + MainCamera.ScreenToViewportPoint(Input.mousePosition));
                Debug.Log("MainCamera.ScreenToWorldPoint(Input.mousePosition) = " + MainCamera.ScreenToWorldPoint(Input.mousePosition));

                //startPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);
            }
        }

    }
}
