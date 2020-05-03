using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Manager : MonoBehaviour
{
    public void Start_item_regen()
    {
        StartCoroutine(Cloud_item_regen());
    }

    IEnumerator Cloud_item_regen()
    {
        float regen_Time = Random.Range(1.0f, 5.0f);

        yield return new WaitForSeconds(regen_Time);

        transform.GetChild(0).gameObject.SetActive(true); // 첫번째 자식 오브젝트 랜덤 시간 경과 후 아이템 리젠 적용하기
    }
}
