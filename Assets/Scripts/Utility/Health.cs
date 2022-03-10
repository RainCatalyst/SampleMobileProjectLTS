using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int hp = 1;
    public UnityAction<int> OnHpUpdated;
    public UnityAction<int> OnDamageReceived;
    public UnityAction OnDeath;

    public int Hp 
    {
        get => hp;
        set
        {
            hp = value;
            OnHpUpdated?.Invoke(hp);
        }
    }

    public void DealDamage(int damage)
    {
        Hp -= damage;
        OnDamageReceived?.Invoke(damage);
        if (Hp <= 0)
            Die();
    }

    public void Die()
    {
        Hp = 0;
        OnDeath?.Invoke();
    }
}
