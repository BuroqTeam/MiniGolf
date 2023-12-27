//using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Learn_OdinInspector
{
    public class SampleClass : MonoBehaviour
    {
        //[GUIColor(0.75f, 0.5f, 1)]
        public string Top;
        public string Middle;
        public string Bottom;

        [Header("Int Type")]
        public int value1 = 1;
        public int value2 = 2;
        public int value3 = 3;

        //[ShowInInspector]
        private string str = "unity attributes";
    }

    public enum MyEnum
    {
        zero,
        one, 
        two,
        three,
        four
    }
}
