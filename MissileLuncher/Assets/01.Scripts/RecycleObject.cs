using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    protected bool isActivated = false;   // 현재 활성 상태인지 여부

    public Action<RecycleObject> Destroyed; // 파괴(사용 종료) 시 호출할 이벤트
    protected Vector3 targetPosition;       // 이동 목표 위치

    // 단순 활성화 : 위치만 지정
    public virtual void Activate(Vector3 position)
    {
        isActivated = true;
        transform.position = position;
    }

    // 시작 위치와 목표 위치를 함께 지정하는 활성화 함수
    public virtual void Activate(Vector3 startPosition, Vector3 targetPosition)
    {
        // 시작 위치 설정
        transform.position = startPosition;

        // 목표 위치 저장
        this.targetPosition = targetPosition;

        // 시작 위치 -> 목표 위치 방향 벡터 계산
        Vector3 dir = (targetPosition - startPosition).normalized;

        // 해당 방향을 바라보도록 회전
        // 2D에서 transform.up 방향으로 이동시키기 위해 up을 dir로 맞춤
        transform.rotation = Quaternion.LookRotation(transform.forward, dir);

        // 활성화 상태로 전환
        isActivated = true;
    }
}