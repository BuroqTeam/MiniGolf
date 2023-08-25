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
        Vector3 mousePosition;

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
        }

        private void OnMouseDown()
        {
            mousePosition = Input.mousePosition - GetMousePos();
        }

        private void OnMouseDrag()
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            transform.position = new Vector3(newPos.x, currentPosition.y, newPos.z);
        }

        void RetakePosition()
        {
            currentPosition = gameObject.transform.position;
        }

    }
}
