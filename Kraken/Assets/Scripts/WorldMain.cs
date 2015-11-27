using UnityEngine;
using System.Collections;

public class WorldMain : MonoBehaviour {

    public int moveSpeed = 10;
    private GameObject boat;
    private Vector3 yMovement = new Vector3(0f, 1f, 0f);
    private Vector3 xMovement = new Vector3(1f, 0f, 0f);
    private float maxRange = 4f;

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
            Vector3 touchPosition = Input.GetTouch(0).deltaPosition;
            //if (boat.transform.position.y <= maxRange && boat.transform.position.x <= maxRange && boat.transform.position.y >= -maxRange && boat.transform.position.x >= -maxRange)
            //{
            //    // Move object across XY plane
            //    boat.transform.position += touchPosition / 10;
            //}
            //else
            //{
            //    boat.transform.position -= touchPosition / 10; 
            //}
            if (touchPosition.x >= 0 && boat.transform.position.x <= maxRange)
            {
                boat.transform.position += new Vector3((touchPosition.x / 10),0f,0f);
            }
            else if (touchPosition.x < 0 && boat.transform.position.x >= -maxRange)
            {
                boat.transform.position += new Vector3((touchPosition.x / 10), 0f, 0f);
            }
            if (touchPosition.y >= 0 && boat.transform.position.y <= maxRange)
            {
                boat.transform.position += new Vector3(0f, (touchPosition.y / 10), 0f);
            }
            else if (touchPosition.y < 0 && boat.transform.position.y >= -maxRange)
            {
                boat.transform.position += new Vector3(0f, (touchPosition.y / 10), 0f);
            }

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
