using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldMain : MonoBehaviour {

    public int moveSpeed = 10;
    private GameObject boat;
    private Vector3 yMovement = new Vector3(0f, 1f, 0f);
    private Vector3 xMovement = new Vector3(1f, 0f, 0f);
    private float maxRange = 7f;


    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("Boat"))
        {
            boat = GameObject.Find("Boat");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            // Get movement of the finger
            Vector3 touchPosition = Input.GetTouch(0).position;
            var x = (touchPosition.x-960)/108;
            var y = (touchPosition.y-540)/108;
            var z = touchPosition.z;
            boat.transform.position = new Vector3(x, y, 0f);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (boat.transform.position.y <= maxRange)
            {
                boat.transform.position += (yMovement / moveSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (boat.transform.position.y >= -maxRange)
            {
                boat.transform.position -= (yMovement / moveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (boat.transform.position.x >= -maxRange)
            {
                boat.transform.position -= (xMovement / moveSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            if (boat.transform.position.x <= maxRange)
            {
                boat.transform.position += (xMovement / moveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Application.LoadLevel("Kraken");
        }
    }
}
