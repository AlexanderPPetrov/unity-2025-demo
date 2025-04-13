using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float patrolDistance = 5f;
    public float patrolSpeed = 2f;


    private Vector3 _startPosition;
    private int _direction = 1;
    private DirectionController _directionController;


    //Enemy AI
    //Damager script
    //Mortality script
    //Health bar
    private void Awake()
    {
        _startPosition = transform.position;
        Debug.Log("Enempy patrol awake");
        _directionController = GetComponent<DirectionController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Enemy patrol start");
    }

    // Update is called once per frame
    void Update()
    {
        float move = patrolSpeed * Time.deltaTime * _direction;
        transform.Translate(Vector2.right * move);

        _directionController?.FaceDirection(_direction);
        

        float distanceFromStart = Vector2.Distance(_startPosition, transform.position);

        if(distanceFromStart >= patrolDistance)
        {
            _direction *= -1;
            _startPosition = transform.position;
        }
    }

    void FixedUpdate()
    {
        
    }

    void LateUpdate()
    {
        
    }
}
