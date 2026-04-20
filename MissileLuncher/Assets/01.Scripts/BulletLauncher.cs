using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;   // 총알 프리팹

    [SerializeField]
    Explosion explosionPrefab;   // 폭발 프리팹

    [SerializeField]    
    Transform firePosition;   // 총알 발사 시작 위치

    [SerializeField]
    float fireDelay = 0.5f;   // 발사 딜레이(연사 간격)

    float elapsedFireTime;    // 현재 누적된 시간
    bool canShoot = true;     // 발사 가능 여부

    Factory bulletFactory;    // 총알 오브젝트 풀
    Factory explosionFactory; // 폭발 오브젝트 풀

    private void Start()
    {
        // 시작 시 총알/폭발 풀 생성 준비
        bulletFactory = new Factory(bulletPrefab);
        explosionFactory = new Factory(explosionPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        // 발사가 불가능한 상태일 때만 딜레이 시간 계산
        if (!canShoot)
        {
            elapsedFireTime += Time.deltaTime;

            // 딜레이 시간이 지나면 다시 발사 가능
            if(elapsedFireTime >= fireDelay)
            {
                canShoot = true;
                elapsedFireTime = 0f;
            }
        }
    }

    // 버튼 입력 등으로 발사 요청이 들어왔을 때 호출
    public void OnFireButtonPressed(Vector3 position)
    {
        // 현재 발사 불가능하면 종료
        if (!canShoot) return;

        // 총알 풀에서 총알 하나 꺼냄
        RecycleObject bullet = bulletFactory.Get();

        // 총알 활성화
        // firePosition.position 에서 시작해서
        // 전달받은 position 방향을 향해 날아가도록 설정
        bullet.Activate(firePosition.position, position);

        // 총알이 파괴(도착)되었을 때 호출될 함수 등록
        bullet.Destroyed += OnBulletDestroyed;

        // 발사 직후에는 다시 발사 불가 상태로 변경
        canShoot = false;
    }

    // 총알이 목적지에 도착해서 Destroyed 이벤트가 발생했을 때 실행
    void OnBulletDestroyed(RecycleObject usedBullet)
    {
        // 폭발을 생성할 위치를 총알의 마지막 위치로 저장
        Vector3 lastBulletPosion = usedBullet.transform.position;

        // 이벤트 중복 등록 방지를 위해 제거
        usedBullet.Destroyed -= OnBulletDestroyed;

        // 사용 끝난 총알을 다시 풀로 반환
        bulletFactory.Restore(usedBullet);

        // 폭발 오브젝트를 풀에서 가져옴
        RecycleObject explosion = explosionFactory.Get();

        // 폭발 위치 활성화
        explosion.Activate(lastBulletPosion);

        // 폭발이 끝났을 때 호출될 함수 등록
        explosion.Destroyed += OnExplosionDestroyed;
    }

    // 폭발 이펙트가 끝났을 때 실행
    void OnExplosionDestroyed(RecycleObject usedExplosion)
    {
        // 이벤트 중복 등록 방지
        usedExplosion.Destroyed -= OnExplosionDestroyed;

        // 폭발 오브젝트를 다시 풀로 반환
        explosionFactory.Restore(usedExplosion);
    }
}