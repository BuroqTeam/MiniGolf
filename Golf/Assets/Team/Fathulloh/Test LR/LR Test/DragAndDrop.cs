using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer
{
    /// <summary>
    /// Interfeyssiz drag and drop.
    /// </summary>
    public class DragAndDrop : MonoBehaviour
    {
        Vector3 mousePosition2;

        //[Header("Hozirgi pozitsiya")]
        //public Vector3 CurPos;

        Vector3 currentPosition;

        void Awake()
        {
            RetakePosition();
        }

        private Vector3 GetMousePos()
        {
            return Camera.main.WorldToScreenPoint(transform.position);
            //Debug.Log("ishlayabdi  222");
        }

        private void OnMouseDown()
        {
            mousePosition2 = Input.mousePosition - GetMousePos();
            Debug.Log("mousePosition" + mousePosition2);
        }

        private void OnMouseDrag()
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition2);
            transform.position = new Vector3(newPos.x, currentPosition.y, newPos.z);
            Debug.Log("33 33 33 33 33");
            Debug.Log(transform.position + "  //  " + newPos);
        }

        void RetakePosition()
        {
            currentPosition = gameObject.transform.position;
        }

    }
}
