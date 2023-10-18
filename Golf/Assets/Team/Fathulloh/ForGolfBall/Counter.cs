using TMPro;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class Counter : MonoBehaviour
    {
        public TMP_Text CurrentText;
        [SerializeField] private int currentScore;

        void Start()
        {

        }

        public void IncrementUIScore()
        {
            currentScore++;
            CurrentText.text = currentScore.ToString();
            Debug.Log("currentScore = " + currentScore);
        }

    }
}
