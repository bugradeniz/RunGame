using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloneControl : MonoBehaviour

{
    GameObject target;
    NavMeshAgent _navMashAgent;
    GameController gameController;
    void Start()
    {
        // prefab klon objesinin navmash ve gameController objesine erismesi saglandi

        _navMashAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().endPoint;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void LateUpdate()
    {
        // klonlarin hedef noktasina ilerlemesi saglandi
        //hedef nokta lateUpdate de surekli guncelleniyor

        _navMashAgent.SetDestination(target.transform.position);

    }
    private void OnTriggerEnter(Collider other)
    {
        // Clonlarin engellere carptiginda yok olmasi saglandi
        if (other.CompareTag("Obstacle"))
        {
            
            gameController.removeClone(this.gameObject,other.tag);
            
        }
        // hammer a carptiginda farkli effekt olusturmasi saglandi 
        if (other.CompareTag("Hammer"))
        {
            Debug.Log("hammer a carptiiiiiiiiii");
            gameController.removeClone(this.gameObject,other.tag);

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
                transform.Translate(Vector3.left * 4f * Time.deltaTime);
            }
            else
            {

                Debug.Log("on trigger pusher  a girildi");
                transform.Translate(Vector3.right * 4f * Time.deltaTime);
            }
        }
    }

}
