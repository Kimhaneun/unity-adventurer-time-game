using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Player player;     // player�� ��ġ
    [SerializeField] Bow bow;
    [SerializeField] Boss boss;         // boss�� ��ġ
    [SerializeField] GameManager gameManager;

    Vector3 distance;                   // target �� camera �� �Ÿ� ����  
    float lerp = 5f;                    // �����ϴ� ��

    private Vector3 playerPos;          // target = player

    float duration = 1;                              //���ӽð�
    Vector3 strength = new Vector3(1, 1, 1);         //ī�޶� ��鸮�� ����
    int vibrato = 10;                                //����?�����ɷα����
    float randomness = 5;                            //����ƥ���ϰ�?

    bool play = true;                                // ���� boss�� ������ ���� ����

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
        play = false; // player ���󰡴� ���� ����
        Vector3 bossPos = new Vector3(boss.transform.position.x, boss.transform.position.y, -10);
        transform.position = bossPos; // ī�޶� boss���� ����

        yield return new WaitForSeconds(1.5f);
        boss.BossStart();       // boss ���� animator
        yield return new WaitForSeconds(0.3f);
        transform.DOShakePosition(duration, strength, vibrato, randomness);     // ī�޶� ��鸲

        yield return new WaitForSeconds(2.5f);
        play = true; // player ���󰡱⸦ �ٽ� ����
        
        player.playerBossStart = false;
        bow.bowBossStart = false;

        gameManager.bossUI.SetActive(true);
    }

    public void PlayerHit()
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness);
    }
}
