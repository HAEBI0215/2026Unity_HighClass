using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory 
{
    List<RecycleObject> pool = new List<RecycleObject>(); // 사용 가능한 오브젝트를 담아두는 풀
    int defaultPoolSize;                                  // 풀이 비었을 때 한 번에 생성할 개수
    RecycleObject prefab;                                 // 복제할 원본 프리팹

    // 생성자 : 어떤 프리팹을 몇 개씩 풀에 채울지 설정
    public Factory(RecycleObject prefab, int defaultPoolSize = 5)
    {
        this.prefab = prefab;
        this.defaultPoolSize = defaultPoolSize;

        // 프리팹이 비어 있으면 에러 표시
        Debug.Assert(this.prefab != null, "Prefab is null");
    }

    // 풀에 오브젝트를 defaultPoolSize 만큼 생성해서 넣어두는 함수
    void CreatePool()
    {
        for(int i = 0; i < defaultPoolSize; i++)
        {
            // 프리팹 복제
            RecycleObject obj = GameObject.Instantiate(prefab) as RecycleObject;

            // 처음엔 비활성화 상태로 보관
            obj.gameObject.SetActive(false);

            // 풀에 추가
            pool.Add(obj);
        }
    }

    // 풀에서 오브젝트 하나 꺼내 반환
    public RecycleObject Get()
    {
        // 풀이 비어 있으면 새로 생성
        if(pool.Count == 0)
        {
            CreatePool();
        }

        // 리스트의 마지막 오브젝트를 꺼냄
        int lastIndex = pool.Count - 1;
        RecycleObject obj = pool[lastIndex];
        pool.RemoveAt(lastIndex);

        // 사용하기 위해 활성화
        obj.gameObject.SetActive(true);

        return obj;
    }

    // 사용이 끝난 오브젝트를 다시 풀에 반환
    public void Restore(RecycleObject obj)
    {
        Debug.Assert(obj != null, "Null Object to be returned!");

        // 보관을 위해 비활성화
        obj.gameObject.SetActive(false);

        // 다시 풀에 추가
        pool.Add(obj);
    }
}