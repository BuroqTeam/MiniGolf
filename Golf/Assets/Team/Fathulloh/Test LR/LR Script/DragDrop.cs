using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf_LineRenderer2
{
    /// <summary>
    /// SpheraMain obyektiga qo‘shilgan script. Bu orqali circle ni harakatga keltirish mumkin.
    /// Drag  Drop qilish uchun buyerda interfacelardan foydalanilgan.
    /// </summary>
    public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [HideInInspector] public Vector3 MousePos;
        
        public GameObject CircleForMouse;
        public LineRenderer MyLineRenderer;
        public LineRenderer WhiteArrowWay;

        public InputManager Inputmanager;
        public GameObject FrontArrow;


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
            Vector3 mainBallPos = gameObject.transform.position;
            float distanceObj = Vector3.Distance(mainBallPos, new Vector3(newPos.x, mainBallPos.y, newPos.y)); //F++
            
            //Debug.Log("newPos = " + newPos + " mainBallPos = " + mainBallPos);            
            if (distanceObj < Inputmanager.redLineLength)
            {
                CircleForMouse.transform.position = new Vector3(newPos.x, mainBallPos.y, newPos.y/*newPos.z*/);
                //Debug.Log("CircleForMouse.transform.position = " + CircleForMouse.transform.position);
            }
            else if (Vector3.Distance(mainBallPos, new Vector3(newPos.x + mainBallPos.x, mainBallPos.y, newPos.y + mainBallPos.z)) < Inputmanager.redLineLength)
            {
                //Debug.Log(new Vector3(newPos.x, mainBallPos.y, newPos.y));
                CircleForMouse.transform.position = new Vector3(mainBallPos.x + newPos.x, mainBallPos.y, mainBallPos.z + newPos.y);
                //Debug.Log(new Vector3(newPos.x, newPos.y, newPos.z) + "   CircleForMouse.transform.position = " + CircleForMouse.transform.position);
            }
            //else
            //{   // Agar sichqoncha juda uzoqda bo‘lsa CIrcleni sichqonchaga eng yaqin bo‘lgan nuqtaga joylashtiradi.
            //    Vector3 newPosForCircle = FindPointOnLine(gameObject.transform.position, new Vector3(newPos.x, transform.position.y, newPos.z), Inputmanager.redLineLength);
            //    CircleForMouse.transform.position = newPosForCircle;
            //}

            FrontArrow.GetComponent<WhiteArrowPointer>().ArrowPointer();
            Inputmanager.ShowTrajectoryLine();
            //SomeDebugs();
            //Debug.Log("=");
        }


        //void SomeDebugs()
        //{
        //    Debug.Log(" Input.mousePosition = " + Input.mousePosition);
        //    Debug.Log(" Camera.main.ScreenToWorldPoint(Input.mousePosition) = " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //    //Debug.Log(" newPos = " + newPos + " Input.mousePosition - MousePos = " + (Input.mousePosition - MousePos));
        //    //Debug.Log(" MousePos = " + MousePos + " Camera.main.WorldToScreenPoint(CircleForMouse.transform.position) = " + Camera.main.WorldToScreenPoint(CircleForMouse.transform.position));
        //}


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
            FrontArrow.SetActive(_isTrue);
        }


        private void OnMouseDown() // Sichqonchaning mos pozitsiyasini olib beradi.
        {
            //Debug.Log("OnMouseDown() ");
            MousePos = Input.mousePosition - GetMousePos();
        }


        private Vector3 GetMousePos() // Amalni bajaradi va Vector3 tipli qiymat qaytaradi.
        {
            //Debug.Log("GetMousePos() ");
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


        /// <summary>
        /// Ikkita nuqta berilgan. Birinchi va ikkinchi nuqtalar orasida joylashgan va birinchi nuqtadan x masofada joylashgan uchinchi nuqtani topish.
        /// </summary>
        /// <param name="point1">Birinchi nuqtaning kordinatasi</param>
        /// <param name="point2">Ikkinchi nuqtaning kordinatasi</param>
        /// <param name="distance">Birinchi nuqtadan maksimal masofa</param>
        /// <returns></returns>
        Vector3 FindPointOnLine(Vector3 point1, Vector3 point2, float distance)
        {
            float totalDistance = Vector3.Distance(point1, point2);
            float ratio = distance / totalDistance;

            float newX = point1.x + ratio * (point2.x - point1.x);
            float newY = point1.y + ratio * (point2.y - point1.y);
            float newZ = point1.z + ratio * (point2.z - point1.z);

            return new Vector3(newX, newY, newZ);
        }


        //private void OnMouseDrag()
        //{
        //    Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePos);
        //    transform.position = new Vector3(newPos.x, /*currentPosition.y*/ 1, newPos.z);
        //    Debug.Log(gameObject.transform.position);
        //}
    }
}
