using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Monster : MoveObject, ILife
{
    private float _coldDowntime;
    public float CdTime;
    public PathFinder MyPathFinder;
    private Rigidbody2D _rb2D;
    private Collider2D _cd2D;
    /// <summary>
    /// 视野
    /// </summary>
    public float ViewRange;
    protected override void DoSomething(ICanbeFuck t)
    {
        throw new NotImplementedException();
    }

    public void Awake()
    {
        
    }

    public void Start()
    {
        _coldDowntime = CdTime;
        MyPathFinder = GetComponent<PathFinder>();
        _rb2D = GetComponent<Rigidbody2D>();
        _cd2D = GetComponent<Collider2D>();
    }

    public void Update()
    {
        _coldDowntime += Time.deltaTime;
        if (_coldDowntime > CdTime)
        {
            _coldDowntime = 0;
            ConfirmTarget();
        }
    }

    /// <summary>
    /// 用来确认目标  
    /// </summary>
    private void ConfirmTarget()
    {
        _cd2D.enabled = false;
       var ratcastHit2Ds=  Physics2D.BoxCastAll(_rb2D.position, new Vector2(ViewRange, ViewRange),0f,new Vector2());
       
            //这里要不要根据周围物体的权重来排行来跟踪哪一个呢?
            //还是仅仅去寻找最近的东西
        if (ratcastHit2Ds.Length>0)
        {
            //var gb = ratcastHit2Ds.Where(s => s.transform.tag == "player").FirstOrDefault().transform ??ratcastHit2Ds[0].transform;
                    var gb = ratcastHit2Ds.Where(s => s.transform.tag == "player").FirstOrDefault().transform;
                    if (gb != null)
                    {
                       print("我找到玩家了");
                    }
                    else
                    {
                     gb = ratcastHit2Ds[0].transform;//因为boxcast是按距离排序的,哈哈,所以第一个就是最近的东西.
                                                //todo 如果不为空就说明范围内有个人类,干掉他. 并且学习一下三元运算符的写法.
            }

            MyPathFinder.GoalTransform = gb;
           
        }
        _cd2D.enabled = true;

    }

    public int AttackNum { get; set; }
    public int DefenceNum { get; set; }
    public int VelocityNum { get; set; }
    public int VirgourNum { get; set; }
    public GameNum HPNum { get; set; }
    public int HorizonNum { get; set; }
    public void ChangeByTime()
    {
        throw new NotImplementedException();
    }
}