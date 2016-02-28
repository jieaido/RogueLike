using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Monster : MoveObject, Ilife
{
   

    protected override void DoSomething(ICanbeFuck t)
    {
        throw new NotImplementedException();
    }

    public int AttackNum { get; set; }
    public int DefenceNum { get; set; }
    public int VelocityNum { get; set; }
    public int VirgourNum { get; set; }
    public int HPNum { get; set; }
    public int HorizonNum { get; set; }
}