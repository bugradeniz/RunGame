using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject target;
    public GameObject clonePrefab;
    public GameObject startPoint;
    public GameObject endPoint;
    void Start()
    {
        
    }

    void Update()
    {
        // tusa basildiginda klonlarin olusturulmasi saglandi.
        // ileride degistirilecek
        if (Input.GetKey(KeyCode.Mouse1)) {

            Instantiate(clonePrefab, startPoint.transform.position, Quaternion.identity);
        
        }
    }
}
