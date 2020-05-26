using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//파이어베이스 관련 네임스페이스 선언.
using Firebase.Auth; 
using Firebase;
using Firebase.Unity.Editor;


public class System_Firebase_Auth_Manager : MonoBehaviour
{

    //싱글톤 패턴 처리.
    private static System_Firebase_Auth_Manager instance;
    public static System_Firebase_Auth_Manager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<System_Firebase_Auth_Manager>();

                if (!instance)
                {
                    GameObject gameObject = new GameObject();
                    gameObject.name = "System_Firebase_Auth_Manager";
                    instance = gameObject.AddComponent<System_Firebase_Auth_Manager>();
                }
            }
            return instance;
        }
    }

    //파이어베이스를 사용 변수 선언.
    FirebaseAuth auth;    
    public FirebaseUser user;


    // 파이어베이스 연동 인증 초기화 작업
    void Start()
    {
        //패널 초기화 처리.
        System_Panel_Manager.Instance.Lobby_Panel_GO.SetActive(false); // 로비 패널 비활성화.
        System_Panel_Manager.Instance.Sign_In_Panel_GO.SetActive(true); // 사용자 접속 패널 활성화.
        System_Panel_Manager.Instance.Sign_Up_Panel_GO.SetActive(false); // 사용자 가입 패널 비활성화.



        //panda: 인스턴스를 auth에 적용함.
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }



    /// <summary>
    /// 파이어베이스 연동 신규 회원가입 전처리 작업.
    /// </summary>
    public void Firebase_PreSignUp()
    {
        if(System_Panel_Manager.Instance.SUP_Pass_InputField.text.Equals(System_Panel_Manager.Instance.SUP_RePass_InputField.text))
        {
            //비밀번호입력 및 확인까지 되었다면.
            Firebase_SignUp();//회원 가입 처리 진행.
        }
        else
        {
            //비밀번호 재입력 바람.
            System_Panel_Manager.Instance.SUP_Info_Text.text = "패스워드가 일치하지 않습니다.";
        }
    }



    /// <summary>
    /// 파이어베이스 연동 신규 회원가입.
    /// </summary>
    public void Firebase_SignUp()
    {
        //유니티 파이어베이스 SDK 다운로드 주소.
        //https://firebase.google.com/download/unity?hl=ko
        

        auth.CreateUserWithEmailAndPasswordAsync(System_Panel_Manager.Instance.SUP_Email_InputField.text, System_Panel_Manager.Instance.SUP_Pass_InputField.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                //접속 정보 표시.                
                //System_Panel_Manager.Instance.SUP_Info_Text.text = "로그인 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                //접속 정보 표시.
                //System_Panel_Manager.Instance.SUP_Info_Text.text = "로그인 실패";                
                return;
            }
            


            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

        });
    }


    
    string system_Message = ""; //시스템 메세지.

    /// <summary>
    /// 파이어베이스 연동 회원 접속
    /// </summary>
    public void Firebase_SignIn()
    {
        
        
        StartCoroutine(System_Message_Use(System_Panel_Manager.Instance.SIP_Info_Text));



        auth.SignInWithEmailAndPasswordAsync(System_Panel_Manager.Instance.SIP_Email_InputField.text, System_Panel_Manager.Instance.SIP_Pass_InputField.text).ContinueWith(task => {
            if (task.IsCanceled)
            {

                Debug.Log("SignInWithEmailAndPasswordAsync was canceled.");

                system_Message = "로그인 실패하였습니다!";
                return;
            }
            if (task.IsFaulted)
            {


                Debug.Log("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);

                system_Message = "로그인 실패하였습니다!";

                return;
            }

                

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);


                system_Message = "로그인 성공";


        });

        
    }


    /// <summary>
    /// 시스템 메세지 코루틴.
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    IEnumerator System_Message_Use(Text text)
    {
        yield return new WaitUntil(()=> !system_Message.Equals(""));
        text.text = system_Message; // 시스템 메세지 처리.

        system_Message = "";//초기화.
    }



    /// <summary>
    /// 파이어베이스 연동 인증 SignOut
    /// </summary>
    public void Firebase_SignOut()
    {
        auth.SignOut();

        //사용자 로그 아웃시 사용자 접속 관리 패널 열기
        System_Panel_Manager.Instance.Lobby_Panel_GO.SetActive(true); // 패널 비활성화.

        System_Panel_Manager.Instance.Sign_In_Panel_GO.SetActive(true); // 패널 활성화.
    }



    /// <summary>
    /// 파이어베이스 연동 인증후 콜백함수 처리.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);

                //접속 정보 표시.
                System_Panel_Manager.Instance.LP_Info_Text.text = user.Email + " \n " + user.UserId;

                //접속 완료시 사용자 접속 관리 패널 닫기.
                System_Panel_Manager.Instance.Sign_In_Panel_GO.SetActive(false); // 패널 비활성화.                

                System_Panel_Manager.Instance.Sign_Up_Panel_GO.SetActive(false); //회원가입 패널 비활성화.

                System_Panel_Manager.Instance.Lobby_Panel_GO.SetActive(true); //로비 패널 활성화.

                System_Panel_Manager.Instance.LP_Info_Text.text = "welcome!" + user.UserId; //회원가입 성공 메세지 처리.


                if(System_Panel_Manager.Instance.SIP_Remember_Email_Toggle.isOn) // 토글 체크되어 있을 경우.
                {
                    //System_Data_Manager.Instance.Email_Data_Save(user.Email); //이메일 저장 처리.
                }
                                

            }
        }
    }


    /// <summary>
    /// 파이어베이스 연동 익명 로그인.
    /// </summary>
    public void Firebase_SignIn_Anonymously()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            Debug.Log(newUser.Email + " / " + newUser.UserId);
                      

        });
    }
}
