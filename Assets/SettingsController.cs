using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;
public class SettingsController : MonoBehaviour
{
    Pref pf = new Pref();
    public Slider[] sliders;
    public AudioSource buttonSound;
    void Start()
    {
        buttonSound.volume=pf.getF("FXSound");
        sliders[0].value = pf.getF("MenuSound");
        sliders[1].value = pf.getF("FXSound");
        sliders[2].value = pf.getF("GameSound");
    }
    public void menuBar()
    {
        pf.setF("MenuSound", sliders[0].value);

    }
    public void fxBar()
    {
        pf.setF("FXSound", sliders[1].value);
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
    public void leftButton()
    {
        buttonSound.Play();

        // devam edecak...

    }
    public void rightButton()
    {
        buttonSound.Play();


        // devam edecak...

    }
}
