using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static List<PathNode> FindNodes = new List<PathNode>();


    public List<PathNode> CloseNodes = new List<PathNode>();
    public Transform GoalTransform;
    private Rigidbody2D NodeR2D;
    public List<PathNode> OpenNodes = new List<PathNode>();

    public PathNode ReturnFirstNode;




    /*现在我要开始写寻找目标的代码了
        首先怪物会有个寻找范围,可以把它想想成为视野
        1每到一次运动时间(coldDown),怪物会进行一次搜索.如果发现有类似的食物,player和道具,怪物会把目标定位.
		2当目标定位后(目标非空),怪物会进行寻路,如果是道具.怪物会把道具吃掉,然后重复第一步
		3如果怪物是玩家
 






    */
    // Use this for initialization
    private void Start()
    {
        NodeR2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void sd()
    {
    }

    public bool FindGoal()
    {
        Vector2 endNode = GoalTransform.position;

        OpenNodes.Add(getNodeByPostion(NodeR2D.position));
        while (OpenNodes.Count > 0)
        {
            var currentNode = OpenNodes.OrderBy(p => p.TotalF).First();
            OpenNodes.Remove(currentNode);
            CloseNodes.Add(currentNode);
            var bbb = currentNode.Position == endNode;
            if (bbb)
            {
                var pps = new List<PathNode>();
                var p = currentNode.ParentNode;
                while (p != null)
                {
                    pps.Add(p);
                    p = p.ParentNode;
                }
                if (pps.Count == 1)
                {
                    ReturnFirstNode = pps.First();
                }
                else
                {
                    ReturnFirstNode = pps[pps.Count - 2];
                    ReturnFirstNode.ParentNode = null;
                }
                OpenNodes.Clear();
                CloseNodes.Clear();
                return true;
            }
            foreach (var node in GetBoundNodes(currentNode.Position, endNode))
            {
                if (CloseNodes.Exists(p => p.Position == node.Position))
                {
                    continue;
                }
                if (!OpenNodes.Exists(p => p.Position == node.Position))
                {
                    node.ParentNode = currentNode;
                    OpenNodes.Add(node);
                    continue;
                }
                if (OpenNodes.Exists(p => p.Position == node.Position))
                {
                    var tempP = OpenNodes.Find(p => p.Position == node.Position);
                    if (node.TotalF < tempP.TotalF)
                    {
                        tempP.StartG = node.StartG;
                        tempP.EndH = node.EndH;
                        tempP.ParentNode = currentNode;
                    }
                }
            }
        }
        return false;
    }

    public List<PathNode> GetBoundNodes(Vector2 _pos, Vector2 goalPos)
    {
        var bounderNodes = new List<PathNode>();
        PathNode[] pns =
        {
            FindNodes.Find(p => (int) p.Position.x == (int) _pos.x && (int) p.Position.y == (int) _pos.y + 1),
            FindNodes.Find(p => (int) p.Position.x == (int) _pos.x && (int) p.Position.y == (int) _pos.y - 1),
            FindNodes.Find(p => (int) p.Position.x == (int) _pos.x + 1 && (int) p.Position.y == (int) _pos.y),
            FindNodes.Find(p => (int) p.Position.x == (int) _pos.x - 1 && (int) p.Position.y == (int) _pos.y)
        };
        for (var i = 0; i < pns.Length; i++)
        {
            if (pns[i] != null && pns[i].IsPass)
            {
                var p = pns[i];


                CalcTotalF(p, _pos, goalPos);
                bounderNodes.Add(p);
            }
        }
        return bounderNodes;
    }

    private PathNode getNodeByPostion(Vector2 posVector2)
    {
        return FindNodes.Find(p => p.Position == posVector2);
    }

    private void CalcTotalF(PathNode thisNode, Vector2 PreVector2, Vector2 goalPos)
    {
        if (thisNode.ParentNode != null)
        {
            thisNode.StartG = getNodeByPostion(PreVector2).StartG +
                              PathNode.CalcEndH(thisNode.Position, PreVector2);
        }
        thisNode.EndH = PathNode.CalcEndH(thisNode.Position, goalPos);
    }
}