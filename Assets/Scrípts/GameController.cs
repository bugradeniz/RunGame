using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject target;
    public GameObject clonePrefab;
    public GameObject startPoint;
    public GameObject endPoint;
    public List<GameObject> clonePool;
    void Start()
    {

    }

    void Update()
    {
        // tusa basildiginda klonlarin olusturulmasi saglandi.
        // ileride degistirilecek
        // klone havuzundaki aktif olmayan ilk klonu konumlandirip aktif hale getirilmesi saglandi instantiate komutu performans icin kullanilmayacak
        if (Input.GetKey(KeyCode.Mouse1))
        {
            foreach (GameObject clone in clonePool)
            {
                if (!clone.activeInHierarchy)
                {
                    
                    clone.transform.position = startPoint.transform.position;
                    clone.SetActive(true);
                    break;
                }
            }

            // Instantiate(clonePrefab, startPoint.transform.position, Quaternion.identity);

        }
    }
}
