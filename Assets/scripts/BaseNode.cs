using UnityEngine;
using System.Collections;

/// <summary>
/// 这个是基础的节点类,所有的放置在地图上的都要继承于此类
/// </summary>
public class BaseNode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public PathNode PathNode=new PathNode(0,0);
    

}
