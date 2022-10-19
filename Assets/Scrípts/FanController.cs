using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public Animator animator;
    public float delay;
    public BoxCollider wind;
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
