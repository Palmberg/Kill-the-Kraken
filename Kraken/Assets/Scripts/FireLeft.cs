using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireLeft : MonoBehaviour
{
    public GameObject mainEngine;
    KrakenMain script;
    private GameObject hello;

    // Use this for initialization
    void Start()
    {
        //mainEngine = GameObject.Find("_MainEngine");
        script = (KrakenMain)mainEngine.GetComponent<KrakenMain>();
        hello = GameObject.Find("Text");

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void shootLeft()
    {

        if (hello.activeSelf)
        {
            hello.SetActive(false);
        }
        /*else
        {
            hello.SetActive(true);
        }*/
        script.leftShot();

    }
}
