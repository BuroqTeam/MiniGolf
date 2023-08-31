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
        
        public GameObject CircleForMouse;
        public LineRenderer MyLineRenderer;
        public LineRenderer WhiteArrowWay;

        public InputManager Inputmanager;

        private void Awake()
        {
            CircleForMouse.transform.position = gameObject.transform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log(" + " + gameObject.transform.position);
            StartCoroutine(CirclePositionSet());
            ActivateMouseLine(true);
        }


        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePos);

            float distanceObj = Vector3.Distance(gameObject.transform.position, new Vector3(newPos.x, transform.position.y, newPos.z)); //F++
            if (distanceObj < Inputmanager.redLineLength)
            {
                CircleForMouse.transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
            }

            Inputmanager.ShowTrajectoryLine();
            //Debug.Log("=");
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            //mousePosition = Input.mousePosition - GetMousePos();
            ActivateMouseLine(false);
            //Debug.Log("-");
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
            WhiteArrowWay.enabled = _isTrue;
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
            //Debug.Log("Ishladi IE");
            yield return new WaitForSeconds(0.25f);
            Inputmanager.ShowTrajectoryLine();
        }


        //private void OnMouseDrag()
        //{
        //    Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePos);
        //    transform.position = new Vector3(newPos.x, /*currentPosition.y*/ 1, newPos.z);
        //    Debug.Log(gameObject.transform.position);
        //}
    }
}
