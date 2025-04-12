using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject bullet;
    public float bulletSpeed = 5f;
    public bool facingRight = true;

    public void Shoot()
    {
        GameObject spawnedBullet = Instantiate(
            bullet,
            transform.position,
            Quaternion.identity);


        Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
        float direction = facingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * bulletSpeed, 0f);

        Destroy(spawnedBullet, 3f); //Destroy bullet after some time

    }

}
