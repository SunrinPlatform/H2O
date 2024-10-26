using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    private bool isMove;
    float horizontal;
    Transform transform;
    Rigidbody2D rigidbody2D;
    private Animator animator;


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

        //print(horizontal);
        
        rigidbody2D.velocity = new Vector2(horizontal * moveSpeed*Time.deltaTime, rigidbody2D.velocity.y);
        
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
            transform.localScale = new Vector3(-7, 7, 1);
        }
        else if (horizontal == 1)
        {
            transform.localScale = new Vector3(7, 7, 1);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 충돌한 오브젝트가 "Ground" 태그를 가진 경우
        {
            isGrounded = true; // 땅에 붙어 있는 상태로 설정
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 충돌한 오브젝트가 "Ground" 태그를 가진 경우
        {
            isGrounded = false; // 땅에서 벗어난 상태로 설정
        }
    }
}
