using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GolfBall_Smooth
{
    public class LockControl : MonoBehaviour
    {
        public List<GameObject> LevelButtons;


        private void Awake()
        {
            ButtonChange();
        }


        void ButtonChange()
        {
            for (int i = 1; i < LevelButtons.Count; i++)
            {
                int lev = PlayerPrefs.GetInt("Solid" + i);

                //Debug.Log("Lev = " + lev);
                if (lev == 1)
                {
                    LevelButtons[i].GetComponent<Button>().interactable = true;
                    LevelButtons[i].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                }
            }

            //for (int i = 1; i < 8; i++)
            //{
            //    PlayerPrefs.SetInt("Level" + (i).ToString(), 0);
            //}
        }
    }
}
