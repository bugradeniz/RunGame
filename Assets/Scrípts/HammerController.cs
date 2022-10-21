using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{

    // isTrigger ozelligini aktif eden animasyon olayi
    public void triggerEnable()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    // isTrigger ozelligini deaktif eden animasyon olayi
    public void triggerDisable() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
