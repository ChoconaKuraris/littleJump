using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//引入unity的UI编辑库

public class score : MonoBehaviour
{
    public jump player;//定义PlayerControl类
    public int _score;//定义积分变量
    public Text ScoreUI;//定义要修改的Text
    // Start is called before the first frame update
    void Start()
    {
        player.GetScore += OnGetScore;//调用PlayerControl类，订阅Player的得分事件
       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public int Score//定义积分属性，使用属性来进行控制，使每次积分被改变时就调用一次文本显示的修改
    {
        get 
        {
            return _score;
        }
        set
        {
            _score = value;
            ScoreUI.text = _score.ToString();//让UI显示的分数等于score的值，这里有String类型的转换
        }
    }

    private void OnGetScore(int score)//定义消息处理器来处理消息，给属性赋值，改变积分值
    {
        Score += score;
        
        Debug.Log("playerScore=" + score);
        Debug.Log("textScore="+Score);
    }


    
}
