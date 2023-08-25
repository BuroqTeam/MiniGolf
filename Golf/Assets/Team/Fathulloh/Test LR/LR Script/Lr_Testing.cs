using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer
{
    public class Lr_Testing : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private LineController line;

                
        private void Start()
        {
            line.SetUpLine(points);
        }


        void Update()
        {

        }
    }
}
