using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class Player : Unit
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    private bool isMove;
    float horizontal;
    Transform transform;
    Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
       rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        UpdateAnimation();
        Reverse();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0) {
            isMoving = true;
            rigidbody2D.velocity = new Vector2(horizontal * moveSpeed, rigidbody2D.velocity.y);
        }
        else {
            if (isMoving) {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            }

            isMoving = false;
        }
        
    }
    void Jump()
    {
        if (  Input.GetKeyDown(KeyCode.Space)  && isGrounded )
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
           
    }
    void UpdateAnimation()
    {
        if(horizontal != 0)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);

        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
    }
    void Reverse()
    {
        if (horizontal == -1)
        {
            transform.localScale = new Vector3(-5f, 5f, 1);
        }
        else if (horizontal == 1)
        {
            transform.localScale = new Vector3(5f, 5f, 1);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // �浹�� ������Ʈ�� "Ground" �±׸� ���� ���
        {
            isGrounded = true; // ���� �پ� �ִ� ���·� ����
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // �浹�� ������Ʈ�� "Ground" �±׸� ���� ���
        {
            isGrounded = false; // ������ ��� ���·� ����
        }
    }

    void Hurt(int amount) {
        if (amount > defence) {
            currentHealth -= 1;
            // play sound

            if (currentHealth <= 0) {
                Death();
            }
        }
        else {
            // play sound
        }
    }

    void Death() {
        // play sound
    }
}
