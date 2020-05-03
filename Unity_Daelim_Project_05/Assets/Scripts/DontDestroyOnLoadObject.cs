using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		// 씬 간의 이동이 처리될 때 오브젝트를 유지함
        DontDestroyOnLoad(this);
    }
}
