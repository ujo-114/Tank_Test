using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : MonoBehaviour {

    float BoomTime;

    private void Awake()
    {
      
    }

    private void OnEnable()
    {
        BoomTime = 0.6f;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BoomTime -= Time.deltaTime;

        if (BoomTime<=0)
        {          
            GameManagerScript.Instance.RecoveryGameObjct(gameObject);
        }
    }


}
