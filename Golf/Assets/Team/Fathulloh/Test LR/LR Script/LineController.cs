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
        }


    }
}
