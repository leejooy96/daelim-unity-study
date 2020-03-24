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
        for (int i = 0; i < Id_Array.Length; i++)
        {
            //디버그 코드.
            Debug.Log("비밀번호 체크: " + Pass_InputField.text);

            //id 와 Pass Array에서 입력 정보 비교 처리.
            if (Id_InputField.text.Equals(Id_Array[i]) && Pass_InputField.text.Equals(Pass_Array[i]))
            {
                //회원 정보가 있을 경우 로그인 및 메인 패널 활성화 처리.
                Start_Panel_GO.SetActive(false); 
                Main_Panel_GO.SetActive(true);

                Main_Panel_Text.text = Id_Array[i] + "님\n" + "반갑습니다.";
            }
        }
    }    
}
