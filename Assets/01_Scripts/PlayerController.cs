using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;         // 플레이어 이동 속도
    [SerializeField]
    private float jumpForce = 10f;        // 플레이어 점프 힘
    [SerializeField]
    private LayerMask groundLayer;        // Ground 레이어를 선택할 수 있도록 추가
    private bool isGrounded;                               // 플레이어가 바닥에 닿았는지 확인하는 변수
    private float horizontal;                              // 좌우 입력 감지를 위한 변수
    private Rigidbody2D rb2D;                              // 2D 물리 처리를 위한 리지드바디
    private Animator animator;                             // 애니메이션을 관리하는 컴포넌트
    private float groundCheckRadius = 0.2f;                // 바닥 감지를 위한 원의 반지름
    private Transform groundCheck;                         // 캐릭터 아래에 있는 바닥 감지 위치

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();                // Rigidbody2D 컴포넌트 가져오기
        animator = GetComponent<Animator>();               // Animator 컴포넌트 가져오기
        groundCheck = transform.Find("GroundCheck");       // GroundCheck라는 이름의 하위 오브젝트 가져오기 (바닥 감지용)
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");       // 좌우 입력 감지 (수평 입력)
        Jump();                                            // 점프 함수 호출
        UpdateAnimation();                                 // 애니메이션 상태 업데이트
        Reverse();                                         // 플레이어 방향 반전
    }

    // FixedUpdate is called at a fixed time interval (used for physics updates)
    void FixedUpdate()
    {
        Move();                                            // 플레이어 이동 처리
        CheckGrounded();                                   // 바닥에 닿았는지 확인
    }

    // 플레이어의 이동을 처리하는 함수
    void Move()
    {
        rb2D.velocity = new Vector2(horizontal * moveSpeed, rb2D.velocity.y); // 좌우 이동 처리
    }

    // 점프 처리를 위한 함수
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)  // 스페이스바를 누르고 땅에 있을 때만 점프
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // 위쪽으로 힘을 주어 점프
        }
    }

    // 애니메이션 상태 업데이트 함수
    void UpdateAnimation()
    {
        animator.SetBool("isMove", horizontal != 0);       // 좌우 이동 시 이동 애니메이션 적용
        animator.SetBool("isJump", !isGrounded);           // 점프 중일 때 점프 애니메이션 적용
    }

    // 캐릭터 방향을 좌우로 반전시키는 함수
    void Reverse()
    {
        if (horizontal < 0)                                // 왼쪽으로 이동 중일 때
        {
            transform.localScale = new Vector3(-7, 7, 1);  // 캐릭터를 왼쪽으로 반전
        }
        else if (horizontal > 0)                           // 오른쪽으로 이동 중일 때
        {
            transform.localScale = new Vector3(7, 7, 1);   // 캐릭터를 오른쪽으로 반전
        }
    }

    // 바닥에 닿았는지 확인하는 함수 (Raycast 또는 OverlapCircle 사용)
    void CheckGrounded()
    {
        // GroundCheck 위치에서 원형 범위로 바닥에 닿았는지 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // 충돌이 시작될 때 호출되는 함수 (Ground와의 충돌만 처리)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))     // 충돌한 오브젝트가 "Ground" 태그를 가질 때만 처리
        {
            isGrounded = true;                             // 바닥에 닿았다고 설정
        }
    }

    // 충돌이 끝날 때 호출되는 함수 (Ground와의 충돌만 처리)
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))     // 바닥에서 벗어났을 때 처리
        {
            isGrounded = false;                            // 바닥에서 떨어졌다고 설정
        }
    }

    // 바닥 감지 범위를 유니티 에디터에서 시각적으로 확인하기 위한 함수
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;                      // Gizmo 색상을 빨간색으로 설정
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // GroundCheck 위치에 원형 범위를 그려줌
        }
    }
}
