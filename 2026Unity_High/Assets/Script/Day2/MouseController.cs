using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseController : MonoBehaviour, IGameController
{
    public Action<Vector3> FireButtonPressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (FireButtonPressed != null)
                FireButtonPressed(Vector3.zero);
        }
    }
}
