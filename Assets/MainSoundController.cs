using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class MainSoundController : MonoBehaviour
{
    public static GameObject instance;
    public AudioSource menuSound;
    Pref pf = new Pref();
    void Start()
    {




        DontDestroyOnLoad(gameObject);

        if (instance==null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }


        menuSound.volume = pf.getF("MenuSound");



    }

    // Update is called once per frame
    void Update()
    {
        menuSound.volume = pf.getF("MenuSound");
    }
}
