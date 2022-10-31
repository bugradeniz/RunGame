using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundController : MonoBehaviour
{
    public static GameObject instance;
    public AudioSource menuSound;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
