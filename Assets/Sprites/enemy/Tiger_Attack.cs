using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_Attack : MonoBehaviour
{
    public float AttackColdTime;
    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        AttackColdTime = 1.0f;
    }

    void Update()
    {
        if (AttackColdTime > 0)
        {
            AttackColdTime -= Time.deltaTime;
        }
        if (Vector3.Distance(Player.transform.position,this.transform.position) <= 3)
        {
            if (AttackColdTime <= 0)
            {
                Player.GetComponent<Tank_health>().TankDamage();
                AttackColdTime = 1.0f;
            }
        }
    }
}
