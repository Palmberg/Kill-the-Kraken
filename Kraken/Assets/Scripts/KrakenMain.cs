using UnityEngine;
using System.Collections;

public class KrakenMain : MonoBehaviour
{
    public float maxRange = 4.5f;
    public Vector3 movement = new Vector3(0f, 1f, 0f);
    public Vector3 drawnMovement = new Vector3(0f, 0f, 1f);
    public int moveSpeed = 10;
    private float rightTime=0f;
    private float oldRightTime=0f;
    private float leftTime=0f;
    private float oldLeftTime=0f;

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

	//Animation
	private Animator animLeftCharacter;
	private Animator animRightCharacter;
	private Vector3 initialRotationRight;
	private Vector3 initialRotationLeft;

   // public Vector3 shootMovement = new Vector3(100f, 0f, 100f);

    // Use this for initialization
    void Start()
    {
        //canonBall = GameObject.Find("CanonBall");
        characterRight = GameObject.Find("CharacterRight");
        characterLeft = GameObject.Find("CharacterLeft");

        canonLeftUp = GameObject.Find("canonLeftUp");
        canonLeftMid = GameObject.Find("canonLeftMid");
        canonLeftDown = GameObject.Find("canonLeftDown");

        canonRightUp = GameObject.Find("canonRightUp");
        canonRightMid = GameObject.Find("canonRightMid");
        canonRightDown = GameObject.Find("canonRightDown");

		animLeftCharacter = characterLeft.GetComponent<Animator>();
		animRightCharacter = characterRight.GetComponent<Animator>();
		initialRotationRight = characterRight.transform.rotation.eulerAngles;
		initialRotationLeft = characterLeft.transform.rotation.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
		bool rightCharacterRunning = false;
		bool leftCharacterRunning = false;

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
            //var zl = touchPositionL.z;
            var xr = (touchPositionR.x - 960) / 108;
            var yr = (touchPositionR.y - 540) / 108;
            //var zr = touchPositionR.z;
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
				characterRight.transform.localEulerAngles = new Vector3(-90f,0f, 0f);
				rightCharacterRunning = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (characterRight.transform.position.y >= -maxRange)
            {
                characterRight.transform.position -= (movement / moveSpeed);
				characterRight.transform.localEulerAngles = new Vector3(90f, 180f, 0f);
				rightCharacterRunning = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rightShot();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (characterLeft.transform.position.y <= maxRange)
            {
                characterLeft.transform.position += (movement / moveSpeed);
				characterLeft.transform.localEulerAngles = new Vector3(-90f,0f, 0f);
				leftCharacterRunning = true;
            }
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            if (characterLeft.transform.position.y >= -maxRange)
            {
                characterLeft.transform.position -= (movement / moveSpeed);
				characterLeft.transform.localEulerAngles = new Vector3(90f, 180f, 0f);
				leftCharacterRunning = true;
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            Application.LoadLevel("World");
        }

		if (!rightCharacterRunning) {
			characterRight.transform.eulerAngles = initialRotationRight;
		}
		if (!leftCharacterRunning) {
			characterLeft.transform.eulerAngles = initialRotationLeft;
			//characterLeft.transform.localEulerAngles = new Vector3(0f,90f, -90f);
		}
		animRightCharacter.SetBool ("IsRunning", rightCharacterRunning);
		animLeftCharacter.SetBool ("IsRunning", leftCharacterRunning);
    }
    public void rightShot()
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
    public void leftShot()
    {
        if (distanceWithLeftUp < 1)
        {
            shoot(canonLeftUp);
        }
        else if (distanceWithLeftMid < 1)
        {
            shoot(canonLeftMid);
        }
        else if (distanceWithLeftDown < 1)
        {
            shoot(canonLeftDown);
        }
    }
    void shoot(GameObject canon)
    {
        float canonPositionY = canon.transform.position.y;

        // Right Canon
        if (canon.transform.position.x > 0)
        {
            rightTime = Time.time;
            var rtime = rightTime - oldRightTime;
            if (rtime > 0.3f)
            { 
                bullet = (GameObject)Instantiate(canonBall, new Vector3(5f, canonPositionY, -1.01f), Quaternion.identity);
                bulletRigid = bullet.GetComponent<Rigidbody>();
                bulletRigid.AddForce(-100f,0f,0f);
                oldRightTime = rightTime;
            }
        }
        else
        {
            leftTime = Time.time;
            var ltime = leftTime - oldLeftTime;
            if (ltime > 0.3f) {
                bullet = (GameObject)Instantiate(canonBall, new Vector3(-5f, canonPositionY, -1.01f), Quaternion.identity);
                bulletRigid = bullet.GetComponent<Rigidbody>();
                bulletRigid.AddForce(100f, 0f, 0f);
                oldLeftTime = leftTime;
            }
            
        }

    }

}
