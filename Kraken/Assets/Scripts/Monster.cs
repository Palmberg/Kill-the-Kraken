using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
	public float maxRange = 4.5f;
	public float moveSpeed = 1f;
	public float speed = 100;
	public float slowTime;
	public Vector3 monsterPosition;

	//public float attackReduce = 1f;
	private int attackFromRight = 0;
	private int attackFromLeft = 0;

	private bool moveRandomly = true;
	Rigidbody rb;

	private float moveRate = 0.2f;
	private float initialTime = 0.0F;

	private bool moveToRight;
	// Use this for initialization
	void Start () 
	{
		if (Random.value < 0.5f)
			moveToRight = true;
		else
			moveToRight = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
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

        //Win
        Debug.Log(attackFromRight+attackFromLeft);
		if(attackFromLeft + attackFromRight > 40)
		{
            Debug.Log("win");
			Destroy(this.gameObject);
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
			ranY = Random.Range (-2.5f, 2.5f);
		}
		Vector3 randomMovement = new Vector3 (2f, ranY, 0);
		transform.position += (randomMovement / moveSpeed) *( attacks / 25 );
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
			ranY = Random.Range (-2.5f, 2.5f);
		}
		
		Vector3 randomMovement = new Vector3 (-2f, ranY, 0);
		transform.position += (randomMovement / moveSpeed) *( attacks / 25 );
	}
}
