using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf_LineRenderer
{
    public class DragAndDrop : MonoBehaviour
    {
        Vector3 mousePosition;

        [Header("Hozirgi pozitsiya")]
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
