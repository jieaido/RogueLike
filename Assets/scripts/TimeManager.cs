using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TimeManager : MonoBehaviour
{
    public  static TimeManager Instance;
   
    public uint Hour { get; private set; }
    public uint Year { get; private set; }
    protected HashSet<ILife> IlifeList=new HashSet<ILife>(); 

    public uint Day { get;private set; }

    private float RunTime;
    // Use this for initialization
    void Start()
    {

    }

    public void AddEventLifeList(ILife life)
    {
        IlifeList.Add(life);
      
    }
 
    public void SetGameTime(GameTimeStruck gtGameTimeStruck)
    {
        TimeManager.Instance.Day = gtGameTimeStruck.Day;
        TimeManager.Instance.Hour = gtGameTimeStruck.Hour;
        TimeManager.Instance.Year = gtGameTimeStruck.Year;
    }

    // Update is called once per frame
    void Update()
    {
        RunTime += Time.deltaTime;
        if (RunTime>3)//超过30秒就是1个小时
        {
            RunTime = 0;
            Hour ++;
            foreach (var life in IlifeList)
            {
                life.ChangeByTime();//
            }
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
    public struct GameTimeStruck
    {
        public uint Year { get; private set; }
        public uint Day { get; private set; }
        public uint Hour { get; private set; }

        public GameTimeStruck(uint day, uint year, uint hour) : this()
        {
            Day = day;
            Year= year;
            Hour = hour;
        }
    }
}
