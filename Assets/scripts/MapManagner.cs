﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;


public class MapManagner: MonoBehaviour
{

    public GameObject[] Wall;
    public GameObject[] Floor;
    public GameObject[] InWall;
    public GameObject[] Items;
    public GameObject[] Enemy;
    private readonly List<Vector3> _inWallPosList=new List<Vector3>();

    private GameObject _mapHolder;
    void Start()
    {
      
        InitMap(10,10);
        InitItem(2,5,InWall);
        InitItem(0, (int)Mathf.Log(LevelManager.Nowlevel)+1, Items);
        InitItem(0, (int)Mathf.Log(LevelManager.Nowlevel), Enemy);

    }
    /// <summary>
    /// 根据参数生成地图
    /// </summary>
    /// <param name="cols">行数</param>
    /// <param name="rows">列数</param>
    private void InitMap(int cols,int rows)
        
    {
        _mapHolder =new GameObject("Mapholder");
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (i==0||j==0||i==cols-1||j==rows-1)
                {

                    GameObject go=Instantiate(Wall[Random.Range(0, Wall.Length)], new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(_mapHolder.transform);
                }
                else
                {
                    GameObject go=Instantiate(Floor[Random.Range(0, Wall.Length)], new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(_mapHolder.transform);
                    if ((i>=2&&i<cols-2)&&(j>=2&&j<rows-2))
                    {
                        _inWallPosList.Add(new Vector3(i,j,0));
                    }
                }
               
            }
        }

       
    }
    #region MyRegion
    /// <summary>
    /// 根据参数生成障碍物
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    private void InitInWall(int min, int max)
    {
        int fornum = Random.Range(min, max + 1);
        for (int i = 0; i < fornum; i++)
        {
            var tempVector3 = GetRandomPositon();
            Instantiate(InWall[Random.Range(0, InWall.Length)], tempVector3, Quaternion.identity);
        }
    } 
    #endregion
    /// <summary>
    /// 返回随机地图块
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPositon()
    {
        Vector3 tempVector3 = _inWallPosList[Random.Range(0, _inWallPosList.Count)];
        _inWallPosList.Remove(tempVector3);
        return tempVector3;
    }
    /// <summary>
    /// 生成所需生成的物体
    /// </summary>
    /// <param name="min">生成物体的最少数量</param>
    /// <param name="max">生成物体的最大数量</param>
    /// <param name="initobjects">生成物体的数组</param>
    private void InitItem(int min ,int max,GameObject[] initobjects)
    {
        int fornum = Random.Range(min, max + 1);
        for (int i = 0; i < fornum; i++)
        {
            var tempVector3 = GetRandomPositon();
            Instantiate(initobjects[Random.Range(0, initobjects.Length)], tempVector3, Quaternion.identity);
        }
    }
}