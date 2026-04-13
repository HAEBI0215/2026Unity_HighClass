using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IGameController
{
    public Action<Vector3> FireButtonPressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (FireButtonPressed != null)
                FireButtonPressed(Vector3.zero);
        }
    }
}
