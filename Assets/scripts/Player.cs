﻿using UnityEngine;

public class Player : MoveObject, ILife
{
    private float _cooldownTime;
    public float CdTime = 0.8f;
    public int MaxVirgour;
    public PathFinder pathFinder;
    private int pv_hp;
    public int pv_Virgour;
    public bool RunByAI;


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
        if (VirgourNum > 0)
        {
            VirgourNum--;
        }
        else
        {
            if (HPNum.CurrentNum > 0)
            {
                HPNum.CurrentNum = HPNum.CurrentNum - 1;
                pv_hp = HPNum.CurrentNum;
            }
        }
    }


    // Use this for initialization
    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();

        rb2d = GetComponent<Rigidbody2D>();
        _cooldownTime = CdTime;
        bc2d = GetComponent<BoxCollider2D>();
        TimeManager.Instance.AddEventLifeList(this); //添加到时间处理函数中
    }

    public void Awake()
    {
        VirgourNum = pv_Virgour;
        HPNum = new GameNum(100, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        /*先把Monster的AI写在这里了
         if (RunByAI) 如果在ai状态
        {
                如果现在的位置不是目标位置,准备移动
            if (rb2d.position != (Vector2) pathFinder.GoalTransform.position)
            {当移动冷却可以移动的时候
                if (_cooldownTime >= CdTime)
                {
                    _cooldownTime = 0.0f;
                      开始寻路,如果找到路了,
                    if (pathFinder.FindGoal())
                    {
                        
                        var nextPos = pathFinder.ReturnFirstNode.Position;
                        if (nextPos != null)
                        {
                            AttemptMove((int) (nextPos.x - rb2d.position.x), (int) (nextPos.y - rb2d.position.y));
                        }

                        return;
                    }
                }
            }
            else
            {
                RunByAI = false;
            }























        */
        _cooldownTime += Time.deltaTime;
        if (RunByAI)
        {
            if (rb2d.position != (Vector2) pathFinder.GoalTransform.position)
            {
                if (_cooldownTime >= CdTime)
                {
                    _cooldownTime = 0.0f;
                    if (pathFinder.FindGoal())
                    {
                        var nextPos = pathFinder.ReturnFirstNode.Position;
                        if (nextPos != null)
                        {
                            AttemptMove((int) (nextPos.x - rb2d.position.x), (int) (nextPos.y - rb2d.position.y));
                        }

                        return;
                    }
                }
            }
            else
            {
                RunByAI = false;
            }
        }
        // _rigidbody2D.MovePosition(Vector2.Lerp(transform.position, taegetpos, 10*Time.deltaTime));
        var h = (int) Input.GetAxisRaw("Horizontal");
        var v = (int) Input.GetAxisRaw("Vertical");
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

    //    protected override bool AttemptMove(int h, int v)
    //    {
    //        return base.AttemptMove(h, v);
    //    }


    protected override void DoSomething(ICanbeFuck t)
    {
        t.Befuck(this);
    }
}