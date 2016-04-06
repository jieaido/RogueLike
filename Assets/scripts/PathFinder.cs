using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static List<PathNode> FindNodes = new List<PathNode>();


    public List<PathNode> CloseNodes = new List<PathNode>();
    public Transform GoalTransform;
    public Rigidbody2D NodeR2D;
    public List<PathNode> OpenNodes = new List<PathNode>();

    public PathNode ReturnFirstNode;

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
            CloseNodes.Remove(currentNode);
            if (currentNode.Position == endNode)
            {
                var pps = new List<PathNode>();
                var p = currentNode.ParentNode;
                while (p != null)
                {
                    pps.Add(p);
                    p = p.ParentNode;
                }
                ReturnFirstNode = pps.Last();
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
                CalcTotalF(p, goalPos);
                bounderNodes.Add(p);
            }
        }
        return bounderNodes;
    }

    private PathNode getNodeByPostion(Vector2 posVector2)
    {
        return FindNodes.Find(p => p.Position == posVector2);
    }

    private void CalcTotalF(PathNode thisNode, Vector2 goalPos)
    {
        if (thisNode.ParentNode != null)
        {
            thisNode.StartG = thisNode.ParentNode.StartG +
                              PathNode.CalcEndH(thisNode.Position, thisNode.ParentNode.Position);
        }
        thisNode.EndH = PathNode.CalcEndH(thisNode.Position, goalPos);
    }
}