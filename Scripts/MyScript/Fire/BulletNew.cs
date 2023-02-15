using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNew : MonoBehaviour
{
    public float BulletSpeed = 10f;
    public float BulletLifeTime = 0.5f;
    public float BulletFireDistance = 1000;
    [HideInInspector]
    public int BulletDamage = 10;
    private float SpawnTime = 0.0f;
    private Transform trans;
    private Health _health;
    // Start is called before the first frame update
    private void OnEnable()
    {
        trans = transform;
        SpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        trans.position += BulletSpeed * Time.deltaTime * trans.forward;
        BulletFireDistance -= BulletSpeed * Time.deltaTime;
        if(Time.time > SpawnTime + BulletLifeTime || BulletFireDistance < 0)
        {
            // Destroy(gameObject);
            BulletCache.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "AI_Trigger")
        {
            BulletCache.Destroy(gameObject);
            _health = other.GetComponent<Health>();
            if(_health != null)
            {
                _health.OnDamage(BulletDamage);
            }
        }
    }
}
