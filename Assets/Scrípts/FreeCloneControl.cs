using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreeCloneControl : MonoBehaviour

{
    GameObject target;
    NavMeshAgent _navMashAgent;
    Animator animator;
    public Material blueMaterial;
    GameController gameController;
    public bool isRuning;
    void Start()
    {
        // elemanlara erisim saglandi

        _navMashAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        target = gameController.endPoint;


        isRuning = false; //baslangicda kosu gorevi deaktif edildi
    }
    private void LateUpdate()
    {
        // klonlarin hedef noktasina ilerlemesi saglandi
        //hedef nokta lateUpdate de surekli guncelleniyor
        if (isRuning)
        {
            _navMashAgent.SetDestination(target.transform.position);

        }


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

    // kendi rengini klon rengine ceviren, tagini Clone yapan,kendisini klon havuzuna gonderen ve kosma gorevini baslatan fonksiyon
    public void takeClone()
    {
        gameObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = blueMaterial;
        gameObject.tag = "Clone";
        gameController.addFreeClone(gameObject);
        startRuning();
    }




    private void OnTriggerEnter(Collider other)
    {
        // Clonlarin engellere carptiginda yok olmasi saglandi
        if (other.CompareTag("Obstacle") || other.CompareTag("Spike"))
        {

            gameController.removeClone(this.gameObject, other.tag);

        }
        // hammer a carptiginda farkli effekt olusturmasi saglandi 
        if (other.CompareTag("Hammer"))
        {
            gameController.removeClone(this.gameObject, other.tag);

        }
        // Oyun sonunda dusmanlara carpinca yok olmasi
        if (other.CompareTag("Enemy"))
        {

            gameController.removeClone(this.gameObject, other.tag);

        }
        // serbest klonlari toplamak icin kullanilan if

        if (other.tag == "FreeClone")
        {
            if (gameObject.tag != "FreeClone")
            {
                other.gameObject.GetComponent<FreeCloneControl>().takeClone();

            }
        }
    }
    private void OnTriggerStay(Collider other)
    {




        // pervane etkisini yaratan sola ve saga itme alanlarini algilayan ifler
        if (other.tag == "Pusher")
        {
            if (other.name == "LeftPusher")
            {
                transform.Translate(Vector3.left * 4f * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * 4f * Time.deltaTime);
            }
        }
    }

}
