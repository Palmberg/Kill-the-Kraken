﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldMain : MonoBehaviour {

    public int moveSpeed = 10;
    private GameObject boat;
    private Vector3 yMovement = new Vector3(0f, 1f, 0f);
    private Vector3 xMovement = new Vector3(1f, 0f, 0f);
    private float maxRange = 7f;
    private GameObject camera;
    private float xDelta=0f;
    private float yDelta=0f;
    private Vector3 oldCenter = new Vector3(0, 0, 0);
    private int worldSpace = 20;
    private Vector3 rTarget = new Vector3(5f, 5f, 5f);
    private Vector3 eTarget = new Vector3(1f, 1f, 0f);

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("Boat"))
        {
            boat = GameObject.Find("Boat");
        }
        if (GameObject.Find("Main Camera"))
        {
            camera = GameObject.Find("Main Camera");
            Debug.Log("Camera found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((boat.transform.rotation.x));
        if (Input.touchCount > 0)
        {
            // Get movement of the finger
            Vector3 touchPosition = Input.GetTouch(0).position;
            var x = ((touchPosition.x-960)/108)+xDelta;
            var y = ((touchPosition.y-540)/108)+yDelta;
            var z = touchPosition.z;
            boat.transform.position = new Vector3(x, y, 0f);
            var yPosTmp = boat.transform.position.y - oldCenter.y;
            var xPosTmp = boat.transform.position.x - oldCenter.x;
            if (yPosTmp>2.5f && y < worldSpace)
            {
                //if (boat.transform.rotation.x == 0)
                //{
                //    boat.transform.Rotate(-90f, 0f, 0f);
                //}
                
                camera.transform.position += yMovement*yPosTmp/5;
                yDelta += yPosTmp/5;
                oldCenter+=yMovement*yPosTmp/5;
            }
            else if (yPosTmp < -2.5f && y > -worldSpace)
            {
                camera.transform.position += yMovement * yPosTmp / 5;
                yDelta += yPosTmp/5;
                oldCenter += yMovement * yPosTmp / 5;
            }
            else if (xPosTmp > 3f && x < worldSpace)
            {
                camera.transform.position += xMovement * xPosTmp / 5;
                xDelta += xPosTmp/5;
                oldCenter += xMovement*xPosTmp/5;
            }
            else if (xPosTmp < -3f && x > -worldSpace)
            {
                camera.transform.position += xMovement * xPosTmp / 5;
                xDelta += xPosTmp/5;
                oldCenter += xMovement*xPosTmp/5;
            }

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //if (Mathf.Abs(boat.transform.rotation.x) == 0)
            //{
            //    boat.transform.Rotate(270f, 0f, 0f);
            //}
            //else if (Mathf.Abs(boat.transform.rotation.x) == 180f)
            //{
            //    boat.transform.Rotate(90f, 0f, 0f);
            //}
            //else if (Mathf.Abs(boat.transform.rotation.x) == 90f)
            //{
            //    boat.transform.Rotate(180f, 0f, 0f);
            //}

            if (boat.transform.position.y <= maxRange)
            {
                boat.transform.position += (yMovement / moveSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //if (Mathf.Abs(boat.transform.rotation.x) == 0)
            //{
            //    boat.transform.Rotate(90f, 0f, 0f);
            //}
            //else if (Mathf.Abs(boat.transform.rotation.x) == 180f)
            //{
            //    boat.transform.Rotate(270f, 0f, 0f);
            //}
            //else if (Mathf.Abs(boat.transform.rotation.x) == 270f)
            //{
            //    boat.transform.Rotate(180f, 0f, 0f);
            //}
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
        if (Input.GetKey(KeyCode.R))
        {
            boat.transform.LookAt(rTarget);
        }
        if (Input.GetKey(KeyCode.E))
        {
            boat.transform.LookAt(eTarget);
        }
    }

}
