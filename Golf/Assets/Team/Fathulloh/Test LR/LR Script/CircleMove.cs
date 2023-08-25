using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer2
{
    //Sichqoncha harakatiga ko'ra blue circleni qaytadan harakatga keltiruvchi script.
    public class CircleMove : MonoBehaviour
    {
        Vector3 mousePosition;
        Vector3 currentPosition;

        void Awake()
        {
            RetakePosition();
        }


        private Vector3 GetMousePos()
        {
            return Camera.main.WorldToScreenPoint(transform.position);
        }


        private void OnMouseDown()
        {
            mousePosition = Input.mousePosition - GetMousePos();
        }


        private void OnMouseDrag()
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            transform.position = new Vector3(newPos.x, currentPosition.y, newPos.z);

            //gameObject.transform.position = new Vector3(newPos.x, currentPosition.y, newPos.z);
            Debug.Log(gameObject.transform.position);
        }


        /// <summary>
        /// gameObyekt pozitsiyasini qaytadan olish.
        /// </summary>
        void RetakePosition()
        {
            currentPosition = gameObject.transform.position;
        }
    }
}
