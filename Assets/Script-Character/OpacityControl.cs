using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityControl : MonoBehaviour {

    public Color t;
    // Use this for initialization
    public Color x;
    public bool fullOff = false;
	void Start () {
        t = this.GetComponent<SpriteRenderer>().color;
        x = t;
        if (fullOff == false) { 
            x.a = 0.5f;
        }
        else
        {
            x.a = 0;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = x;
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = t;
        }
    }
}
