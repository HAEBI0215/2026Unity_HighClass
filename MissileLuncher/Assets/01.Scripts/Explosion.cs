using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 오브젝트에는 CircleCollider2D와 Rigidbody2D가 반드시 있어야 함
[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Explosion : RecycleObject
{
    CircleCollider2D box;   // 폭발 범위용 콜라이더
    Rigidbody2D body;       // 물리 설정용 리지드바디

    [SerializeField]
    float timeToRemove = 1f;   // 몇 초 뒤 제거할지

    float elapsedTime = 0f;    // 활성화 후 경과 시간

    // 컴포넌트 초기 설정
    private void Awake()
    {
        box = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();

        // 폭발은 충돌 판정만 하고 물리적으로 밀리지 않게 트리거/키네마틱 설정
        box.isTrigger = true;
        body.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        // 활성화 상태일 때만 시간 측정
        if (isActivated)
        {
            elapsedTime += Time.deltaTime;

            // 설정된 시간이 지나면 스스로 제거 처리
            if(elapsedTime >= timeToRemove)
            {
                elapsedTime = 0f;
                DestroySelf();
            }
        }
    }

    // 자기 자신을 비활성 처리하고 Destroyed 이벤트 호출
    void DestroySelf()
    {
        isActivated = false;

        // null 체크 후 이벤트 호출
        Destroyed?.Invoke(this);
    }
}