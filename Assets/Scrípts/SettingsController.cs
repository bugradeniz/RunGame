using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;
using TMPro;
public class SettingsController : MonoBehaviour
{
    Pref pf = new Pref();
    public Slider[] sliders;
    public AudioSource buttonSound;
    public TextController[] settingsTexts;
    public Button leftButton;
    public Button rightButton;
    int languageIndex;
    List<string> languages=new List<string> {"EN","TR"};

    public delegate void UpdateSound();
    public static event UpdateSound updateMainMenuSound;
    void Start()
    {
        languageIndex = pf.getI("LanguageIndex");

        buttonSound.volume = pf.getF("FXSound");
        sliders[0].value = pf.getF("MenuSound");
        sliders[1].value = pf.getF("FXSound");
        sliders[2].value = pf.getF("GameSound");
    }
    public void menuBar()
    {
        pf.setF("MenuSound", sliders[0].value);
        if (updateMainMenuSound !=null)
        {
            updateMainMenuSound();
        }


    }
    public void fxBar()
    {
        pf.setF("FXSound", sliders[1].value);
        buttonSound.volume = sliders[1].value;
    }
    public void gameBar()
    {
        pf.setF("GameSound", sliders[2].value);
    }

    public void backButton()
    {

        buttonSound.Play();
        SceneManager.LoadScene(0);

    }
    public void LeftButton()
    {
        if (languageIndex==0)
        {
            languageIndex = languages.Count-1;
        }
        else
        {
            languageIndex--;
        }
        I18n.changeLanguage(languages[languageIndex]);
        foreach (var text in settingsTexts)
        {
            text.setText();
        }
        buttonSound.Play();

        // devam edecak...

    }
    public void RightButton()
    {

        if (languageIndex == languages.Count-1)
        {
            languageIndex =  0;
        }
        else
        {
            languageIndex++;
        }
        I18n.changeLanguage(languages[languageIndex]);
        foreach (var text in settingsTexts)
        {
            text.setText();
        }
        buttonSound.Play();


        // devam edecak...

    }
}
