using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    East,
    West,
    North,
    South,
    None
}

public class SpongeController : MonoBehaviour
{
    [SerializeField]
    float shootingSpeed;
    public Direction shotDirection = Direction.None;

    [SerializeField]
    GameObject player;

    [SerializeField]
    bool isTouchedPlayer = false;
    private bool Sponge;
    Rigidbody2D playerRigidbody2D;
    SpriteRenderer color;
    [SerializeField]
    bool isPlayerInSponge = false;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Absorption();
    }
    void Absorption()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("pressed");
            // 만약 플레이어가 스폰지 안에 들어가 있다면
            if (isPlayerInSponge)
            {
                if (shotDirection == Direction.East)
                {
                    print("발사!히히");
                    player.SetActive(true);
                    player.transform.position = transform.position;
                    print("asdf" + shootingSpeed);
                    playerRigidbody2D.AddForce(Vector2.right * shootingSpeed, ForceMode2D.Impulse);
                    color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                    isPlayerInSponge = false; // 발사 후 스폰지를 빠져나왔음을 표시
                }

                else if (shotDirection == Direction.West)
                {
                    print("발사!히히");
                    player.SetActive(true);
                    player.transform.position = transform.position;
                    print("asdf" + shootingSpeed);
                    player.transform.position += Vector3.left * shootingSpeed;
                    color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                    isPlayerInSponge = false; // 발사 후 스폰지를 빠져나왔음을 표시
                }
                else if (shotDirection == Direction.North)
                {
                    print("hhhhhhh 발사!히히");
                    player.SetActive(true);
                    player.transform.position = transform.position;
                    print("asdf" + shootingSpeed);
                    player.transform.position += Vector3.up * shootingSpeed;
                    color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                    isPlayerInSponge = false; // 발사 후 스폰지를 빠져나왔음을 표시
                }
                else if (shotDirection == Direction.South)
                {
                    print("발사!히히");
                    player.SetActive(true);
                    player.transform.position = transform.position;
                    print("asdf" + shootingSpeed);
                    player.transform.position += Vector3.down * shootingSpeed;
                    color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                    isPlayerInSponge = false; // 발사 후 스폰지를 빠져나왔음을 표시
                }

            }
            else if (isTouchedPlayer)
            {
                player.SetActive(false);
                color.color = new Color(114f / 255f, 235f / 255f, 255f / 255f);
                isPlayerInSponge = true; // 플레이어를 스폰지 안에 넣음

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouchedPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouchedPlayer = false;

        }
    }


}