using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 10f;


    public bool playerBossStart = false;

    public int playerHP;

    [SerializeField] GameManager gameManager;
    [SerializeField] MainCamera mainCamera;


    private void Awake()
    {
        transform.position = new Vector3(0, -9f, 0);
        playerHP = gameManager.maxPlayerHP;
    }

    void Update()
    {
        PlayerMove();
        MovementRange();
    }

    void PlayerMove()
    {
        if (this != null && playerBossStart == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(x, y, 0) * Time.deltaTime * speed;

            if (x <= -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (x >= 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }



    void MovementRange()
    {
        if (transform.position.y > 11.5f)      //╩С
        {
            transform.position = new Vector3(transform.position.x, 11.5f, transform.position.z);
        }

        if (transform.position.y < -11.5f)     // го
        {
            transform.position = new Vector3(transform.position.x, -11.5f, transform.position.z);
        }

        if (transform.position.x < -11.5f)     // аб
        {
            transform.position = new Vector3(-11.5f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 11.5f)       // ©Л
        {
            transform.position = new Vector3(11.5f, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FirePurple"))
        {
            playerHP -= 10;
            mainCamera.PlayerHit();
        }

        if (collision.CompareTag("FireGreen"))
        {
            playerHP -= 20;
            mainCamera.PlayerHit();
        }
    }
}
