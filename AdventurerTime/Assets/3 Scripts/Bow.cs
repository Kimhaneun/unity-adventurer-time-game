using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Camera mainCamera;
    public Arrow arrowPrefab;       // arrow �� �����ϴ� Prefab
    float arrowDelayTime = 0.2f;      // arrow ������ �� DelayTime
    public bool bowBossStart = false;

    public Vector2 point;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        PlayerPoint();
    }

    private void OnEnable()     // �ش� ��ũ��Ʈ�� Ȱ��ȭ�� �� (gmae object�� true �� �Ǿ��� ��)ȣ��Ǵ� �Լ�
    {
            StartCoroutine(PlayerFire());
    }

    void PlayerPoint()
    {
        Vector2 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        point = new Vector2(worldPoint.x - transform.position.x, worldPoint.y - transform.position.y);

        transform.right = point;
    }

    IEnumerator PlayerFire()
    {
            while (true)
            {
                if (Input.GetMouseButton(0) && bowBossStart == false)        // ���콺 ���� ��ư�� ������ �ִ� ������ ó��
                {
                    Vector2 mousePosition = Input.mousePosition;
                    // ���콺 ��ġ�� ��ũ�� ��ǥ�� ����

                    Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    // ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ

                    Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                    arrow.ArrowMove(worldPosition);

                    yield return new WaitForSeconds(arrowDelayTime);
                }
                yield return null;
            }
    }
}
