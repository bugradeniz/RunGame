using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelController : MonoBehaviour
{
    public GameObject[] levelButtons;
    public int lastLevel;
    public Sprite lockedLevelSprite;
    void Start()
    {
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            lastLevel = PlayerPrefs.GetInt("LastLevel");

        }
        else
        {
            lastLevel = 1;

        }
        setLevelButtons();
    }
    void setLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < lastLevel)
            {
                int index = i + 1;
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = "" + (index);
                levelButtons[i].GetComponent<Button>().onClick.AddListener(delegate { selectedLevelLoader(index); });
            }
            else
            {

                levelButtons[i].GetComponent<Image>().sprite = lockedLevelSprite;
                levelButtons[i].GetComponent<Button>().interactable = false;
                levelButtons[i].GetComponentInChildren<Text>().text = "";
            }
        }

    }

    public void selectedLevelLoader(int index)
    {
        SceneManager.LoadScene(index+4);
    }

    public void back()
    {
        SceneManager.LoadScene(0);
    }
}
