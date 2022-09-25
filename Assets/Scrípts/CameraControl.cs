using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;


    void Start()
    {
        // kameranin sahnedeki konumu takip mesafesi olarak ayarlandi
        target_offset = transform.position - target.position;

    }




    private void LateUpdate()
    {
        // kameranin karakteri takip etmesi saglandi
        transform.position = Vector3.Lerp(transform.position , target.position + target_offset, .1f);

    }
}
