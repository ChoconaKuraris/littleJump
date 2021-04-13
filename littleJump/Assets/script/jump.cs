using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jump : MonoBehaviour
{
    public GameObject seat;
    private ArrayList seats;
    //最终压缩高度
    public float endscalcey = 0.5f;
    //主相机
    public Camera maincamera;
    //到哪个柱子了
    private GameObject nowat;
    //是否可以跳跃
    private bool canjump = false;
    //按下的时长
    private float time = 0;
    //指示是否按下
    private bool ondown = false;
    //弹跳力
    private int score = 0;
    public float jumpPower = 1.0f;
    //背景
    public GameObject plane;



    public delegate void PlayerScore(int temp);//定义委托
    public event PlayerScore GetScore;//定义得分事件，用于发出得分的消息

    public delegate void GameOver(bool temp);
    public event GameOver IsGameOver;//用于发出死亡的消息


    // Start is called before the first frame update
    void Start()
    {
        seats = new ArrayList();
        seats.Add(Instantiate(seat, new Vector3(0, 0, 0), Quaternion.identity));
        for(int i=1;i<20;i++)
        {//从第一个柱子开始，随机距离添加柱子
            seats.Add(Instantiate(seat, new Vector3(Random.Range(1f, 2.28f) + ((GameObject)seats[i - 1]).transform.position.x, 0, 0), Quaternion.identity));
            //修改柱子的宽度
            ((GameObject)seats[i]).transform.localScale = new Vector3(Random.Range(0.5f, 1.5f), ((GameObject)seats[i]).transform.localScale.y, ((GameObject)seats[i]).transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)&&canjump) //按键，计时并根据计时形变
        {
            var y = Mathf.SmoothStep(1f, endscalcey, time * 0.01f); //平滑插值，在1和endscalcey=0.5 之间以3x^2 -2x^3插值
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z); //弹性形变
            time += Time.timeScale; //计时
          
            ondown = true;
            time = time > 100 ? 100 : time;
  
        }
        if (Input.GetKeyUp(KeyCode.Space) && ondown&&canjump) //抬手
        {
            ondown = false;
            GetComponent<Rigidbody>().AddForce(new Vector3(time * jumpPower, 350, 0)); //给一个向前的力
            time = 0;
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z); //回弹
        }

        if (transform.position.y < 0.2f) //掉落重新载入场景
        {
            if (IsGameOver != null)//检查事件是否为空，即有没有接收器订阅它
            {
                IsGameOver(true);//发送死亡事件消息
                
            }
          
           // SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnCollisionEnter(Collision collision) //碰撞检测防止空中起跳
    {
        if (GetScore != null)//检查事件是否为空，即有没有接收器订阅它
        {
            GetScore(1);//发送得分事件消息，为接收器提供参数1，实现+1分的效果
            Debug.Log("score+1");
        }
        score++;
        canjump = true;

    }
    private void OnCollisionExit(Collision collision)
    {
        canjump = false;

    }

    private void LateUpdate()
    {
        //player所在位置
        Vector3 playerpos = transform.position;
        //相机跟随
        maincamera.transform.position = new Vector3(playerpos.x + 1.12f, maincamera.transform.position.y, maincamera.transform.position.z);
        //背景跟随
        plane.transform.position = new Vector3(playerpos.x + 1.12f, plane.transform.position.y, plane.transform.position.z);
        //回收柱子
        if (playerpos.x > ((GameObject)seats[0]).transform.position.x + 6)
        {
            ((GameObject)seats[0]).SetActive(false);
            Destroy(((GameObject)seats[0]));
            seats.Remove(seats[0]);
            seats.Add(Instantiate(seat, new Vector3(Random.Range(2f, 5f) + ((GameObject)seats[seats.Count - 1]).transform.position.x, Random.Range(-1.09f, 5.53f), -8.2f), Quaternion.identity));
            Debug.Log("add a seat...");
        }

    }

}
