﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{

    float jumpforce = 700.0f;

    float walkforce = 30.0f;

    float maxWalkSpeed = 2.0f;


    public GameObject camera_GO; //카메라 오브젝트 선언.


    bool left_Pointer = false; //좌측 포인터 사용 변수.
    bool right_Pointer = false;//우측 포인터 사용 변수.



    // Update is called once per frame
    void Update()
    {

        //카메라 오브젝트 위치값에 주인공 위치값을 입력.
        if (transform.position.y > 0)
        {
            camera_GO.transform.position = new Vector3(camera_GO.transform.position.x, transform.position.y, camera_GO.transform.position.z);
        }
        else
        {
            camera_GO.transform.position = new Vector3(0, 0, -1f); // 카메라 위치 초기화.
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.GetComponent<Rigidbody2D>().velocity.y == 0) // 속도가 0일때만 점프 가능 처리.
            {
                transform.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpforce);
            }
        }


        //속도 제한(절대값 반환).
        float speed = Mathf.Abs(transform.GetComponent<Rigidbody2D>().velocity.x);

        if (speed < maxWalkSpeed)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || left_Pointer)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(transform.right * -walkforce);
                transform.localScale = new Vector3(-1, 1, 1); //주인공 이동 방향에 따라 객체 반전.
            }

            if (Input.GetKey(KeyCode.RightArrow) || right_Pointer)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(transform.right * walkforce);
                transform.localScale = new Vector3(1, 1, 1); //주인공 이동 방향에 따라 객체 반전.
            }
        }


    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "flag")
        {
            Debug.Log("골인!" + other.name);

            //씬매니저 싱글톤 사용하여 접근.
            Scene_Manager.Instance.Game_Result();// 게임 클리어 및 이동.


        }
    }



    /// <summary>
    /// 좌측 포인터 Down
    /// </summary>
    public void Left_Pointer_Down()
    {
        left_Pointer = true;
    }



    /// <summary>
    /// 좌측 포인터 Down
    /// </summary>
    public void Left_Pointer_Up()
    {
        left_Pointer = false;
    }



    /// <summary>
    /// 우측 포인터 Down
    /// </summary>
    public void Right_Pointer_Down()
    {
        right_Pointer = true;
    }



    /// <summary>
    /// 우측 포인터 Down
    /// </summary>
    public void Right_Pointer_Up()
    {
        right_Pointer = false;
    }




    /// <summary>
    /// 점프 이동 버튼.
    /// </summary>
    public void Jump_Btn()
    {
        if (transform.GetComponent<Rigidbody2D>().velocity.y == 0) // 속도가 0일때만 점프 가능 처리.
        {
            transform.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpforce);
        }

    }


}
