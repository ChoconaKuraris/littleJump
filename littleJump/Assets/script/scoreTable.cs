using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//引入unity的UI编辑库

public class scoreTable : MonoBehaviour
{
    public jump player;//定义PlayerControl类
    public score score; //获取最终的计分
    public Text scoreText;
    public Button restart_button;
    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        //隐藏ui
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        player.IsGameOver += OnGameover;//调用PlayerControl类，订阅Player的死亡事件
        restart_button.onClick.AddListener(onclick);
    }

    private void OnGameover(bool gameOver)
    {
        isGameOver = gameOver;
     
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver) //如果游戏结束显示ui
        {
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            scoreText.text = score.Score.ToString(); //计总分
            Time.timeScale = 0;
            
        }
    }

    private void onclick()
    {
        SceneManager.LoadScene("SampleScene"); //重新载入场景
        Debug.Log("click btn");
        Time.timeScale = 1;
    }
}
