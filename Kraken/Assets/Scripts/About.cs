using UnityEngine;
using System.Collections;

public class About : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void game()
    {
        Application.LoadLevel("AboutGame");
    }
    public void dev()
    {
        Application.LoadLevel("AboutDev");
    }
    public void back()
    {
        Application.LoadLevel("Intro");
    }
}
