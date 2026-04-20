using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Explosion : RecycleObject
{
    CircleCollider2D box;
    Rigidbody2D body;

    [SerializeField]
    float timeToRemove = 1f;
    float elapsedTime = 0f;

    private void Awake()
    {
        box = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();

        box.isTrigger = true;
        body.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= timeToRemove)
            {
                elapsedTime = 0f;
                DestroySelf();
            }
        }
    }

    void DestroySelf()
    {
        isActivated = false;
        //Debug.Log(gameObject.name + "is destroyed!");
        //if(Destroyed != null)
        //{
        //    Destroyed(this);
        //}
        Destroyed?.Invoke(this);
    }
}
