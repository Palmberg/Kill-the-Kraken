using UnityEngine;
using System.Collections;

public class KrakenMain : MonoBehaviour
{
    public int moveSpeed = 10;
    private GameObject characterRight;
    private GameObject characterLeft;
    private Vector3 movement = new Vector3(0f, 1f, 0f);
    private float maxRange = 4.5f;
    private GameObject canonBall;
    public GameObject CanonBallprefab;
    private Rigidbody canonBallRigid;
    //private Vector3 shotMovement = new Vector3(1f, 0f, 0f);
    private float startTime;
    private float shotTime = 5f;
    // public int x;
    public Rigidbody projectile;

    // Use this for initialization
    void Start()
    {
        canonBallRigid = GameObject.Find("CanonBall").GetComponent<Rigidbody>();
        characterRight = GameObject.Find("CharacterRight");
        characterLeft = GameObject.Find("CharacterLeft");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Get movement of the finger
            Vector3 touchPositionL = Input.GetTouch(0).position;
            Vector3 touchPositionR = Input.GetTouch(1).position;
            var xl = (touchPositionL.x - 960) / 108;
            var yl = (touchPositionL.y - 540) / 108;
            var zl = touchPositionL.z;
            var xr = (touchPositionR.x - 960) / 108;
            var yr = (touchPositionR.y - 540) / 108;
            var zr = touchPositionR.z;
            if (xl < 0)
            {
                characterLeft.transform.position = new Vector3(-7.5f, yl, -0.5f);
            }
            else if (xl > 0)
            {
                characterRight.transform.position = new Vector3(7.5f, yl, -0.5f);
            }
            if (xr < 0)
            {
                characterLeft.transform.position = new Vector3(-7.5f, yr, -0.5f);
            }
            else if (xr > 0)
            {
                characterRight.transform.position = new Vector3(7.5f, yr, -0.5f);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (characterRight.transform.position.y <= maxRange)
            {
                characterRight.transform.position += (movement / moveSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (characterRight.transform.position.y >= -maxRange)
            {
                characterRight.transform.position -= (movement / moveSpeed);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Shoot();
            ShootRigid();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (characterLeft.transform.position.y <= maxRange)
            {
                characterLeft.transform.position += (movement / moveSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            if (characterLeft.transform.position.y >= -maxRange)
            {
                characterLeft.transform.position -= (movement / moveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            Application.LoadLevel("World");
        }
    }
    //void Shoot()
    //{
    //    startTime = Time.time;
    //    // x = 0;
    //    canonBall = (GameObject)Instantiate(CanonBallprefab, new Vector3(10.45f, characterRight.transform.position.y, -2.27f), Quaternion.identity);
    //    shootUpdate();
    //}
    //void shootUpdate()
    //{
    //    float running = Time.time - startTime;


    //    if (running < shotTime)
    //    {
    //        canonBall.transform.position -= (shotMovement);
    //        //x += 1;
    //    }


    //}
    void ShootRigid()
    {
        startTime = Time.time;
        canonBallRigid.transform.position = characterRight.transform.position;
        ShootRigidUpdate();
    }
    void ShootRigidUpdate()
    {
        float running = Time.time - startTime;


        if (running < shotTime)
        {
            Vector3 movement = new Vector3(10f,0f,0f);
            canonBallRigid.AddForce(-movement);
        }
    }
    
}
