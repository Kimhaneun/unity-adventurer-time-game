using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Player player;     // player의 위치
    [SerializeField] Bow bow;
    [SerializeField] Boss boss;         // boss의 위치
    [SerializeField] GameManager gameManager;

    Vector3 distance;                   // target 과 camera 의 거리 차이  
    float lerp = 5f;                    // 보간하는 값

    private Vector3 playerPos;          // target = player

    float duration = 1;                              //지속시간
    Vector3 strength = new Vector3(1, 1, 1);         //카메라 흔들리는 범위
    int vibrato = 10;                                //세기?였던걸로기억함
    float randomness = 5;                            //어디로튈지일걸?

    bool play = true;                                // 아직 boss를 만나지 못한 상태

    void LateUpdate()
    {
        TargetPlayer();
    }

    void TargetPlayer()
    {
        if (play == true)
        {
            distance = new Vector3(0, 0, -10f);
            playerPos = player.transform.position + distance;
            transform.position = Vector3.Lerp(transform.position, playerPos, lerp * Time.deltaTime);
        }
    }

    IEnumerator BossCameraStart()
    {
        play = false; // player 따라가는 것을 멈춤
        Vector3 bossPos = new Vector3(boss.transform.position.x, boss.transform.position.y, -10);
        transform.position = bossPos; // 카메라를 boss에게 맞춤

        yield return new WaitForSeconds(1.5f);
        boss.BossStart();       // boss 공격 animator
        yield return new WaitForSeconds(0.3f);
        transform.DOShakePosition(duration, strength, vibrato, randomness);     // 카메라 흔들림

        yield return new WaitForSeconds(2.5f);
        play = true; // player 따라가기를 다시 시작
        
        player.playerBossStart = false;
        bow.bowBossStart = false;

        gameManager.bossUI.SetActive(true);
    }

    public void PlayerHit()
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness);
    }
}
