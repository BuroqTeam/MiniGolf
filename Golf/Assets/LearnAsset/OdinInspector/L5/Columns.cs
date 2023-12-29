using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Learn_OdinInspector
{
    public class Columns : MonoBehaviour
    {
        [Header("Group Attribute")]
        [Space]
        public string VerticalGroup = "Vertical Group";

        [HorizontalGroup("HorizontalBase", LabelWidth = 40)]

        [VerticalGroup("HorizontalBase/Column 1")] // VerticalBase is base group name, Column1 is column  name.
        public string a;
        [VerticalGroup("HorizontalBase/Column 1")] // VerticalBase is base group name, Column1 is column  name.
        public string b;

        [VerticalGroup("HorizontalBase/Column 2")]
        public string c;
        [VerticalGroup("HorizontalBase/Column 2")]
        public string d;



        [Space]
        public string BoxGroup = "Box Group";

        [HorizontalGroup("BoxBase", LabelWidth = 40)]

        [BoxGroup("BoxBase/Box 1")]   // BoxBase is base group name, Column1 is column  name.
        public string e;
        [BoxGroup("BoxBase/Box 1")]   // BoxBase is base group name, Column1 is column  name.
        public string f;

        [BoxGroup("BoxBase/Box 2")]
        public string g;
        [BoxGroup("BoxBase/Box 2")]
        public string h;



        [Space]
        public string TabGroup = "Tab Group";

        [TabGroup("Tab Group 1", "Tab 1")]
        public string i;
        [TabGroup("Tab Group 1", "Tab 1")]
        public string j;

        [TabGroup("Tab Group 1", "Tab 2")]
        public string k;
        [TabGroup("Tab Group 1", "Tab 2")]
        public string l;

        [BoxGroup("Tab Group 1/Tab 2/Sub Box Group")]
        public string m;
        [BoxGroup("Tab Group 1/Tab 2/Sub Box Group")]
        public string n;



        /*
        public SomeStruct DefaultStruct;
    
        [Serializable]
        public struct SomeStruct
        {
            public int One;
            public int Two;
            public int Three;
        }

        */


    }
}
