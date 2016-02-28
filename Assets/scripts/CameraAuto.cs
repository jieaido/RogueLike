using UnityEngine;
using System.Collections;

public class CameraAuto : MonoBehaviour
{

    public GameObject target;





    private Transform targetpos;

	// Use this for initialization
	void Start ()
	{
	    targetpos = target.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	   float targetX = transform.position.x;
        float targetY = transform.position.y;
        if (Mathf.Abs(transform.position.x-targetpos.position.x)>2.5f)
	    {
	       targetX= Mathf.Lerp(transform.position.x, targetpos.position.x, 10*Time.deltaTime);
	    }
        if (Mathf.Abs(transform.position.y - targetpos.position.y) > 2.5f)
        {
            targetY = Mathf.Lerp(transform.position.y, targetpos.position.y, 10 * Time.deltaTime);
        }
        transform.position=new Vector3(targetX,targetY,transform.position.z);
    }
}
