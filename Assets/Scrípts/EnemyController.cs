using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    GameObject target;
    NavMeshAgent _navMashAgent;
    GameController gameController;
    Animator animator;
    public bool isRuning;
    void Start()
    {
        // elemanlara erisim saglandi

        _navMashAgent = GetComponent<NavMeshAgent>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        animator = gameObject.GetComponent<Animator>();
        target = gameController.endPoint;


        isRuning = false; //baslangicda kosu gorevi deaktif edildi

    }


    //Animasyonu ve ilerlemeyi baslatan fonksiyon
    public void startRuning() {
        isRuning = true;
        animator.SetBool("run", true);
    }

    //Animasyonu ve ilerlemeyi durduran fonksiyon
    public void stopRuning() {
        isRuning = false;
        animator.SetBool("run", false);
        _navMashAgent.isStopped = true;

    }

    private void LateUpdate()
    {
        // dusmanlarin hedef noktasina ilerlemesi saglandi
        // navmash hedef noktasi lateUpdate de surekli guncelleniyor
        if (isRuning)
        {
            _navMashAgent.SetDestination(target.transform.position);
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {

        // Dusmanlar da klonlara carptigi zaman kendini yok ediyor
        if (other.CompareTag("Clone"))
        {

            gameController.removeEnemy(this.gameObject);

        }
    }
   
}
