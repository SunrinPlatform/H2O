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

    [SerializeField] GameObject player;

    [SerializeField] bool isTouchedPlayer = false;
    private bool Sponge;
    Rigidbody2D playerRigidbody2D;
    SpriteRenderer color;
    [SerializeField] bool isPlayerInSponge = false;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>(); 
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Absorption();
    }
void Absorption()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        print("pressed");
        // ���� �÷��̾ ������ �ȿ� �� �ִٸ�
        if (isPlayerInSponge)
        {
            if (shotDirection == Direction.East)
            {
                print("�߻�!����");
                player.SetActive(true);
                player.transform.position = transform.position;
                print("asdf" + shootingSpeed);
                //player.transform.position += Vector3.right * shootingSpeed;
                playerRigidbody2D.velocity = Vector2.right * shootingSpeed;
                color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                isPlayerInSponge = false; // �߻� �� �������� ������������ ǥ��
            }

            else if (shotDirection == Direction.West)
            {
                print("�߻�!����");
                player.SetActive(true);
                player.transform.position = transform.position;
                print("asdf" + shootingSpeed);
                player.transform.position += Vector3.left * shootingSpeed;
                color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                isPlayerInSponge = false; // �߻� �� �������� ������������ ǥ��
            }
            else if (shotDirection == Direction.North)
            {
                print("hhhhhhh �߻�!����");
                player.SetActive(true);
                player.transform.position = transform.position;
                print("asdf" + shootingSpeed);
                player.transform.position += Vector3.up * shootingSpeed;
                color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                isPlayerInSponge = false; // �߻� �� �������� ������������ ǥ��
            }
            else if (shotDirection == Direction.South)
            {
                print("�߻�!����");
                player.SetActive(true);
                player.transform.position = transform.position;
                print("asdf" + shootingSpeed);
                player.transform.position += Vector3.down * shootingSpeed;
                color.color = new Color(255f / 255f, 242f / 255f, 114f / 255f);
                isPlayerInSponge = false; // �߻� �� �������� ������������ ǥ��
            }
               
        }
        else if (isTouchedPlayer)
        { 
                player.SetActive(false);
                color.color = new Color(114f/255f,235f/255f,255f/255f);
            isPlayerInSponge = true; // �÷��̾ ������ �ȿ� ����
            
        }
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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

