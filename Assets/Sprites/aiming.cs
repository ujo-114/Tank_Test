using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming : MonoBehaviour
{
    private Transform headTransform;
    LayerMask floorMask;
    Rigidbody headRigidbody;
    public Transform Tank;
    void Start()
    {
        headTransform = transform.Find("head");//找到head
        floorMask = LayerMask.GetMask("floor");//获取地板layer
        headRigidbody = this.GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        headAiming();
        //headTransform.position = Tank.position;
    }
    void headAiming()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cameraHit;
        if(Physics.Raycast(cameraRay,out cameraHit, 100f, floorMask))
        {
            Vector3 playerToMouse = cameraHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newQuaternion = Quaternion.LookRotation(playerToMouse);
            headRigidbody.MoveRotation(newQuaternion);
        }
    }
}
