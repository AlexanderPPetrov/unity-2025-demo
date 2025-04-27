using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mortality : MonoBehaviour
{

    public float hp;
    public float maxHp;
    public float destroyDelay = 0f;


    public Animator animator;
    public AudioSource soundOnDie;
    public AudioSource soundOnDamage;

    [Header("Health Bar")]
    public GameObject healthBarPrefab;
    public float verticalOffset;

    private Image _healthFill;
    private GameObject _healthBarInstance;
    private Transform _healthBarTransform;

    void Awake()
    {
        maxHp = hp;

        if(healthBarPrefab != null)
        {
            _healthBarInstance = Instantiate(healthBarPrefab, transform.position + new Vector3(0, verticalOffset, 0), Quaternion.identity);
            _healthBarTransform = _healthBarInstance.transform;
            _healthFill = _healthBarInstance.transform.Find("Fill").GetComponent<Image>();
            _healthBarInstance.transform.SetParent(null); 
        }

    }

    public void GiveHp(float giveHp)
    {
        hp += giveHp;
        if(hp > maxHp)
        {
            hp = maxHp;
        }

        UpdateHealthBar();
    }

    public void TakeHp(float takeHp)
    {
        hp -= takeHp;

        UpdateHealthBar();

        if (soundOnDamage != null)
        {
            soundOnDamage.Play();
        }

        Debug.Log("Damage: " + takeHp);
        if (hp <= 0f)
        {
            DieHandler();
        }
    }

    private void UpdateHealthBar()
    {
        if(_healthFill != null)
        {
            _healthFill.fillAmount = Mathf.Clamp01(hp / maxHp);
        }
    }

    private void DieHandler()
    {

        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector2.zero;

        if (animator != null)
        {
            animator.SetTrigger("Die");
            if(soundOnDie != null)
            {
                soundOnDie.Play();
            }
        }

        StartCoroutine(DestroyWithDelay(destroyDelay));
       
    }

    private IEnumerator DestroyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        Destroy(_healthBarInstance);
    }

    public void LateUpdate()
    {
        if(_healthBarTransform != null)
        {
            Debug.Log("Healthbar instance: " + _healthBarTransform);
            _healthBarTransform.position = transform.position + new Vector3(0, verticalOffset, 0);
            _healthBarTransform.rotation = Quaternion.identity;

            Debug.Log("healthBarInstance.position" + _healthBarTransform.position);
        }
    }

}
