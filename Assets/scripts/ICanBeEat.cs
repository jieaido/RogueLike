using UnityEngine;
using System.Collections;
public interface ICanBeEat
{
    int eatnum { get; }

    void DoSomeThing(Player player);
}