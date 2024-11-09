using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float velocity = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(Shoot), 1f, 1f);
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0f);
    }
}
