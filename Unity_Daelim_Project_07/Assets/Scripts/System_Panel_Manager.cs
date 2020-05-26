using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class System_Panel_Manager : MonoBehaviour
{

    private static System_Panel_Manager instance;
    public static System_Panel_Manager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<System_Panel_Manager>();

                if (!instance)
                {
                    GameObject gameObject = new GameObject();
                    gameObject.name = "System_Panel_Manager";
                    instance = gameObject.AddComponent<System_Panel_Manager>();
                }
            }
            return instance;
        }
    }

    //1차.
    public GameObject Lobby_Panel_GO; // 로비 패널 GO 선언.
    public Text LP_Info_Text; // 로비 패널 정보 텍스트 선언.


    public GameObject Sign_In_Panel_GO; // 사용자 접속 패널 GO 선언.
    public Text SIP_Info_Text; // 사용자 접속 정보 텍스트 선언.
    public InputField SIP_Email_InputField;  // 사용자 접속 이메일 입력 필드 선언.
    public InputField SIP_Pass_InputField; // 사용자 접속 비밀번호 입력 필드 선언.
    


    public GameObject Sign_Up_Panel_GO; // 사용자 가입 패널 GO 선언.
    public Text SUP_Info_Text; // 사용자 가입 정보 텍스트 선언.
    public InputField SUP_Email_InputField; // 사용자 가입 이메일 입력 필드 선언.
    public InputField SUP_Pass_InputField; // 사용자 가입 비밀번호 입력 필드 선언.
    public InputField SUP_RePass_InputField; // 사용자 가입 비밀번호 재입력 필드 선언.

    
    //2차.
    public Toggle SIP_Remember_Email_Toggle; // 이메일 기억하기 토글 선언.


    /// <summary>
    /// 회원 가입 패널 버튼 사용 처리.
    /// </summary>
    public void Sign_Up_Page_Btn_Use()
    {
        Sign_In_Panel_GO.SetActive(false); // 패널 비활성화.
        Sign_Up_Panel_GO.SetActive(true); // 패널 활성화.

    }


    /// <summary>
    /// 회원 인증 패널 버튼 사용 처리.
    /// </summary>
    public void Sign_In_Page_Btn_Use()
    {
        Sign_In_Panel_GO.SetActive(true); // 패널 활성화.
        Sign_Up_Panel_GO.SetActive(false); // 패널 비활성화.

    }

    /// <summary>
    /// 사용자 로그아웃 버튼 사용 처리.
    /// </summary>
    public void Sign_Out_Btn_Use()
    {
        Lobby_Panel_GO.SetActive(false); // 패널 비활성화.
        Sign_In_Panel_GO.SetActive(true); // 패널 활성화.
        Sign_Up_Panel_GO.SetActive(false); // 패널 비활성화.

    }


}


