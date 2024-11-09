using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyAfterTime(3f));
    }

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (!collided.gameObject.TryGetComponent<Health>(out var health)) return;
        
        health.TakeDamage(5);
        Destroy(this.gameObject);
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
