using UnityEngine;
using System.Collections;

public class YetiMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.X))
        {
            Application.LoadLevel("World");
        }
    }
}
