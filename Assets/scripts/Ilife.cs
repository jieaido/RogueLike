using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface ILife
{
    int AttackNum { get; set; }
    int DefenceNum { get; set; }
    int VelocityNum { get; set; }
    int VirgourNum { get; set; }
    GameNum HPNum { get; set; }
    int HorizonNum { get; set; }
    void ChangeByTime();
}

public class GameNum
{
    int MaxNum;
    int MinNum;
    private int currentNum;
    /// <summary>
    /// 
    /// </summary>
    public int CurrentNum
    {
        get
        {
           
            return currentNum;
        }
        set
        {
//            if (value<=MinNum)
//            {
//                currentNum = MinNum;
//            }else if (value>=MaxNum)
//            {
//                currentNum = MaxNum;
//            }
          currentNum=  Mathf.Clamp(value, MinNum, MaxNum);

        }
    }

    public GameNum(int maxNum, int minNum) 
    {
        MaxNum = maxNum;
        MinNum = minNum;
        CurrentNum = maxNum;
    }

    public GameNum(int maxNum, int minNum, int cNum)
    {
        MaxNum = maxNum;
        MinNum = minNum;
        CurrentNum = cNum;
    }
}