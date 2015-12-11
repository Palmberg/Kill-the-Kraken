using UnityEngine;
using System.Collections;

public class shotBehaviour : MonoBehaviour {
    private float initTime;
    private float moveTime;
	// Use this for initialization
	void Start () {
        initTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        moveTime = Time.time;
        var tmp = moveTime - initTime;
        if (tmp > 1.7f)
        {
            Destroy(gameObject);
        }
	}
}
