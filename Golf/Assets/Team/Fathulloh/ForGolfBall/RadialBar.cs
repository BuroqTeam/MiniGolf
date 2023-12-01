using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GolfBall_Smooth
{
    /// <summary>
    /// Golf Ball ning qanday kuch bilan urilayotganini ko'rsatuvchi Bar obyektidagi script. LineRenderer 
    /// </summary>
    public class RadialBar : MonoBehaviour
    {
        [SerializeField] private Image _powerBar;
        [SerializeField] private LineDrawer _lineDrawer;
        [SerializeField] private BallDataSO _ballData;        
        [SerializeField] private BallMovement _ballMove;
        float _maxLength;


        void Awake()
        {
            _maxLength = _ballData.MaximalLengthOfLine/*_lineDrawer._maxLength*/;
        }


        void Update()
        {
            UpdatePowerRadialBar();
        }


        void UpdatePowerRadialBar()
        {
            ChangeRadialColor(_lineDrawer.CurrentColor);
            ChangeRadialText(Vector3.Distance(_lineDrawer._endPoint, _lineDrawer._startPoint));
           
        }


        void ChangeRadialColor(Color newColor)
        {
            _powerBar.color = newColor;
        }


        void ChangeRadialText(float distance)
        {
            distance *= 100;
            float percentageOfBar = (distance / _maxLength) / 100;
            _powerBar.fillAmount = percentageOfBar;

            if (percentageOfBar.Equals(0) || !_ballMove.IsBallClicked)
            {
                _powerBar.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
                _powerBar.fillAmount = 0;
            }
            else
            {
                _powerBar.transform.GetChild(0).GetComponent<TMP_Text>().text = Mathf.RoundToInt(10 * percentageOfBar).ToString();
            }
        }

    }
}
