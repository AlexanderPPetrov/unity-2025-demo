using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Movement settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 7f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float jumpForce = 2f;

    [Header("Grounded settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;


    private bool _isGrounded = false;
    private bool _canDoubleJump = false;
    //private bool _isRunning = false;
    private float _currentSpeed = 0f;
    private float _targetSpeed = 0f;

    private AudioSource _shootSound;


    private Shooter _shooter;

    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private DirectionController _directionController;

    public Animator animator;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _shooter = GetComponentInChildren<Shooter>();
        _directionController = GetComponent<DirectionController>();
        _shootSound = GetComponent<AudioSource>();

    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float speed = isRunning ? runSpeed : walkSpeed;
        _targetSpeed = moveX * speed;


        float accelerationRate = (Mathf.Abs(_targetSpeed) > 0) ? acceleration : deceleration;

        _currentSpeed =
            Mathf.MoveTowards(_currentSpeed, _targetSpeed, accelerationRate * Time.deltaTime);

        if (moveX != 0)
        {
            _shooter.facingRight = moveX > 0;
        }

        _directionController.FaceDirection(moveX);


        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        if (Input.GetKeyDown(KeyCode.F))
        {
            _shooter.Shoot();
            animator.SetTrigger("Attack");
            _shootSound.Play();
        }

        if (_isGrounded)
        {
            _canDoubleJump = true;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (_isGrounded)
            {
                Jump();
            } else if (_canDoubleJump)
            {
                Jump();
                _canDoubleJump = false;
            }

        }


        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }


    void FixedUpdate()
    {
        rb.velocity = new Vector2(_currentSpeed, rb.velocity.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("HealthCollectable"))
        {
            Debug.Log("Health");
        }
    }

}
