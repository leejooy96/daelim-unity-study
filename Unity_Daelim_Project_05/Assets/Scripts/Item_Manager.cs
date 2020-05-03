using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.name)
        {
            case "cat":
                {
                    // 아이템이 사라지기 전에 리젠 처리
                    gameObject.GetComponentInParent<Cloud_Manager>().Start_item_regen();

                    Debug.Log("아이템 획득: " + other.name);
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
