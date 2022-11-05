using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
public class CharacterControl : MonoBehaviour
{
    private Pref pf = new Pref();
    public GameController gameController;
    public bool isEnding;
    public Transform endingPoint;
    public bool isRuning;
    public Animator animator;
    public Rigidbody rb;
    public float maxDistance;
    public GameObject[] hats;
    public Vector3 rotation;

    public GameObject[] sticks;
    public Material[] skins;
    public bool canMove;

    void Start()
    {

        //Baslangic ve bitis arasindaki mesafe saklandi.

        // baslangicta ileri kosma ve sag sola kontrol devreye giriyor.
        // bitis gorevi deaktif tutuluyor.

        

        showItems();

        startRuning();

    }


    //giyili esyalari aktif eden fonksiyon
    void showItems()
    {



        for (int i = 0; i < hats.Length; i++)
        {
            hats[i].SetActive(false); // listedeki herbir obje sirasi geldiginde pasif hale getiriliyor.

            if (i == pf.getI("CurrentHat"))                         // eger index sahip olunan esya indexinde ise iceri giriyor
            {
                hats[i].SetActive(true);                            // once esya aktif hale getiriliyor.


            }

        }

        for (int i = 0; i < sticks.Length; i++)// sopalar icin de ayni sey gecerlli
        {
            sticks[i].SetActive(false);

            if (i == pf.getI("CurrentStick"))
            {
                sticks[i].SetActive(true);



            }

        }


        for (int i = 0; i < skins.Length; i++) // kiyafetteki tek fark pasif hale getirme yok
        {


            if (i == pf.getI("CurrentSkin"))
            {

                gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = skins[i];// ve tuttugumuz renderer objesine indexteki material ataniyor.


                break;
            }


        }


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
        canMove = true;
        isRuning = true;
        isEnding = false;
        animator.SetBool("run", true);
    }

    //Animasyonu ve ilerlemeyi durduran fonksiyon
    public void stopRuning()
    {
        isRuning = false;
        animator.SetBool("run", false);
        isEnding = false;

    }
    private void Update()
    {
        rotation = (Vector3.forward);
        if (Input.GetKey(KeyCode.Mouse0)&& canMove)
        {
            if (Input.GetAxis("Mouse X") < 0)
            {

                rotation = (Vector3.forward + Vector3.left);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                rotation = (Vector3.forward + Vector3.right);

            }
        }

       
    }
    private void FixedUpdate()
    {
        if (isRuning)
        {


            rb.MovePosition(Vector3.Lerp(transform.position, transform.position + (rotation) * 10f, Time.deltaTime * 0.1f));
            rb.velocity = Vector3.zero;







            /*

                        transform.Translate(Vector3.forward * 1f * Time.deltaTime);

                        // Mouse tiklanip suruklendiginde o yonde kaydirilir.
                        // Ilerde ekrana dokunma ve surukleme olarak degistirilecek
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            if (Input.GetAxis("Mouse X") < 0)
                            {

                                transform.Translate((Vector3.left) * 1f * Time.deltaTime);
                            }
                            if (Input.GetAxis("Mouse X") > 0)
                            {
                                transform.Translate((Vector3.right) * 1f * Time.deltaTime);
                            }

                        }

                        // karaktere ileri dogru bir hiz verir

                     */

        }
        if (isEnding)
        {
            //oyun sonunda karakteri belirli noktaya yavasca ilerletir
            rb.MovePosition(Vector3.Lerp(transform.position, endingPoint.position, 0.05f));
            //belirli noktaya geldigini algilayan if
            if ((transform.position - endingPoint.position).sqrMagnitude < 0.01f)
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
        if (col.gameObject.CompareTag("Column") )
        {
            canMove = false;

        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Column"))
        {
            canMove = true;

        }
    }


}
