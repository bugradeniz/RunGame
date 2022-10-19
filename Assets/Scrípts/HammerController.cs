using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{

    // isTrigger ozelligini aktif eden animasyon olayi
    public void triggerEnable()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger =true;

    
    }
    // isTrigger ozelligini deaktif eden animasyon olayi
    public void triggerDisable() {
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}
