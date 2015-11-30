using UnityEngine;
using System.Collections;

public class KrakenMain : MonoBehaviour
{
    public float maxRange = 4.5f;
    public Vector3 movement = new Vector3(0f, 1f, 0f);
    public Vector3 drawnMovement = new Vector3(0f, 0f, 1f);
    public int moveSpeed = 10;

    public GameObject characterRight;
    public GameObject characterLeft;

    public GameObject canonRightUp;
    public GameObject canonRightMid;
    public GameObject canonRightDown;

    public GameObject canonLeftUp;
    public GameObject canonLeftMid;
    public GameObject canonLeftDown;

    public GameObject canonBall;
    public GameObject bullet;
    public Rigidbody bulletRigid;

    //This distance stands for the distance between the Right player and the right canon
    private double distanceWithRightUp;
    private double distanceWithRightMid;
    private double distanceWithRightDown;

    private double distanceWithLeftUp;
    private double distanceWithLeftMid;
    private double distanceWithLeftDown;

    public Vector3 shootMovement = new Vector3(-100f, 0f, 0f);

    // Use this for initialization
    void Start()
    {
        canonBall = GameObject.Find("CanonBall");
        characterRight = GameObject.Find("CharacterRight");
        characterLeft = GameObject.Find("CharacterLeft");

        canonLeftUp = GameObject.Find("canonLeftUp");
        canonLeftMid = GameObject.Find("canonLeftMid");
        canonLeftDown = GameObject.Find("canonLeftDown");

        canonRightUp = GameObject.Find("canonRightUp");
        canonRightMid = GameObject.Find("canonRightMid");
        canonRightDown = GameObject.Find("canonRightDown");
    }

    // Update is called once per frame
    void Update()
    {
        distanceWithRightUp = Mathf.Abs(characterRight.transform.position.y - canonRightUp.transform.position.y);
        distanceWithRightMid = Mathf.Abs(characterRight.transform.position.y - canonRightMid.transform.position.y);
        distanceWithRightDown = Mathf.Abs(characterRight.transform.position.y - canonRightDown.transform.position.y);

        distanceWithLeftUp = Mathf.Abs(characterLeft.transform.position.y - canonLeftUp.transform.position.y);
        distanceWithLeftMid = Mathf.Abs(characterLeft.transform.position.y - canonLeftMid.transform.position.y);
        distanceWithLeftDown = Mathf.Abs(characterLeft.transform.position.y - canonLeftDown.transform.position.y);

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
            if (distanceWithRightUp < 1)
            {
                shoot(canonRightUp);
            }
            else if (distanceWithRightMid < 1)
            {
                shoot(canonRightMid);
            }
            else if (distanceWithRightDown < 1)
            {
                shoot(canonRightDown);
            }
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
    void shoot(GameObject canon)
    {
        float canonPositionY = canon.transform.position.y;

        // Right Canon
        if (canon.transform.position.x > 0)
        {
            bullet = (GameObject)Instantiate(canonBall, new Vector3(6f, canonPositionY, -2.27f), Quaternion.identity);
            bulletRigid = bullet.GetComponent<Rigidbody>();
            bulletRigid.AddForce(shootMovement);


            ////Debug.Log("Bullet X position " + bullet.transform.position.x);
            ////Once the bullet is fired, we cannot control them anymore
            //if (bullet.transform.position.x < 0)
            //{
            //    Debug.Log("Bullet X position " + bullet.transform.position.x);
            //    bullet.transform.position -= drawnMovement;
            //}
        }
        else
        {
            bullet = (GameObject)Instantiate(canonBall, new Vector3(-6f, canonPositionY, -2.27f), Quaternion.identity);
            bulletRigid = bullet.GetComponent<Rigidbody>();
            bulletRigid.AddForce(-shootMovement);
        }

    }

}
