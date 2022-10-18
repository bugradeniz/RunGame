using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public GameController gameController;
    void Start()
    {
    }

    private void FixedUpdate()
    {
        // karaktere ileri dogru bir hiz verir
        transform.Translate(Vector3.forward * .5f * Time.deltaTime);



    }
    private void Update()
    {
        // Mouse tiklanip suruklendiginde o yonde kaydirilir.
        // Ilerde ekrana dokunma ve surukleme olarak degistirilecek
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z), .01f);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), .01f);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Karakterin islem kapilarindan gectigini algilayan if
        if (other.tag=="Addition" || other.tag == "Subtraction" || other.tag == "Multiplication" || other.tag == "Division" )
        {
            Debug.Log("on trigger a girildi");
            gameController.cloneManager(int.Parse(other.name), other.tag, other.transform); 
        }
    }
}
