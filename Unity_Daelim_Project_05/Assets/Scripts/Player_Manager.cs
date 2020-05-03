using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Manager : MonoBehaviour
{
    float jumpForce = 500.0f;

    float walkForce = 30.0f;

    float maxWalkSpeed = 2.0f;

    public GameObject camera_GO; // 카메라 오브젝트 선언

    bool left_Pointer = false; // 좌측 포인터 사용 변수
    bool right_Pointer = false; // 우측 포인터 사용 변수

    bool jump_Pointer = false; // 점프 포인터 사용 변수

    public GameObject HP_Gauge_Img_GO; // HP 오브젝트 GO 선언

    // Update is called once per frame
    void Update()
    {
        // 낭떠러지로 떨어졌을 때 처리
        if (transform.position.y < -10)
        {
            //Scene_Manager.Instance.Scene_Change("Game_Stage_1"); // 게임 재시작 처리
            Scene_Manager.Instance.Scene_Change("Game_Lobby");
        }

        // 카메라 오브젝트 위치 값에 주인공 위치 값을 입력
        if (transform.position.y > 0)
        {
            camera_GO.transform.position = new Vector3(camera_GO.transform.position.x, transform.position.y, camera_GO.transform.position.z); // y, 위 아래로만 이동
        }
        else
        {
            camera_GO.transform.position = new Vector3(0, 0, -1f); // 카메라 위치 초기화
        }

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) || jump_Pointer)
        {
            // 점프 애니메이션으로 전환
            transform.GetComponent<Animator>().SetTrigger("JumpTrigger");

            if (transform.GetComponent<Rigidbody2D>().velocity.y == 0) // 속도가 0일 때만 점프 가능 처리
            {
                transform.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
            }
        }

        // 속도 제한 (절대값 반환)
        float speed = Mathf.Abs(transform.GetComponent<Rigidbody2D>().velocity.x);

        if (speed < maxWalkSpeed)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || left_Pointer)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(transform.right * -walkForce);
                transform.localScale = new Vector3(-1, 1, 1); // 주인공 이동 방향에 따라 객체 반전하기 (그래픽 리소스 뒤집기)
            }

            if (Input.GetKey(KeyCode.RightArrow) || right_Pointer)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(transform.right * walkForce);
                transform.localScale = new Vector3(1, 1, 1); // 주인공 이동 방향에 따라 객체 반전하기
            }
        }

        // 업데이트 문으로 이동
        if (HP_Gauge_Img_GO.GetComponent<Image>().fillAmount <= 0)
        {
            // 게임오버
            Debug.Log("게임 오버! 재시작");
            Scene_Manager.Instance.Scene_Change("Game_Lobby"); // 게임 재시작 로비로 이동
        }
    }

    /// <summary>
    /// HP 감소 코루틴 처리
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    IEnumerator HP_Change(float damage)
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            HP_Gauge_Img_GO.GetComponent<Image>().fillAmount -= damage; // HP 데미지 처리
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.name)
        {
            case "flag":
                {
                    Debug.Log("골인!" + other.name);

                    if ("Game_Stage_1".Equals(SceneManager.GetActiveScene().name))
                    {
                        // 씬매니저 싱글톤을 사용하여 접근
                        Scene_Manager.Instance.Scene_Change("Game_Stage_2");
                    }
                    else
                    {
                        // 씬매니저 싱글톤을 사용하여 접근
                        Scene_Manager.Instance.Scene_Change("Game_Result");
                    }

                    //Scene_Manager.Instance.Scene_Change("Game_Result");
                }
                break;
            case "thorn":
                {
                    Debug.Log("가시 데미지!" + other.name);

                    StartCoroutine(HP_Change(0.01f));
                    // HP_Gauge_Img_GO.GetComponent<Image>().fillAmount -= 0.1f; // HP 데미지 처리
                }
                break;
            case "item":
                {
                    Debug.Log("획득 내용" + other.name);

                    // 코루틴 처리
                    StartCoroutine(HP_Change(-0.01f)); // HP 증가 코루틴 처리
                }
                break;
        }
        /*
        if (other.name == "flag")
        {
            Debug.Log("골인!" + other.name);

            // 씬 매니저를 사용하여 접근
            //Scene_Manager.Instance.Game_Result(); // 게임 클리어 씬으로 이동
            Scene_Manager.Instance.Scene_Change("Game_Result");
        }
        */
    }

    /// <summary>
    /// 좌측 포인터 Down
    /// </summary>
    public void Left_Pointer_Down()
    {
        left_Pointer = true;
    }

    /// <summary>
    /// 좌측 포인터 Up
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
    /// 우측 포인터 Up
    /// </summary>
    public void Right_Pointer_Up()
    {
        right_Pointer = false;
    }

    /// <summary>
    /// 점프 이동 버튼
    /// </summary>
    public void Jump_Btn()
    {
        jump_Pointer = true;
        /*
        if (transform.GetComponent<Rigidbody2D>().velocity.y == 0) // 속도가 0일때만 점프 가능 처리
        {
            transform.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
        }
        */
    }
}
