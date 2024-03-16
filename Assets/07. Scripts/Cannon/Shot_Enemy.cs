using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Enemy : MonoBehaviour
{
    public float ForwardSpeed = 10000f; // 앞으로 돌진하는 속도

    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
      transform.SetParent(null);

        rigid = GetComponent<Rigidbody>();
        // 포탄 초기 속도 설정
        rigid.velocity = transform.forward * ForwardSpeed;
    }
}
