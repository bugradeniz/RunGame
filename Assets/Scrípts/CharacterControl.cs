using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public GameController gameController;
    public bool isEnding;
    public Transform endigPoint;
    public bool isRuning;
    public Animator animator;

    
    void Start()
    {

        // baslangicta ileri kosma ve sag sola kontrol devreye giriyor.
        // bitis gorevi deaktif tutuluyor.

        isRuning = true;
        isEnding = false;
    }

    
    // oyun sonuna gelindiginde degistirilen paramatereler.
    // Ileri kosma ve sag sola kontrol duruyor.
    // Belirli bir niktaya ilerleme basliyor.
    public void ending()
    {
        isRuning = false;
        isEnding = true;
    }

    //Animasyonu ve ilerlemeyi baslatan fonksiyon
    public void startRuning()
    {
        isRuning = true;
        animator.SetBool("run", true);
    }

    //Animasyonu ve ilerlemeyi durduran fonksiyon
    public void stopRuning()
    {
        isRuning = false;
        animator.SetBool("run", false);

    }

    private void Update()
    {
        if (isRuning)
        {
            // karaktere ileri dogru bir hiz verir
            transform.Translate(Vector3.forward * 1f * Time.deltaTime);


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
        if (isEnding)
        {
            //oyun sonunda karakteri belirli noktaya yavasca ilerletir
            transform.position = Vector3.Lerp(transform.position, endigPoint.position, 0.05f);
            //belirli noktaya geldigini algilayan if
            if ((transform.position - endigPoint.position).sqrMagnitude < 0.1f)
            {
                stopRuning();
            }

        }



    }
    private void OnTriggerEnter(Collider other)
    {
        // Karakterin islem kapilarindan gectigini algilayan if
        if (other.tag == "Addition" || other.tag == "Subtraction" || other.tag == "Multiplication" || other.tag == "Division")
        {
            gameController.cloneManager(int.Parse(other.name), other.tag, other.transform);
        }

        // Karakterin islem kapilarindan gectigini algilayan if
        if (other.tag == "FreeClone")
        {
            other.gameObject.GetComponent<FreeCloneControl>().takeClone();
        }

        // Oyun sonuna gelindigini algilayan if
        if (other.tag == "EndTrigger")
        {
            gameController.ending();
        }

       
        



    }
    private void OnCollisionEnter(Collision col)
    {


        // oyun icinde karakterimizin takili kamasini engellemeye calisan kod
        // bu kodu hic begenmedim sonra degistiricem
        if (col.gameObject.CompareTag("Column")|| col.gameObject.CompareTag("Spike"))
        {
            if (transform.position.x >= 0)
            {
                transform.Translate(Vector3.left * 10f * Time.deltaTime);
            }
            else
            {

                transform.Translate(Vector3.right * 10f * Time.deltaTime);
            }
        }
    }


}
