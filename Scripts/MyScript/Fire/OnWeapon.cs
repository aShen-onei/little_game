using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWeapon : MonoBehaviour
{
    // this is bullet prefab
    public GameObject Bullet;
    public Transform SpawnPoint;
    public float Frequency = 10f;
    public float ConAngle = 1.5f;
    public int Damage = 10;
    private bool Firing = false;
    private float LastFireTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        if(SpawnPoint == null)
        {
            SpawnPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Firing)
        {
            if(Time.time > LastFireTime + 1 / Frequency)
            {
                var randomRotation = Quaternion.Euler(Random.Range(-ConAngle, ConAngle), Random.Range(-ConAngle, ConAngle), 0);
                // GameObject blt = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation * randomRotation);
                GameObject blt = BulletCache.Create(Bullet, SpawnPoint.position, SpawnPoint.rotation * randomRotation);
                var bullet = blt.GetComponent<BulletNew>();
                bullet.BulletDamage = Damage;
                bullet.BulletFireDistance = 1000;
                LastFireTime = Time.time;
            }
        }
    }

    void OnFire()
    {
        Firing = true;
    }

    void StopFire()
    {
        Firing = false;
    }
}
