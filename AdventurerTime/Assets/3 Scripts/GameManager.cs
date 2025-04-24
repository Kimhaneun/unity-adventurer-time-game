using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool once = true;
    public int maxPlayerHP = 100;
    public int currentPlayerHP;
    public int maxBossHP = 100;
    public int currentBossHP;
    [SerializeField] Player player;
    [SerializeField] Bow bow;
    [SerializeField] Boss boss;
    [SerializeField] MainCamera mainCamera;
    [SerializeField] Slider playerHPSlider;
    [SerializeField] Slider bossHPSlider;
    public GameObject bossUI;

    private void Awake()
    {
        currentPlayerHP = maxPlayerHP;
        UpdatePlayerHP();

        currentBossHP = maxBossHP;
        UpdateBossHP();

        bossUI.SetActive(false);
    }
    
    void Update()
    {
        SetPlayerHP();
        SetBoosHP();
    }

    void LateUpdate()
    {
        BossStart();
    }

    void BossStart()
    {
        if (once == true && player.transform.position.y > -5)
        {
            player.playerBossStart = true;
            bow.bowBossStart = true;
            mainCamera.StartCoroutine("BossCameraStart");
            
            once = false;
        }
    }

    public void SetPlayerHP()
    {
        currentPlayerHP = player.playerHP;
        UpdatePlayerHP();

        if (currentPlayerHP <= 0)
        {
            currentPlayerHP = 0;

            PlayerOnButtonClick();
            //죽었을 때
        }
    }

    public void SetBoosHP()
    {
        currentBossHP = boss.bossHP;
        UpdateBossHP();

        if (currentBossHP <= 0)
        {
            currentPlayerHP = 0;
            BossOnButtonClick();
            //죽었을 때
        }

    }

    public void UpdatePlayerHP()
    {
        playerHPSlider.value = (float)currentPlayerHP;
    }

    public void UpdateBossHP()
    {
        bossHPSlider.value = (float)currentBossHP;
    }

    public void PlayerOnButtonClick()
    {
        SceneManager.LoadScene("GameOverScene"); // 대상 씬으로 이동
    }

    public void BossOnButtonClick()
    {
        SceneManager.LoadScene("GameEndScene"); // 대상 씬으로 이동
    }
}
