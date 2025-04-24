using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStartButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("GamePlayScene"); // 대상 씬으로 이동
    }
}
