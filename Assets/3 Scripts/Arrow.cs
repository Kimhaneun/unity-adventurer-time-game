using UnityEngine;

public class Arrow : MonoBehaviour
{
    Camera mainCamera;
    Vector3 dir;
    Vector2 point;
    float speed = 15f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void Start()
    {
        Destroy(gameObject, 30f);
        PlayerPoint();

    }

    void Update()
    {
        transform.position += dir.normalized * Time.deltaTime * speed;
    }

    public void ArrowMove(Vector2 insDir)
    {
        dir = insDir - (Vector2)transform.position;
    }

    void PlayerPoint()
    {
        Vector2 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        point = new Vector2(worldPoint.x - transform.position.x, worldPoint.y - transform.position.y);

        transform.right = point;
    }
}
