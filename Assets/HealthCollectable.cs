using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
   
    public float health = 20f;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    //TODO add healthbars for enemies
    //TODO add point cointer UI
    //TODO add start screen + game end

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Mortality mortality = collision.GetComponent<Mortality>();

            if(mortality != null)
            {
                mortality.GiveHp(health);
            }

            if(_audioSource != null)
            {
                _audioSource.Play();
            }


            StartCoroutine(DestroyAfterDelay(2f));
            _spriteRenderer.enabled = false;

        }
    }

    IEnumerator DestroyAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        Destroy(gameObject);
    }
}
