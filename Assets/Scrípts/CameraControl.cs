using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;
    public bool isEnding;
    public Transform endingPoint;


    void Start()
    {
        // kameranin sahnedeki karaktere gore olan konumu takip mesafesi olarak ayarlandi
        target_offset = transform.position - target.position;

    }




    private void FixedUpdate()
    {

        if (isEnding)
        {
            //eger oyun sonuna gelindiyse oyun sonu pozisyonuna gecmesi saglandi
            transform.position = Vector3.Lerp(transform.position, endingPoint.position, .01f);

        }
        else
        {
            // kameranin karakteri takip etmesi saglandi
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .1f);

        }

    }

    // sona gelindiginde bitis gorevi aktif ediliyor
    public void ending()
    {
        isEnding = true;
    }
}
