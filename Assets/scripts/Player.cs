﻿using System.Collections;
using UnityEngine;

public class Player : MoveObject, ILife
{
    public float CdTime = 0.8f;
    public int MaxVirgour;
    public int pv_Virgour;
    private int pv_hp;


    private float _cooldownTime;



    // Use this for initialization
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _cooldownTime = CdTime;
        bc2d = GetComponent<BoxCollider2D>();





    }

    public void Awake()
    {
        VirgourNum = pv_Virgour;
        HPNum=new GameNum(100,0);
        TimeManager.Instance.AddEventLifeList(this);//添加到时间处理函数中
    }

    // Update is called once per frame
    private void Update()
    {
        _cooldownTime += Time.deltaTime;
        // _rigidbody2D.MovePosition(Vector2.Lerp(transform.position, taegetpos, 10*Time.deltaTime));
        var h = (int)Input.GetAxisRaw("Horizontal");
        var v = (int)Input.GetAxisRaw("Vertical");
        if (h != 0)
        {
            v = 0;
        }
        if (h != 0 || v != 0)
        {

            if (_cooldownTime >= CdTime)
            {
                _cooldownTime = 0f;
                //taegetpos += new Vector2(h, v);
                //StartCoroutine(Moveing(taegetpos));//此时就直接移动了,所以不行,添加测试函数atteptmove,这句话移动到atteptmove里
                AttemptMove(h, v);

                // _rigidbody2D.MovePosition(taegetpos);
            }
        }
    }

    protected override bool AttemptMove(int h, int v)
    {
        return base.AttemptMove(h, v);
    }



    protected override void DoSomething(ICanbeFuck t)
    {
        t.Befuck(this);
    }


    /*
    private IEnumerator Moveing(Vector2 endpos)
    {



       
        //实现方式1
  
        #region MyRegion
//        while (_rigidbody2D.position != endpos)
//        {
//            print(Time.time);
//            _rigidbody2D.MovePosition(Vector2.MoveTowards(_rigidbody2D.position, endpos, 10 * Time.deltaTime));//直接这样用
//            yield return null;
//        } 
        #endregion

       
            //实现方式2
        
        
       
                       float sqrRemainingDistance = (_rigidbody2D.position - endpos).sqrMagnitude;
                      //获取两点间的距离
                    //While that distance is greater than a very small amount (Epsilon, almost zero):
                     //当两点间的距离大于一个非常小的数接近于零 
                    while (sqrRemainingDistance > float.Epsilon)
                    {
            
                        //Find a new position proportionally closer to the end, based on the moveTime
                        Vector2 newPostion = Vector2.MoveTowards(_rigidbody2D.position, endpos, 10 * Time.deltaTime);
        
                        //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                        _rigidbody2D.MovePosition(endpos);
        
                        //Recalculate the remaining distance after moving.
                        sqrRemainingDistance = (_rigidbody2D.position - endpos).sqrMagnitude;
                        print(sqrRemainingDistance > float.Epsilon);
                      
                        //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                        yield return null;
                    } 
    

    }
    */


    public int AttackNum { get; set; }
    public int DefenceNum { get; set; }
    public int VelocityNum { get; set; }

    public int VirgourNum { get; set; }
    public GameNum HPNum { get; set; }
    public int HorizonNum { get; set; }
    public void ChangeByTime()
    {
        if (VirgourNum>0)
        {
            VirgourNum--;
        }
        else
        {
            if (HPNum.CurrentNum>0)
            {
                HPNum.CurrentNum=HPNum.CurrentNum-1;
                pv_hp = HPNum.CurrentNum;
            }
        }
        
    }
}