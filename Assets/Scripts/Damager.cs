using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{

    public float damagePoints = 10f;
    public GameObject objectToSpawn;
    public bool destroyOnCollision = false;

    public void OnCollisionEnter2D(Collision2D other)
    {

        Debug.Log(other);
        Mortality mortality = other.collider.GetComponent<Mortality>();

        if(objectToSpawn != null)
        {
            Vector2 position = other.contacts[0].point;
            Instantiate(objectToSpawn, position, Quaternion.identity);
        }

        if(mortality != null)
        {
            mortality.TakeHp(damagePoints);
        }

        if(destroyOnCollision)
        {
            Destroy(this.gameObject);
        }
    }


}
