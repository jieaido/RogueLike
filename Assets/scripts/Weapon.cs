using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters;

public  class Weapon :ICanbeFuck
{
    public int Addhp = 20;
	
  

    public override void Befuck(MoveObject mo)
    {
        if ( mo is Player)
        {
            mo.Hp += Addhp;
            print(mo.Hp+"_"+mo.Mp);
            gameObject.SetActive(false);
        }
       
    }
}
