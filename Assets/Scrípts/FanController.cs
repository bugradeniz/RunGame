using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public Animator animator;
    public float delay;
    public BoxCollider wind;

    //pervane animasyonu sonunda aktif olan, animasyonu ve ruzgar efektini durduran, belirli bir beklemeden sonra 
    //tekrar animasyonu ve ruzgar efektini devreye sokan animasyon olayi
    public void AnimationEnd()
    {
       
        animator.SetBool("start", false);
        wind.enabled = false;

        StartCoroutine(fanDelay());

    }

    IEnumerator fanDelay()
    {
        yield return new WaitForSeconds(delay);

        animator.SetBool("start", true);
        wind.enabled = true;


    }
}
