using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav : MonoBehaviour
{
    //获取导航代理组件
    UnityEngine.AI.NavMeshAgent agent;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
            return;
        else
        agent.SetDestination(Player.transform.position);
    }
}
