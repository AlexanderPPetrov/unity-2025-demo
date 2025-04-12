using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Movement settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 2f;

    [Header("Grounded settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;


    private bool _isGrounded = false;
    private Shooter _shooter;

    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private DirectionController _directionController;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _shooter = GetComponentInChildren<Shooter>();
        _directionController = GetComponent<DirectionController>();
    }

    // Update is called once per frame
    void Update()
    {

        //TODO jump logic with ground check and ground layer
        float moveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector2(moveX, 0).normalized;

        if(moveDirection.x != 0)
        {
            _shooter.facingRight = moveDirection.x > 0;
        }

        _directionController.FaceDirection(moveX);

        if (Input.GetKeyDown(KeyCode.F))
        {
            _shooter.Shoot();
        }

    }


    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, 0);
    }

}
