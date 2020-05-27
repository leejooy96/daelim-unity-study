using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamsongi_Manager : MonoBehaviour
{
    public void Shoot(Vector3 dir)
    {
        // 벡터 방향으로 오브젝트에 힘을 가한다.
        transform.GetComponent<Rigidbody>().AddForce(dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 이전 시간의 구름 처럼 작용하는 힘을 무시하고 정지 처리한다.
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<ParticleSystem>().Play(); // 파티클 시스템을 처리한다.
        transform.GetComponent<AudioSource>().Play(); // 효과음 처리
        Debug.Log(collision.gameObject);
        if (collision.gameObject == GameObject.Find("target"))
        {
            Game_Manager.score += 10;
        }
        else if (collision.gameObject == GameObject.Find("Terrain"))
        {
            Game_Manager.score -= 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Shoot(new Vector3(0, 200, 2000));
    }
}
