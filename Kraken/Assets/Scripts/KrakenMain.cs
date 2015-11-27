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
