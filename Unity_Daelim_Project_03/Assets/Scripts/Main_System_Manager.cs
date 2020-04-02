using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //유니티 UGUI 사용 선언.
public class Main_System_Manager : MonoBehaviour
{

    public GameObject Start_Panel_GO; // 스타트 패널 오브젝트 선언.
    public GameObject Main_Panel_GO; // 메인 패널 오브젝트 선언.
    public InputField Id_InputField; // 메인 패널 - ID 입력필드 선언.
    public InputField Pass_InputField; // 메인 패널 - ID 입력필드 선언.
    public Text Main_Panel_Text; //메인 패널 - 텍스트 선언.
    string[] Id_Array = { "제임스", "마이클", "에드워드" }; //Id 와 Pass 한쌍으로 적용.
    string[] Pass_Array = { "jjjjj", "mmmmm", "eeeee" };


    public GameObject Roulette_Img_GO; // 룰렛 이미지 GO 선언.
    public GameObject Rolling_Btn_GO; // 롤링 버튼 GO 선언.


    float rolling_Speed; // 룰렛 스피드 변수.

    public float Start_Speed = 0; // 회전 속도.
    public float break_Speed = 0; // 정지 속도.

    bool playing = false; //작동 확인 변수.
    float play_Time = 0; //룰렛 기준 시간.

    public float play_Min_Time = 0; // 사용자 입력 최소 시간.
    public float play_Max_Time = 0; // 사용자 입력 최대 시간.

    int id_Index = 0; //ID 인덱스 변수 선언.


    // Start is called before the first frame update
    void Start()
    {
        // 스크립트 시작시 메인 패널 비활성화 처리함.
        Start_Panel_GO.SetActive(true);
        Main_Panel_GO.SetActive(false);
    }


    /// <summary>
    /// 로그인 버튼 클릭 메서드 선언.
    /// </summary>
    public void Login_Button_Click()
    {

        //디버그 코드.
        Debug.Log("비밀번호 체크: " + Pass_InputField.text);

        for (int i = 0; i < Id_Array.Length; i++)
        {

            //id 와 Pass Array에서 입력 정보 비교 처리.
            if (Id_InputField.text.Equals(Id_Array[i]) && Pass_InputField.text.Equals(Pass_Array[i]))
            {
                //회원 정보가 있을 경우 로그인 및 메인 패널 활성화 처리.
                Start_Panel_GO.SetActive(false);
                Main_Panel_GO.SetActive(true);

                Main_Panel_Text.text = Id_Array[i] + "님\n" + "반갑습니다.";

                id_Index = i; // 현재 Id 인덱스 저장.


            }
        }


    }


    public void Rolling_Btn_Click()
    {


        playing = true;
        Rolling_Btn_GO.transform.GetComponent<Button>().interactable = false;
        rolling_Speed = Start_Speed; // 시작 스피드 입력.

        //play_Time = 1f;

        play_Time = Random.Range(play_Min_Time, play_Max_Time); // Min과 Max 사이 시간 랜덤 값 입력.


    }





    private void Update()
    {
        //프레임 및 타임 설명
        //Debug.Log("Time.deltaTime : " + Time.deltaTime);


        /*
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭. ( (0) 은 왼쪽 버튼 )
        {
            rolling_Speed = 100f;
        }

        Roulette_Img_GO.transform.Rotate(0, 0, rolling_Speed);


        rolling_Speed *= 0.95f;
        */

        if (playing) // 작동중일 경우.
        {
            play_Time -= Time.deltaTime;

            if (play_Time <= 0)
            {
                rolling_Speed -= break_Speed * Time.deltaTime;
            }

            Roulette_Img_GO.transform.Rotate(0, 0, -rolling_Speed * Time.deltaTime);

            if (rolling_Speed <= 0)
            {
                playing = false;
                Rolling_Btn_GO.transform.GetComponent<Button>().interactable = true;

                if (330 <= Roulette_Img_GO.transform.eulerAngles.z && Roulette_Img_GO.transform.eulerAngles.z < 30)
                {
                    Main_Panel_Text.text = Id_Array[id_Index] + "님\n" + "운수가 나쁠것 같습니다.";

                }
                else if (30 <= Roulette_Img_GO.transform.eulerAngles.z && Roulette_Img_GO.transform.eulerAngles.z < 90)
                {
                    Main_Panel_Text.text = Id_Array[id_Index] + "님\n" + "운수가 대통할 것 같습니다.";

                }
                else if (90 <= Roulette_Img_GO.transform.eulerAngles.z && Roulette_Img_GO.transform.eulerAngles.z < 150)
                {
                    Main_Panel_Text.text = Id_Array[id_Index] + "님\n" + "운수가 매우 나쁠 것 같습니다.";

                }
                else if (150 <= Roulette_Img_GO.transform.eulerAngles.z && Roulette_Img_GO.transform.eulerAngles.z < 210)
                {
                    Main_Panel_Text.text = Id_Array[id_Index] + "님\n" + "운수가 보통인 것 같습니다.";

                }
                else if (210 <= Roulette_Img_GO.transform.eulerAngles.z && Roulette_Img_GO.transform.eulerAngles.z < 270)
                {
                    Main_Panel_Text.text = Id_Array[id_Index] + "님\n" + "운수가 조심 상태입니다.";

                }
                else if (270 <= Roulette_Img_GO.transform.eulerAngles.z && Roulette_Img_GO.transform.eulerAngles.z < 330)
                {
                    Main_Panel_Text.text = Id_Array[id_Index] + "님\n" + "운수가 좋을 것 같습니다.";

                }



            }
        }



    }



}
