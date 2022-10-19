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
        if (other.tag == "Addition" || other.tag == "Subtraction" || other.tag == "Multiplication" || other.tag == "Division")
        {
            Debug.Log("on trigger islem e girildi");
            gameController.cloneManager(int.Parse(other.name), other.tag, other.transform);
        }


    }
    private void OnTriggerStay(Collider other)
    {
        // pervane etkisini yaratan sola ve saga itme alanlarini algilayan ifler
        if (other.tag == "Pusher")
        {
            if (other.name == "LeftPusher")
            {
                Debug.Log("on trigger pusher  a girildi");
                transform.Translate(Vector3.left * 0f * Time.deltaTime);
            }
            else if(other.name == "RightPusher")
            {

                Debug.Log("on trigger pusher  a girildi");
                transform.Translate(Vector3.right * 0f * Time.deltaTime);
            }
        }
    }
}
