using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf_LineRenderer2
{
    /// <summary>
    /// Drag  Drop qilish uchun buyerda interfacelardan foydalanilgan.
    /// </summary>
    public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Vector3 MousePos;
        //public Vector3 CurrentPos;
        
        public GameObject CircleForMouse;
        public LineRenderer MyLineRenderer;

        public InputManager Inputmanager;


        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("+");
            CirclePositionSet();
            ActivateMouseLine(true);
        }


        public void OnDrag(PointerEventData eventData)
        {
            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition /*- mousePosition*/);
            //transform.position = new Vector3(pos.x, pos.y, pos.z);\
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePos);
            CircleForMouse.transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);

            Inputmanager.ShowTrajectoryLine();
            //Debug.Log("=");
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            //mousePosition = Input.mousePosition - GetMousePos();
            ActivateMouseLine(false);
            Debug.Log("-" + CircleForMouse.transform.position);
            StartCoroutine(CirclePositionSet());
        }


        /// <summary>
        /// Obyektlarni active qilish uchun
        /// </summary>
        /// <param name="_isTrue"></param>
        void ActivateMouseLine(bool _isTrue)
        {
            CircleForMouse.SetActive(_isTrue);
            MyLineRenderer.enabled = _isTrue;
        }


        private void OnMouseDown()
        {
            MousePos = Input.mousePosition - GetMousePos();
        }


        private Vector3 GetMousePos()
        {
            return Camera.main.WorldToScreenPoint(CircleForMouse.transform.position);
        }


        /// <summary>
        /// Aylanani Golf koptogi turgan joydan boshlab beradi.
        /// </summary>
        IEnumerator CirclePositionSet()
        {
            CircleForMouse.transform.position = gameObject.transform.position;
            yield return new WaitForSeconds(0.25f);
            Inputmanager.ShowTrajectoryLine();
        }


        //IEnumerator

        //private void OnMouseDrag()
        //{
        //    Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePos);
        //    transform.position = new Vector3(newPos.x, /*currentPosition.y*/ 1, newPos.z);
        //    Debug.Log(gameObject.transform.position);
        //}
    }
}
