using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Camera mainCamera;
    public Arrow arrowPrefab;       // arrow 를 생성하는 Prefab
    float arrowDelayTime = 0.2f;      // arrow 생성할 때 DelayTime
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

    private void OnEnable()     // 해당 스크립트가 활성화될 때 (gmae object가 true 가 되었을 때)호출되는 함수
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
                if (Input.GetMouseButton(0) && bowBossStart == false)        // 마우스 왼쪽 버튼을 누르고 있는 도중의 처리
                {
                    Vector2 mousePosition = Input.mousePosition;
                    // 마우스 위치를 스크린 좌표로 얻음

                    Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    // 스크린 좌표를 월드 좌표로 변환

                    Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                    arrow.ArrowMove(worldPosition);

                    yield return new WaitForSeconds(arrowDelayTime);
                }
                yield return null;
            }
    }
}
