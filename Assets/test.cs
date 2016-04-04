using System;
using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        var rend = GetComponent<SpriteRenderer>();

        // duplicate the original texture and assign to the material

	    var t2d = rend.sprite.texture;
        // colors used to tint the first 3 mip levels
        Color[] colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
      //  int mipCount = Mathf.Min(3, t2d.mipmapCount);

        // tint each mip level
	    for (int i = 0; i < 3; i++)
	    {
	        t2d.SetPixel(i,i,Color.clear);
	    }
        // actually apply all SetPixels, don't recalculate mip levels
        t2d.Apply(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
