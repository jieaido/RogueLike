using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters;

public  class Weapon :ICanbeFuck
{
    public int _heightNum;
    public int _attacknum;

    public int HeightNum
    {
        get { return _heightNum; }

        set { _heightNum = value; }
    }

    public int AttackNum
    {
        get { return _attacknum; }

        set { _attacknum = value; }
    }

    public override void Befuck(MoveObject mo)
    {
       
       
    }
}