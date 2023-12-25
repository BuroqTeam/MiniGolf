using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace GolfBall_Smooth
{
    /// <summary>
    /// Count how many hitted and catched how many diamonds.
    /// </summary>
    public class Counter : MonoBehaviour
    {
        public TMP_Text CurrentText;
        [SerializeField] private int currentScore;

        public TypeOfScore CurrentType;
        public BallDataSO BallDataSo;

        private void Update()
        {
            if (CurrentType == TypeOfScore.WithInUpdate)
            {
                WriteWayLength();
            }
        }


        public void WriteWayLength()
        {
            Vector3 vec1 = GManager.Instance.BallMove.InitialPosBeforeHit;
            Vector3 vec2 = GManager.Instance.BallMove.transform.position;

            float walkingLength = Mathf.RoundToInt(Vector3.Distance(vec1, vec2) / BallDataSo.SizeEachCell);
            if (vec2.x ==0 && vec2.z == 0)
            {
                CurrentText.text = "0m";
            }
            else
            {
                CurrentText.text = walkingLength.ToString() + "m";
            }
            //CurrentText.text = walkingLength.ToString() + "m";
            //Debug.Log("Walking length = " + walkingLength + " " + Vector3.Distance(vec1, vec2));
        }


        public void IncrementUIScore()
        {
            currentScore++;
            CurrentText.text = currentScore.ToString();
        }        

    }

    public enum TypeOfScore
    {
        WithOutUpdate,
        WithInUpdate
    }
}
