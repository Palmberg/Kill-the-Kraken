using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Monster : MonoBehaviour 
{
    public AudioSource loseLife;
    public AudioSource gameOver;
    public AudioSource fightMusic;
	public float maxRange = 4.5f;
	public float moveSpeed;
//	public float slowTime;
	public Vector3 monsterPosition;
	private Vector3 originalPosition = new Vector3 (0f, 0f, 0f);
	private Vector3 smallerSize = new Vector3(0.8F, 0.8f, 1f);
	private Vector3 smallestSize = new Vector3(0.6F, 0.6f, 1f);

	public Text winText;
    public GameObject wText;
    public GameObject lText;
    public GameObject backButton;



	//public float attackReduce = 1f;
	private int attackFromRight = 0;
	private int attackFromLeft = 0;

	private int totalAttack = 0;

	private bool moveRandomly = true;
	private GameObject rightPlayer;
	private GameObject leftPlayer;

	private GameObject rightHert1;
	private GameObject rightHert2;
	private GameObject rightHert3;

	private GameObject leftHert1;
	private GameObject leftHert2;
	private GameObject leftHert3;
	
	private float moveRate = 0.2f;
	private float initialTime = 0.0F;

	private bool moveToRight;

	private int rightPlayerDeath = 0;
	private int leftPlayerDeath = 0;
	// Use this for initialization
	void Start () 
	{
		if (Random.value < 0.5f)
			moveToRight = true;
		else
			moveToRight = false;
        wText.SetActive(false);
        lText.SetActive(false);
        backButton.SetActive(false);

		winText.text = "";

		rightPlayer = GameObject.Find("CharacterRight");
		leftPlayer = GameObject.Find("CharacterLeft");

		rightHert1 = GameObject.Find ("rh1");
		rightHert2 = GameObject.Find ("rh2");
		rightHert3 = GameObject.Find ("rh3");

		leftHert1 = GameObject.Find ("lh1");
		leftHert2 = GameObject.Find ("lh2");
		leftHert3 = GameObject.Find ("lh3");



	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log("Total : " + totalAttack);
		if (moveRandomly & Time.time > initialTime) 
		{ 
			moveRandom();
			initialTime = Time.time + moveRate;
		}
		else if(attackFromRight > attackFromLeft & Time.time > initialTime )
		{
			moveRight(attackFromRight);
			initialTime = Time.time + moveRate;
		}
		else if(Time.time > initialTime)
		{
			moveLeft(attackFromLeft);
			initialTime = Time.time + moveRate;
		}

		if (totalAttack > 20 && totalAttack < 30) 
		{
			Debug.Log("Smaller");
			transform.localScale = smallerSize;
		}
		if (totalAttack >= 30 && totalAttack < 50) 
		{
			Debug.Log("Smallest");

			transform.localScale = smallestSize;
		}


        //Win
		if(totalAttack > 40)
		{
            //winText.text = "You Win!!";
            wText.SetActive(true);
			gameObject.SetActive(false);
            backButton.SetActive(true);
			//Destroy(this.gameObject);
		}

		//Lose
		if(transform.position.x > maxRange)
		{
			Debug.Log("Death Times Right:" + rightPlayerDeath);
			rightPlayerDeath++;

			if(rightHert1.activeSelf)
			{
				rightHert1.SetActive(false);

                loseLife.Play();
            }
			else if(rightHert2.activeSelf)
			{
				rightHert2.SetActive(false);

                loseLife.Play();
            }
			else
			{
				rightHert3.SetActive(false);
                fightMusic.Stop();
                gameOver.Play();
            }
			transform.position = originalPosition;
			attackFromRight = 0;
			attackFromLeft = 0;
			rotateToLeft();
			moveLeft(attackFromLeft);
		}
		if(transform.position.x < -maxRange)
		{
			Debug.Log("Death Times Left:" + leftPlayerDeath);
			leftPlayerDeath++;

			if(leftHert1.activeSelf)
			{
				leftHert1.SetActive(false);

                loseLife.Play();
            }
			else if(rightHert2.activeSelf)
			{
				leftHert2.SetActive(false);

                loseLife.Play();
            }
			else
			{
				leftHert3.SetActive(false);
                fightMusic.Stop();
                gameOver.Play();
            }
            transform.position = originalPosition;
			attackFromRight = 0;
			attackFromLeft = 0;
			rotateToRight();
			moveRight(attackFromRight);
		}

		if(leftPlayerDeath > 2 || rightPlayerDeath > 2)
		{
            //Lose
            //winText.text = "Try Again";
            
            lText.SetActive(true);
			leftPlayer.SetActive(false);
			rightPlayer.SetActive(false);
            backButton.SetActive(true);
            //Application.LoadLevel("World");
//			Destroy(leftPlayer);
//			Destroy(rightPlayer);
		}

	}
	void rotateToRight()
	{

		Vector3 rotateToRight = new Vector3(0f, 0f, -10f);
		float angleDifference = Mathf.Abs (transform.eulerAngles.z - 270);
        
		if(angleDifference > 0)
		{
			transform.Rotate(rotateToRight);
		}
	}
	void rotateToLeft()
	{
		
		Vector3 rotateToRight = new Vector3(0f, 0f, 10f);
		float angleDifference = Mathf.Abs (transform.eulerAngles.z - 90);
		if(angleDifference > 0)
		{
			transform.Rotate(rotateToRight);
		}
	}
	void moveRandom()
	{
		/*
		float ranX = Random.Range (-10f, 10f);
		float ranY = Random.Range (-10f, 10f);
		Vector3 randomMovement = new Vector3 (ranX, ranY, 0);
		if(transform.position.x < maxRange && transform.position.y < maxRange && transform.position.x > -maxRange && transform.position.y > -maxRange)
		{
			transform.position += randomMovement * Time.deltaTime;
		}
		float step = speed * Time.deltaTime;
		float ranX = Random.Range (-100f, 100f);
		float ranY = Random.Range (-100f, 100f);

		Vector3 randomMovement = new Vector3 (ranX, ranY, 0);
		// move object towards the next path point
		transform.position = Vector3.Lerp(transform.position,randomMovement, slowTime);
		*/

		Vector3 movement = new Vector3 (2f, 0, 0);
		if (moveToRight) 
		{
			transform.position += movement * Time.deltaTime;
			rotateToRight();
			//float angleDifference = transform.eulerAngles.x - 
			//transform.Rotate(Vector3,Time.deltaTime);
			//rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * (180 / Mathf.PI) * Time.deltaTime);
		}
		else
		{
			transform.position -= movement * Time.deltaTime;
			rotateToLeft();

		}
	}

	void OnCollisionEnter (Collision col)
	{
		moveRandomly = false;
		totalAttack++;
		//Debug.Log("Collision Happens");

		if(col.transform.position.x > transform.position.x)
		{
			attackFromRight++;
			//Debug.Log("attackFromRight : " + attackFromRight);
		}
		else if(col.transform.position.x < transform.position.x)
		{
			attackFromLeft++;
			//Debug.Log("attackFromLeft : " + attackFromLeft);
		}
		Destroy(col.gameObject);
	}

	void moveRight(int attacks)
	{
		//Debug.Log ("Move to Right");
		rotateToRight ();
		float ranY;
		if (transform.position.y > maxRange) {
			ranY = -5f;
		} else if (transform.position.y < -maxRange) {
			ranY = 5f;
		} else {
			ranY = Random.Range (0f, 2.5f);
		}
		Vector3 randomMovement = new Vector3 (2f, ranY, 0);
		float increase = attacks / 25f;
		transform.position += (randomMovement / moveSpeed) * increase;
	}
	void moveLeft(int attacks)
	{
		//Debug.Log ("Move to Left");
		rotateToLeft ();
		float ranY;
		/*
		float ranY = Random.Range (-100f, 100f);
		Vector3 randomMovement = new Vector3 (2, ranY, 0);
		transform.position -= (randomMovement / moveSpeed) *( attacks / 5 );
		*/
		if (transform.position.y > maxRange) {
			ranY = -5f;
		} else if (transform.position.y < -maxRange) {
			ranY = 5f;
		} else {
			ranY = Random.Range (-2.5f, 0f);
		}
		
		Vector3 randomMovement = new Vector3 (-2f, ranY, 0);
		float increase = attacks / 25f;
		transform.position += (randomMovement / moveSpeed) * increase;
		//transform.position += (randomMovement / moveSpeed) *( attacks / 25 );
	}
    public void reset()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        if (rightPlayer.activeSelf == false)
        {
            rightPlayer.SetActive(true);
        }
        if (leftPlayer.activeSelf == false)
        {
            leftPlayer.SetActive(true);
        }
        if (rightHert1.activeSelf == false)
        {
            rightHert1.SetActive(true);
        }
        if (rightHert2.activeSelf == false)
        {
            rightHert2.SetActive(true);
        }
        if (rightHert3.activeSelf == false)
        {
            rightHert3.SetActive(true);
        }
        if (leftHert1.activeSelf == false)
        {
            leftHert1.SetActive(true);
        }
        if (leftHert2.activeSelf == false)
        {
            leftHert2.SetActive(true);
        }
        if (leftHert3.activeSelf == false)
        {
            leftHert3.SetActive(true);
        }
        if (wText.activeSelf == true)
        {
            wText.SetActive(false);
        }
        if (lText.activeSelf == true)
        {
            lText.SetActive(false);
        }
        if (backButton.activeSelf == true)
        {
            backButton.SetActive(false);
        }
    }
}
