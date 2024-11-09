using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health = 50;

    public UnityEvent onDefeat;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            onDefeat.Invoke();
        }
    }
}
