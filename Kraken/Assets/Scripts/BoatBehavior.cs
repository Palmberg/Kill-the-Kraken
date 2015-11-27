using UnityEngine;
using System.Collections;

public class BoatBehavior : MonoBehaviour
{

    public Collider kraken;
    public Collider yeti;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var kr = GameObject.Find("Kraken");
        if (kr)
        {
            kraken = kr.GetComponent<Collider>();
        }
        var ye = GameObject.Find("Yeti");
        if (ye)
        {
            yeti = ye.GetComponent<Collider>();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (kraken != null)
        {
            if (col == kraken)
            {
                Application.LoadLevel("Kraken");
            }
        }
        if (yeti != null)
        {
            if (col == yeti)
            {
                Application.LoadLevel("Yeti");
            }
        }
    }
}