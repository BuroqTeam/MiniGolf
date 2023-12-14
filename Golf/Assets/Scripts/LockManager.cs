using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockManager : MonoBehaviour
{
    public List<GameObject> LevelButtons;


    private void Awake()
    {
        //ButtonChange();
    }


    void ButtonChange()
    {
        for (int i = 1; i < LevelButtons.Count; i++)
        {
            int lev = PlayerPrefs.GetInt("Level" + i);

            //Debug.Log("Lev = " + lev + "  i = " + i);
            if (lev == 1)
            {
                LevelButtons[i].GetComponent<Button>().interactable = true;
                LevelButtons[i].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                //Debug.Log(i);
            }
        }

        //for (int i = 1; i < 8; i++)
        //{
        //    PlayerPrefs.SetInt("Level" + (i).ToString(), 0);
        //}
    }


    public void ActivateButtons(int number)
    {
        //for(int i = 0; i < LevelButtons.Count; i++)
        //{
        //    if (number == i)
        //    {
        //        LevelButtons[i].GetComponent<Button>().interactable = true;
        //        LevelButtons[i].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
        //    }
        //    else if (number == LevelButtons.Count && number > i) 
        //    {
        //        LevelButtons[LevelButtons.Count - 1].GetComponent<Button>().interactable = true;
        //        LevelButtons[LevelButtons.Count - 1].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
        //    }
        //    else if(number == LevelButtons.Count && )
        //}

        Debug.Log(number);
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            if (i == number)
            {
                LevelButtons[i].GetComponent<Button>().interactable = true;
                LevelButtons[i].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                //Debug.Log(i);
                break;
            }
            else if (i == LevelButtons.Count - 1 && number == LevelButtons.Count)
            {
                LevelButtons[LevelButtons.Count - 1].GetComponent<Button>().interactable = true;
                LevelButtons[LevelButtons.Count - 1].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                //Debug.Log("Last");
            }
            else if (number <= LevelButtons.Count)
            {
                LevelButtons[i].GetComponent<Button>().interactable = false;
                LevelButtons[i].transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                //Debug.Log("i = " + i);
            }
        }

    }

}
