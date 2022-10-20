using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    // Splash efekti olustuktan 5 sn sonra deaktif ediliyor.
    IEnumerator disable()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        StartCoroutine(disable());
    }
}