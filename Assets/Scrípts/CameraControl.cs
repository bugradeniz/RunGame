using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;


    // Start is called before the first frame update
    void Start()
    {
        target_offset = transform.position - target.position;


    }




    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position , target.position + target_offset, .125f);

    }
}
