using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生命脚本
/// </summary>
public class Health : MonoBehaviour
{

    public int MaxHealth = 100;
    public int CurHealth = 100;
    public SignalSender DamageSignal;
    public SignalSender DeathSignal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        MaxHealth = 100;
    }
    /// <summary>
    /// 受到伤害，统一发送给GameController
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage = 1)
    {
        CurHealth -= damage;
        DamageSignal.SendSignals(this, this);
        if (CurHealth <= 0) DeathSignal.SendSignals(this, gameObject);

    }
}
