using System;
using UnityEngine;
using System.Collections;

public class PathNode     {

    public PathNode ParentNode { get; set; }
    public int EndH { get; set; }
    public int StartG { get; set; }

    public int TotalF
    {
        get { return EndH + StartG; }
    }

    public Vector2 Position { get; set; }
    public bool IsPass = true;//是否障碍物，能否穿过,要设置个默认值为true
    public PathNode(int h, int g)
    {
        EndH = h;
        StartG = g;
        IsPass = true;
    }

    public PathNode(Vector2 postion)
    {
        Position = postion;
    }


    public int CompareTo(PathNode other)
    {
        if (other == null) return 1;

        PathNode otherPathNode = other as PathNode;
        if (otherPathNode != null)
            return this.TotalF.CompareTo(otherPathNode.TotalF);
        else
            throw new ArgumentException("Object is not a Temperature");

    }

    public override string ToString()
    {
        return "PathNode--F:" + this.TotalF + "--G:" + this.StartG + "--H:" + this.EndH;
    }

    public static int CalcEndH(Vector2 startPostion, Vector2 goalpPostion)
    {
        return (int) (Math.Abs(goalpPostion.y - startPostion.y) + Math.Abs(goalpPostion.x - startPostion.x));
    }
}
	

