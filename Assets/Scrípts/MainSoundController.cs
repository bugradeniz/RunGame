using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class MainSoundController : MonoBehaviour
{
    public static GameObject instance;
    public AudioSource menuSound;
    Pref pf = new Pref();


    private void OnEnable()
    {
        SettingsController.updateMainMenuSound += updateSound;
    }
    private void OnDisable()
    {
        SettingsController.updateMainMenuSound -= updateSound;
    }
    void Start()
    {




        

        if (instance==null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        menuSound.volume = pf.getF("MenuSound");



    }

    void updateSound()
    {
        menuSound.volume = pf.getF("MenuSound");
    }
}
