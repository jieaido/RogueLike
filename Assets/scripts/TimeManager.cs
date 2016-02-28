using System;
using UnityEngine;
using System.Collections;


public class TimeManager : MonoBehaviour
{
    public TimeManager Instance;
   
    public int Hour { get; private set; }
    public int Year { get; private set; }
    public DateTime Minute;

    public int Day { get;private set; }

    private float RunTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RunTime += Time.deltaTime;
        if (RunTime>30)//超过30秒就是1个小时
        {
            RunTime = 0;
            Hour ++;
            if (Hour>23)//超过23小时就是一天
            {
                Hour = 0;
                Day++;
                if (Day>365)//超过365天就是一年
                {
                    Day = 0;
                    Year++;
                }
                
            }

        }
    }

    public void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else if (Instance!=null)
        {
            Destroy(gameObject);
        }
       
    }
}
