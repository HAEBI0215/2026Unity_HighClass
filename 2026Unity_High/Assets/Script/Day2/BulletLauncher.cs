using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    void Update()
    {
        
    }

    public void OnFireButtonPressed(Vector3 vPosition)
    {
        Debug.Log("발사" + vPosition);
    }
}
