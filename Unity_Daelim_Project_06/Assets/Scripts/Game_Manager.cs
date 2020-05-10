using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public GameObject bamsongiPrefab_GO; // 밤송이 프리팹 GO 선언
    public Slider Power_Slider; // 파워 슬라이더의 사용 선언
    bool Slider_Play_On = false; // 슬라이더 플레이 사용 체크 변수

    IEnumerator Slider_Play()
    {
        while (Slider_Play_On)
        {
            if (Power_Slider.value < 1)
            {
                Power_Slider.value += 0.01f; // 파워 슬라이더가 점점 상승한다.
            }
            else
            {
                Power_Slider.value = 0; // 초기화된다.
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnButton(string button_name)
    {
        switch (button_name)
        {
            case "mouse_down":
                {
                    if (!Slider_Play_On) // 파워 슬라이드 작동하지 않을 경우에만 사용한다.
                    {
                        Power_Slider.value = 0; // 슬라이더 초기화
                        Slider_Play_On = true;

                        StartCoroutine(Slider_Play()); // 파워 슬라이드 작동
                    }
                    break;
                }
            case "mouse_up":
                {
                    Slider_Play_On = false;

                    GameObject bamsongi = Instantiate(bamsongiPrefab_GO) as GameObject;

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라를 사용하여 벡터를 구한다.
                    Vector3 worldDirection = ray.direction; // 구한 벡터 값을 사용 변수에 적용한다.
                    bamsongi.GetComponent<Bamsongi_Manager>().Shoot(worldDirection.normalized * 2000 * Power_Slider.value); // 파워 슬라이드 값을 적용한다.
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*
        // 지속적으로 클릭을 감지한다.
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bamsongi = Instantiate(bamsongiPrefab_GO) as GameObject;
            //bamsongi.GetComponent<Bamsongi_Manager>().Shoot(new Vector3(0, 200, 2000));

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라를 사용하여 벡터를 구한다.
            Vector3 worldDirection = ray.direction; // 구한 벡터 값을 사용 변수에 적용.
            bamsongi.GetComponent<Bamsongi_Manager>().Shoot(worldDirection.normalized * 2000);
        }
        */
    }
}
