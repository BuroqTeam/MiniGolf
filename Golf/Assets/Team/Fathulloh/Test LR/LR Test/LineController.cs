using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer
{
    public class LineController : MonoBehaviour
    {
        public LineRenderer Lrenderer;
        private Transform[] points;


        private void Awake()
        {
            Lrenderer = GetComponent<LineRenderer>();
        }


        public void SetUpLine(Transform[] points)
        {
            Lrenderer.positionCount = points.Length;
            this.points = points;
        }


        void Update()
        {
            for (int i = 0; i < points.Length; i++)
            {
                Lrenderer.SetPosition(i, points[i].position);
            }

            //ShowTrajectoryLine(); //
        }

        //[Header("Main ball")]
        //public GameObject _activeDefender;

        //private void ShowTrajectoryLine()
        //{
        //    Debug.Log(0);
        //    if(Input.GetMouseButton(0))
        //    {
        //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        Vector3 defenderPos = _activeDefender.transform.position;
        //        Lrenderer.enabled = true;
        //        Lrenderer.positionCount = 2;
        //        Lrenderer.SetPosition(0, new Vector3(defenderPos.x, defenderPos.y, -1f));
        //        Lrenderer.SetPosition(1, mousePos);
        //        Debug.Log(1);
        //    }
        //}

    }
}
