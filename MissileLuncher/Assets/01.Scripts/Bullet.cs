using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    [SerializeField]
    float moveSpeed = 5f;   // 총알 이동 속도

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 활성화되지 않은 상태면 아무 동작도 하지 않음
        if (!isActivated) return;

        // 총알이 자신의 위쪽 방향(transform.up)으로 계속 이동
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        // 목표 위치에 도착했는지 검사
        if (IsArrivedToTarget())
        {
            // 도착했으면 비활성 상태로 변경
            isActivated = false;

            // Destroyed 이벤트가 등록되어 있으면 호출
            // 보통 총알을 풀로 반환시키는 용도
            if (Destroyed != null)
            {
                Destroyed(this);
            }
        }
    }

    // 현재 위치와 목표 위치 사이 거리가 매우 가까우면 도착으로 판단
    bool IsArrivedToTarget()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance < 0.1f;
    }
}