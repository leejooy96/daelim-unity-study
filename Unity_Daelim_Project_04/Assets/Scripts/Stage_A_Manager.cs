using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_A_Manager : MonoBehaviour
{
    public GameObject Cheez_Img_GO; // 주인공 오브젝트 GO 선언.
    //public GameObject Arrow_Img_GO; // 장애물 오브젝트 GO 선언.

    public GameObject Arrow_Img_GO_Prefab; // 장애물 오브젝트 프리팹 GO 배열 선언.
    GameObject[] Arrow_Img_Instance_GO; // 장애물 인스턴스 배열 선언.
    public GameObject Prefab_Parent_GO; // 프리팹 부모 GO 선언.

    public int arrow_Max_Count = 0; // 장애물 최대 갯수.
    public float spawn_time = 0; // 생성 시간.
    float time = 0f; // 기준 시간.

    public GameObject HP_Gauge_Img_GO; // HP 게이지 GO 선언.

    public GameObject Goal_Img_GO; // 목적지 GO 선언.

    /// <summary>
    /// 좌 버튼 처리 메소드
    /// </summary>
    public void Left_Btn()
    {
        Cheez_Img_GO.transform.Translate(-20f, 0, 0f);
    }

    /// <summary>
    /// 우 버튼 처리 메소드
    /// </summary>
    public void Right_Btn()
    {
        Cheez_Img_GO.transform.Translate(20f, 0, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        System.Array.Resize(ref Arrow_Img_Instance_GO, (arrow_Max_Count)); // 배열 크기를 arrow_Max_Count 만큼 늘리기

        // 인스턴스 오브젝트 풀 초기화
        for (int i = 0; i < arrow_Max_Count; i++)
        {
            Arrow_Img_Instance_GO[i] = Instantiate(Arrow_Img_GO_Prefab) as GameObject;
            Arrow_Img_Instance_GO[i].SetActive(false); // 인스턴스 오브젝트 풀 비활성화 처리
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawn_time)
        {
            int go_index = 0;

            for (int i = 0; i < arrow_Max_Count; i++)
            {
                if (Arrow_Img_Instance_GO[i].activeSelf == false) // 비활성화된 오브젝트 발견 시
                {
                    go_index = i; // 인덱스 값 전달하기

                    Arrow_Img_Instance_GO[go_index].transform.SetParent(Prefab_Parent_GO.transform); // 부모 캔버스 오브젝트로 이동
                    Arrow_Img_Instance_GO[go_index].transform.localScale = new Vector3(1f, 1f, 1f);
                    Arrow_Img_Instance_GO[go_index].SetActive(true); // 프리팹 인스턴스 활성화 (재활용)
                    float go_X = Random.Range(100f, Screen.width); // 위치 X 값 랜덤 생성. (스크린 가로 사이즈)
                    Arrow_Img_Instance_GO[go_index].transform.position = new Vector3(go_X, Screen.height, 0); // 스크린 세로 사이즈

                    break;
                }
            }

            time = 0; // 사용시간 변수 0으로 초기화
        }

        // Prefab 장애물 낙하 처리
        for (int i = 0; i < arrow_Max_Count; i++)
        {
            if (Arrow_Img_Instance_GO[i].activeSelf)
            {
                // 화면 밖으로 장애물이 내려가면 오브젝트 소멸하기
                if (Arrow_Img_Instance_GO[i].transform.position.y < 0f)
                {
                    Arrow_Img_Instance_GO[i].SetActive(false);
                }
                else
                {
                    // 프레임마다 장애물 등속 낙하
                    Arrow_Img_Instance_GO[i].transform.Translate(0, -(Screen.height / 100f), 0); // 스크린 사이즈에 비례하여 낙하속도 결정
                }
            }
        }

        // 주인공과 장애물 충돌 판정하기
        for (int i = 0; i < arrow_Max_Count; i++)
        {
            if (Arrow_Img_Instance_GO[i].activeSelf) // 활성화된 장애물 오브젝트만 검사
            {
                Vector2 p1 = Cheez_Img_GO.transform.position;
                Vector2 p2 = Arrow_Img_Instance_GO[i].transform.position;
                Vector2 dir = p1 - p2;

                //Debug.Log("dir 벡터 : " + dir);

                float distance = dir.magnitude;

                //Debug.Log("거리 : " + distance);

                if (distance <= 75f) // 두 오브젝트가 충돌 판정되었을 경우.
                {
                    Arrow_Img_Instance_GO[i].SetActive(false); // 충돌된 오브젝트 비활성화 처리.

                    if (HP_Gauge_Img_GO.transform.GetComponent<Image>().fillAmount > 0)
                    {
                        // 충돌 판정되었을 경우 주인공 효과음 적용.
                        Cheez_Img_GO.transform.GetComponent<AudioSource>().Play();

                        // 충돌 판정되었을 경우 주인공 HP 게이지 감소
                        HP_Gauge_Img_GO.transform.GetComponent<Image>().fillAmount -= 0.1f;
                    }
                }
            }
        }

        // 주인공 HP가 0 이하일 때 게임 종료 처리
        if (HP_Gauge_Img_GO.transform.GetComponent<Image>().fillAmount <= 0)
        {
            // 게임 종료 처리.
            Scene_Manager.Instance.Game_Lobby(); // 로비로 이동
        }

        // 주인공이 골에 도착했을 경우.
        Vector2 cheez_p1 = Cheez_Img_GO.transform.position;
        Vector2 goal_p2 = Goal_Img_GO.transform.position;
        Vector2 d = cheez_p1 - goal_p2;

        float goal_Distance = d.magnitude;

        if (goal_Distance <= 50f) // 두 오브젝트가 판정되었을 경우. (목적지에 도착)
        {
            Debug.Log("골인!");

            Scene_Manager.Instance.Game_Stage_B(); // 스테이지 B로 이동
        }


        // 캐릭터 이동과 관련한 PC 에디터 테스트 코드
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Cheez_Img_GO.transform.Translate(-20f, 0, 0f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Cheez_Img_GO.transform.Translate(20f, 0, 0f);
        }

        /*if (Arrow_Img_GO.transform.position.y < -5.0f)
        {
            // Destroy(Arrow_Img_GO); //재활용 이슈
            Arrow_Img_GO.SetActive(false);
        }
        else
        {
            Debug.Log(Arrow_Img_GO.transform.position.y);

            // 프레임 마다 장애물 등속 낙하
            Arrow_Img_GO.transform.Translate(0, -1f, 0);
        }*/
    }
}
