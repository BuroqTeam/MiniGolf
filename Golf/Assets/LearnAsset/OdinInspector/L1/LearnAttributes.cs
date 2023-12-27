using Sirenix.OdinInspector;
using UnityEngine;

namespace Learn_OdinInspector
{
    public class LearnAttributes : MonoBehaviour
    {
        [ShowInInspector]  // Show static variable on Inspector 
        public static int Hundred = 100;
        [ShowInInspector]
        public static LearnAttributes LAttributes;


        void Start()
        {
            Debug.Log(this.GetType().ToString());
            Debug.Log(this.gameObject.GetComponent<MonoBehaviour>());
            LAttributes = this;
        }


        [HorizontalGroup("Buttons")]
        [Button(ButtonSizes.Large)]
        public static void Function3()
        {
            Debug.Log("Hundred = " + (Hundred));
        }


        [HorizontalGroup("Buttons")]  // Create button horizontal.
        [Button(ButtonSizes.Large)]  // Choose button size. 
        public static void Function4()
        {
            Debug.Log("Southand * 10 = " + (Hundred * 10));
        }


        [Button] // Create button
        public static void CustomMessage(string message)
        {
            Debug.Log(message + 2);
        }


        /// <summary>
        /// Odin assetining static Inspector panelini ochib beradi.
        /// </summary>
        [Button("Open Static Class Window"), GUIColor(0.4f, 0.8f, 1f)] // Button yasash va unga rang tanlash.
        [PropertyOrder(-10)]  // Metodning/Buttonning orderi
        private void OpenStaticClassWindow()
        {
            // This code open Inspector panel of Odin Asset. 
            Sirenix.OdinInspector.Editor.StaticInspectorWindow.InspectType(typeof(LearnAttributes));
        }



    }
}
