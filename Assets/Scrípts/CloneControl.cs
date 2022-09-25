using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloneControl : MonoBehaviour

{
    GameObject target;
    NavMeshAgent _navMashAgent;
    void Start()
    {
        // prefab objesinin navmash ve gameController objesine erismesi saglandi

        _navMashAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().endPoint;
    }
    private void LateUpdate()
    {
        // klonlarin hedef noktasina ilerlemesi saglandi
        //hedef nokta surekli guncelleniyor

        _navMashAgent.SetDestination(target.transform.position);

    }

}
