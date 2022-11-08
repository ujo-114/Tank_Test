using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_2 : MonoBehaviour
{
    //移动速度
    public int speed = 60;

    Rigidbody playerRigidBody;   //玩家刚体
    Vector3 moveMent;   //移动向量
    Vector3 moveDirection;//移动方向
    public Transform Body;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        move(h, v);
        //direct();
    }
    void move(float h,float v)
    {
        moveMent.Set(h, 0f, v);
        moveMent = moveMent.normalized * speed * Time.deltaTime;    //设置方向，并标准化
        playerRigidBody.MovePosition(transform.position + moveMent);
        Body.LookAt(transform.position + moveMent);
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }
    /*void direct()
    {	//检测四个斜向的按键
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            Body.localRotation = Quaternion.Euler(0, -45, 0);//旋转的四元数
            //m_Transform.Translate(new Vector3(-1, 0, 1) * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            Body.localRotation = Quaternion.Euler(0, 45, 0);
            // m_Transform.Translate(new Vector3(1, 0, 1) * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            Body.localRotation = Quaternion.Euler(0, -135, 0);
            //m_Transform.Translate(new Vector3(-1, 0, -1) * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            Body.localRotation = Quaternion.Euler(0, 135, 0);
            //m_Transform.Translate(new Vector3(1, 0, -1) * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.W))
            {
                Body.localRotation = Quaternion.Euler(0, 0, 0);
                //m_Transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
            }
        else if (Input.GetKey(KeyCode.S))
            {
                Body.localRotation = Quaternion.Euler(0, 180, 0);
                //  m_Transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
            }
        else if (Input.GetKey(KeyCode.A))
            {
                Body.localRotation = Quaternion.Euler(0, -90, 0);
                //m_Transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            }
        else if (Input.GetKey(KeyCode.D))
            {
            Body.localRotation = Quaternion.Euler(0, 90, 0);
                //m_Transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            }
        }*/
    }