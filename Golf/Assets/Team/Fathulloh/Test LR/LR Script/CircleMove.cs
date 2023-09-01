using UnityEngine;

namespace Golf_LineRenderer2
{
    //Sichqoncha harakatiga ko'ra blue circleni qaytadan harakatga keltiruvchi script.
    public class CircleMove : MonoBehaviour
    {
        public Transform center; // O nuqta, masalan: O(0, 0, 0)
        public float radius = 0.525f; // R radius
        public int numPoints = 12; // 12 nuqta


        void Start()
        {
            //ChoosePointFromCircle();
        }


        /// <summary>
        /// Aylanada yotgan numPoints ta nuqtani tanlab beradi.
        /// </summary>
        void ChoosePointFromCircle()
        {
            Debug.Log(center.position);

            for (int i = 0; i < numPoints; i++)
            {
                float angle = i * 360f / numPoints;
                float radians = angle * Mathf.Deg2Rad;

                float x = center.localPosition.x + radius * Mathf.Cos(radians);
                float y = center.localPosition.y + radius * Mathf.Sin(radians);
                float z = center.localPosition.z;

                Vector3 point = new Vector3(x, y, z);
                Debug.Log("Nuqta " + i + " : " + point);
            }
        }


        //public InputManager Inputmanager;
        //[HideInInspector] public Vector3 mousePos;
        //Vector3 currentPos;

        //void Awake()
        //{
        //    RetakePosition();
        //}


        //private Vector3 GetMousePos()
        //{
        //    return Camera.main.WorldToScreenPoint(transform.position);
        //}


        //private void OnMouseDown()
        //{
        //    mousePos = Input.mousePosition - GetMousePos();

        //    Debug.Log(gameObject.transform.position);
        //}


        //private void OnMouseDrag()
        //{
        //    //Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos);
        //    //transform.position = new Vector3(newPos.x, currentPos.y, newPos.z);

        //    Debug.Log(gameObject.transform.position);
        //    Inputmanager.ShowTrajectoryLine();
        //}


        ///// <summary>
        ///// gameObyekt pozitsiyasini qaytadan olish.
        ///// </summary>
        //void RetakePosition()
        //{
        //    currentPos = gameObject.transform.position;
        //}
    }
}
