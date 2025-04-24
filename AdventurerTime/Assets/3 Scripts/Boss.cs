using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameManager gameManager;
    [SerializeField] Bow bow;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private System.Random random;
    private float timer;
    private float interval = 1.5f;

    public GameObject fireGreenPrefab;
    int numFireGreen = 12;
    float fireGreenSpeed = 8f;
    float fireGreenSpreadAngle = 360f;

    public GameObject firePurplePrefab;
    int numFirePurple = 6;
    float firePurpleSpeed = 8f;
    public float firePurpleSpreadRadius = 0.05f;

    public int bossHP;


    private void Awake()
    {
        transform.position = new Vector3(0, 0, 0);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        bossHP = gameManager.maxBossHP;
    }

    void Start()
    {
        random = new System.Random();
        timer = interval;
    }


    void Update()
    {
        BossFlipX();
        BoosAttack();
    }

    void BossFlipX()
    {
        if (player.transform.position.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        else if (player.transform.position.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void BossStart()
    {
        animator.SetTrigger("onAttack1");
        animator.SetTrigger("onIdle");

    }

    void BoosAttack()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && player.playerBossStart == false && bow.bowBossStart == false && gameManager.once == false)
        {
            BossPattern();
            timer = interval;
        }
    }

    void BossPattern()
    {
        int randomIndex = random.Next(0, 2);
        switch (randomIndex)
        {
            case 0:
                Attack1();
                break;
            case 1:
                Attack2();
                break;

            default:
                Debug.LogError("Invalid random index!");
                break;
        }
    }

    void Attack1()
    {
        animator.SetTrigger("onAttack1");

        float angleStep = 360f / numFirePurple;
        float currentAngle = 0f;

        for (int i = 0; i < numFirePurple; i++)
        {
            GameObject bullet = Instantiate(firePurplePrefab, transform.position, Quaternion.identity);

            Vector2 direction = (player.transform.position - transform.position).normalized;

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = direction * firePurpleSpeed;

            float xOffset = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * firePurpleSpreadRadius;
            float yOffset = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * firePurpleSpreadRadius;
            bullet.transform.position += new Vector3(xOffset, yOffset, 0f);

            currentAngle += angleStep;
        }

        animator.SetTrigger("onIdle");
    }

    void Attack2()
    {
        animator.SetTrigger("onAttack2");

        float angleStep = fireGreenSpreadAngle / numFireGreen;
        float currentAngle = 0f;

        for (int i = 0; i < numFireGreen; i++)
        {

            GameObject bullet = Instantiate(fireGreenPrefab, transform.position, Quaternion.identity);

            bullet.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = bullet.transform.up * fireGreenSpeed;

            currentAngle += angleStep;
        }

        animator.SetTrigger("onIdle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            bossHP -= 2;
        }
    }
}
