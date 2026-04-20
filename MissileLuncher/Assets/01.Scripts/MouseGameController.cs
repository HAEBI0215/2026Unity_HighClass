using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGameController : MonoBehaviour, IGameController
{
    public Action<Vector3> FireButtonPressed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FireButtonPressed != null)
            {
                FireButtonPressed(GetCurrentClickPoint(Input.mousePosition));
            }
        }
    }

    Vector3 GetCurrentClickPoint(Vector3 mousePosition)
    {
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); //Camera가 Perspective 설정시 z값을 넣어야한다. //Camera가 Ortho일 경우는 필요없음.
        Vector3 point = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log(point);
        point.z = 0f;
        return point;
    }
}
