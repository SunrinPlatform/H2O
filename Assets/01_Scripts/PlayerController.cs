using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;         // �÷��̾� �̵� �ӵ�
    [SerializeField]
    private float jumpForce = 10f;        // �÷��̾� ���� ��
    [SerializeField]
    private LayerMask groundLayer;        // Ground ���̾ ������ �� �ֵ��� �߰�
    private bool isGrounded;                               // �÷��̾ �ٴڿ� ��Ҵ��� Ȯ���ϴ� ����
    private float horizontal;                              // �¿� �Է� ������ ���� ����
    private Rigidbody2D rb2D;                              // 2D ���� ó���� ���� ������ٵ�
    private Animator animator;                             // �ִϸ��̼��� �����ϴ� ������Ʈ
    private float groundCheckRadius = 0.2f;                // �ٴ� ������ ���� ���� ������
    private Transform groundCheck;                         // ĳ���� �Ʒ��� �ִ� �ٴ� ���� ��ġ

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();                // Rigidbody2D ������Ʈ ��������
        animator = GetComponent<Animator>();               // Animator ������Ʈ ��������
        groundCheck = transform.Find("GroundCheck");       // GroundCheck��� �̸��� ���� ������Ʈ �������� (�ٴ� ������)
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");       // �¿� �Է� ���� (���� �Է�)
        Jump();                                            // ���� �Լ� ȣ��
        UpdateAnimation();                                 // �ִϸ��̼� ���� ������Ʈ
        Reverse();                                         // �÷��̾� ���� ����
    }

    // FixedUpdate is called at a fixed time interval (used for physics updates)
    void FixedUpdate()
    {
        Move();                                            // �÷��̾� �̵� ó��
        CheckGrounded();                                   // �ٴڿ� ��Ҵ��� Ȯ��
    }

    // �÷��̾��� �̵��� ó���ϴ� �Լ�
    void Move()
    {
        rb2D.velocity = new Vector2(horizontal * moveSpeed, rb2D.velocity.y); // �¿� �̵� ó��
    }

    // ���� ó���� ���� �Լ�
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)  // �����̽��ٸ� ������ ���� ���� ���� ����
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // �������� ���� �־� ����
        }
    }

    // �ִϸ��̼� ���� ������Ʈ �Լ�
    void UpdateAnimation()
    {
        animator.SetBool("isMove", horizontal != 0);       // �¿� �̵� �� �̵� �ִϸ��̼� ����
        animator.SetBool("isJump", !isGrounded);           // ���� ���� �� ���� �ִϸ��̼� ����
    }

    // ĳ���� ������ �¿�� ������Ű�� �Լ�
    void Reverse()
    {
        if (horizontal < 0)                                // �������� �̵� ���� ��
        {
            transform.localScale = new Vector3(-7, 7, 1);  // ĳ���͸� �������� ����
        }
        else if (horizontal > 0)                           // ���������� �̵� ���� ��
        {
            transform.localScale = new Vector3(7, 7, 1);   // ĳ���͸� ���������� ����
        }
    }

    // �ٴڿ� ��Ҵ��� Ȯ���ϴ� �Լ� (Raycast �Ǵ� OverlapCircle ���)
    void CheckGrounded()
    {
        // GroundCheck ��ġ���� ���� ������ �ٴڿ� ��Ҵ��� üũ
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // �浹�� ���۵� �� ȣ��Ǵ� �Լ� (Ground���� �浹�� ó��)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))     // �浹�� ������Ʈ�� "Ground" �±׸� ���� ���� ó��
        {
            isGrounded = true;                             // �ٴڿ� ��Ҵٰ� ����
        }
    }

    // �浹�� ���� �� ȣ��Ǵ� �Լ� (Ground���� �浹�� ó��)
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))     // �ٴڿ��� ����� �� ó��
        {
            isGrounded = false;                            // �ٴڿ��� �������ٰ� ����
        }
    }

    // �ٴ� ���� ������ ����Ƽ �����Ϳ��� �ð������� Ȯ���ϱ� ���� �Լ�
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;                      // Gizmo ������ ���������� ����
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // GroundCheck ��ġ�� ���� ������ �׷���
        }
    }
}
