using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticlesFollower : MonoBehaviour
{
    public Transform particleOffset;
    public float offsetDistance = 0.2f;
    public Rigidbody2D rb;

    void LateUpdate()
    {
        Vector2 moveDir = rb.velocity.normalized;
        particleOffset.localPosition = -(Vector3)moveDir * offsetDistance;
    }
}
