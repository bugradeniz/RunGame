using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject exitPanel;

    void Start()
    {
        
    }

   
   public void loadScene(int index)
    {
        SceneManager.LoadScene(index);
        
    }
    public void playLevel() {
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"+4));



        }
        else
        {
            PlayerPrefs.SetInt("LastLevel",1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel")+4);
        }
    
    }
    public void exit()
    {
        exitPanel.SetActive(true);
    }
    public void exitAnswer(bool answer)
    {
        if (answer)
        {
            Application.Quit();
        }
        else
        {
            exitPanel.SetActive(false);
        }
    }
}
