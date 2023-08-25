using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf_LineRenderer
{
    /// <summary>
    /// Drag and Drop qilish uchun buyerda interfacelardan foydalanilgan.
    /// </summary>
    public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        Vector3 mousePosition;
        public Vector3 MovementPos;

        public Vector3 CurrentPos;


        //void Start()
        //{
        //    Debug.Log("Start");
        //}



        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("+");
        }


        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition /*- mousePosition*/);
            transform.position = new Vector3(pos.x, pos.y, pos.z);
            Debug.Log("=");
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            //mousePosition = Input.mousePosition - GetMousePos();
            Debug.Log("-");
        }


        //private Vector3 GetMousePos()
        //{
        //    return Camera.main.WorldToScreenPoint(transform.position);
        //}




    }
}
