using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;

    [SerializeField]
    Explosion explosionPrefab;

    [SerializeField]    
    Transform firePosition;

    [SerializeField]
    float fireDelay = 0.5f;
    float elapsedFireTime;
    bool canShoot = true;

    Factory bulletFactory;
    Factory explosionFactory;

    private void Start()
    {
        bulletFactory = new Factory(bulletPrefab);
        explosionFactory = new Factory(explosionPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            elapsedFireTime += Time.deltaTime;
            if(elapsedFireTime >= fireDelay)
            {
                canShoot = true;
                elapsedFireTime = 0f;
            }
        }
    }

    public void OnFireButtonPressed(Vector3 position)
    {
        //Debug.Log("Fired a bullet!"+ position);
        if (!canShoot) return;

        //bullet = Instantiate(bulletPrefab);
        RecycleObject bullet = bulletFactory.Get();
        //bullet.transform.position = firePosition.position;
        bullet.Activate(firePosition.position, position);
        bullet.Destroyed += OnBulletDestroyed;

        canShoot = false;
    }

    void OnBulletDestroyed(RecycleObject usedBullet)
    {
        Vector3 lastBulletPosion = usedBullet.transform.position;
        usedBullet.Destroyed -= OnBulletDestroyed;
        bulletFactory.Restore(usedBullet);

        RecycleObject explosion = explosionFactory.Get();
        //explosion.transform.position = lastBulletPosion;
        explosion.Activate(lastBulletPosion);
        explosion.Destroyed += OnExplosionDestroyed;
    }

    void OnExplosionDestroyed(RecycleObject usedExplosion)
    {
        usedExplosion.Destroyed -= OnExplosionDestroyed;
        explosionFactory.Restore(usedExplosion);
    }
}
