using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    // 싱글톤 구성
    public static Scene_Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    ///  스테이지 A 전환하기
    /// </summary>
    public void Game_Stage_A()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; // 씬 화면 전환
        SceneManager.LoadScene("Game_Stage_A"); // 게임 스테이지 A 씬 로드
    }

    /// <summary>
    ///  스테이지 B 전환하기
    /// </summary>
    public void Game_Stage_B()
    {
        Screen.orientation = ScreenOrientation.Portrait; // 씬 화면 전환
        SceneManager.LoadScene("Game_Stage_B"); // 게임 스테이지 B 씬 로드
    }

    /// <summary>
    ///  게임 로비 전환하기
    /// </summary>
    public void Game_Result()
    {
        Screen.orientation = ScreenOrientation.Portrait; // 씬 화면 전환
        SceneManager.LoadScene("Game_Result"); // 게임 결과 씬 로드
    }

    /// <summary>
    ///  게임 로비 전환하기
    /// </summary>
    public void Game_Lobby()
    {
        Screen.orientation = ScreenOrientation.Portrait; // 씬 화면 전환
        SceneManager.LoadScene("Game_Lobby"); // 게임 로비 씬 로드
    }
}
