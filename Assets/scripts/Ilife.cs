using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ILife
{
    int AttackNum { get; set; }
    int DefenceNum { get; set; }
    int VelocityNum { get; set; }
    int VirgourNum { get; set; }
    int HPNum { get; set; }
    int HorizonNum { get; set; }
    void ChangeByTime();
}