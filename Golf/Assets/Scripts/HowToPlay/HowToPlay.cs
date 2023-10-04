using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace MiniGolf
{
    /// <summary>
    /// Qanday o'ynashni tushuntiradigan animatsiya uchun script. //F
    /// </summary>
    public class HowToPlay : MonoBehaviour
    {
        [SerializeField] private RectMask2D rectMask2D;
        [SerializeField] private Image lineImage;
        [SerializeField] private GameObject handCursor;
        [SerializeField] private GameObject ball;
        [SerializeField] private GameObject circleGap;
        [SerializeField] private GameObject circleForHand;

        Vector3 ballPos;
        Vector3 _rotation = new Vector3 (0, 0, 60);
        [HideInInspector] public bool _isRotating = false;
        private bool _isObjectActive = false;
        Vector4 maskPadding;
        
        Color greenColor = new Color(0.25f, 1, 0.27f);
        Color yellowColor = new Color(1, 0.97f, 0.17f);
        Color redColor = new Color(1, 0.17f, 0.05f);

        Image circleImage;


        void Start()
        {
            ballPos = ball.transform.position;
            circleImage = circleForHand.GetComponent<Image>();
            _isRotating = true;
            maskPadding = rectMask2D.padding;            
        }

        private void OnEnable()
        {
            _isObjectActive = true;
            StartCoroutine(BallAnimation());
            //Debug.Log("OnEnable");
        }

        private void OnDisable()
        {
            _isObjectActive = false;
            //Debug.Log("OnDisable");

            _isRotating = true;
            circleGap.SetActive(true);
            rectMask2D.padding = maskPadding;
            circleImage.color = greenColor;
            lineImage.color = greenColor;

            handCursor.SetActive(false);
            circleForHand.SetActive(false);
        }

        void Update()
        {
            if (_isRotating && _isObjectActive)
            {
                circleGap.transform.Rotate(_rotation * Time.deltaTime);
            }                      
        }


        IEnumerator BallAnimation()
        {
            yield return new WaitForSeconds(1.5f);
            _isRotating = false;
            circleGap.SetActive(false);

            handCursor.transform.position = new Vector3(ballPos.x + 25, ballPos.y - 45, ballPos.z);
            circleForHand.transform.position = new Vector3(ballPos.x, ballPos.y - 25, ballPos.z);

            handCursor.SetActive(true);
            //circleForHand.SetActive(true);
            yield return new WaitForSeconds(0.1f);

            handCursor.GetComponent<RectTransform>().DOAnchorPosY(-255, 1.15f);
            circleForHand.GetComponent<RectTransform>().DOAnchorPosY(-215, 1.15f);
            yield return new WaitForSeconds(.02f);
            circleForHand.SetActive(true);

            while (rectMask2D.padding.y > 0)
            {
                rectMask2D.padding = new Vector4(0, rectMask2D.padding.y - 10, 0, 0);
                

                if (rectMask2D.padding.y < 60)
                {
                    yield return new WaitForSeconds(.06f);
                    lineImage.color = redColor;
                    circleImage.color = redColor;
                }
                else if (rectMask2D.padding.y < 120)
                {
                    yield return new WaitForSeconds(0.05f);
                    lineImage.color = yellowColor;
                    circleImage.color = yellowColor;
                }
                else if (rectMask2D.padding.y > 120)
                {
                    yield return new WaitForSeconds(0.03f);
                    lineImage.color = greenColor;
                    circleImage.color = greenColor;
                }

                if (rectMask2D.padding.y <= 1)
                {
                    //Debug.Log("While ishladi");
                    break;
                }
            }

            yield return new WaitForSeconds(2.0f);

            _isRotating = true;
            circleGap.SetActive(true);
            rectMask2D.padding = maskPadding;
            circleImage.color = greenColor;
            lineImage.color = greenColor;

            handCursor.SetActive(false);
            circleForHand.SetActive(false);

            StartCoroutine(BallAnimation());
        }

        
    }
}
