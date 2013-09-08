using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	CharacterController controller;
	GameObject player;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 destination = Vector3.zero;
	
	public AudioClip chargeSound;
	
	private GameObject food;
	private int eatProgress = 0;
	public int eatTime = 1200;
	
	private Vector3 lastPos;
	private float lastCheckTime = 0;
	private float checkTime = 1f;
	private float moveDist = 1f;
	
	public int health;
	
	enum AIMode {None, Wander, Circle, Charge, Eat};
	private AIMode nextMode = AIMode.Wander;
	private AIMode mode = AIMode.Wander;
	private AIMode prevMode = AIMode.None;
	
	public int circleRadius = 30;
	public int noticeRadius = 35;
	public int forgetRadius = 40;
	public int panicRadius = 5;
	private bool reverseCircle = false;
	private int watchCount = 0;
	

	// Use this for initialization
	void Start() {
		controller = transform.root.GetComponent<CharacterController>();
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update() {
		Vector3 toPlayer = player.transform.position - transform.position;
		
		bool notMoving = CheckMovement();
		
		
		switch(mode) {
		case AIMode.None:
			nextMode = AIMode.Wander;
			break;
		case AIMode.Wander:
			Vector3 toDestination = destination - transform.position;
			if(destination == Vector3.zero || prevMode != AIMode.Wander
				|| toDestination.magnitude < 1 || notMoving) {
				destination = Quaternion.Euler(0, Random.Range(-45, 45), 0) *
					toPlayer.normalized *
					Random.Range(toPlayer.magnitude / 2, toPlayer.magnitude);
				//destination = transform.position +
				//	new Vector3(Random.Range(-100, 100), 0,
				//		Random.Range(-100, 100));
				destination.y = 1;
			}
			transform.LookAt(destination);
			rigidbody.velocity = toDestination.normalized * 5;
			
			if(toPlayer.magnitude < noticeRadius && 
				!Physics.Raycast(transform.position, toPlayer, toPlayer.magnitude)) {
				nextMode = AIMode.Circle;
			}
			break;
		case AIMode.Circle:
			if(prevMode != AIMode.Circle)
				watchCount = 0;
			
			if(player.GetComponent<Player>().Dead())
				nextMode = AIMode.Wander;
			
			if(!Physics.Raycast(transform.position, toPlayer, toPlayer.magnitude))
				watchCount++;
			if(watchCount >= 400)
				nextMode = AIMode.Charge;
			
			int rotation = -90;
			//If too far away, wander. Otherwise, try to reach distance of 25.
			//If we get too close... I dunno. Might panic or something.
			if(toPlayer.magnitude > forgetRadius)
				nextMode = AIMode.Wander;
			if(toPlayer.magnitude < forgetRadius /*&& toPlayer.magnitude > panicRadius*/)
				rotation = -90 - 3 * (circleRadius - (int) toPlayer.magnitude);
			
			if(reverseCircle)
				rotation *= -1;
			
			Vector3 route = Quaternion.Euler(0, rotation, 0) * toPlayer;
			transform.LookAt(transform.position + route);
			rigidbody.velocity = route.normalized * 8;
			
			//if(Physics.Raycast(transform.position, transform.forward, 1))
			if(notMoving)
				reverseCircle = !reverseCircle;
			break;
		case AIMode.Charge:
			if(prevMode != AIMode.Charge) {
				moveDirection = toPlayer.normalized;
				AudioSource.PlayClipAtPoint(chargeSound, Camera.main.transform.position);
			}
			transform.LookAt(transform.position + moveDirection);
			rigidbody.velocity = moveDirection.normalized * 20;
			
			if(notMoving)
				nextMode = AIMode.Wander;
			break;
			
		case AIMode.Eat:
			if(prevMode != AIMode.Eat) {
				food = null;
				eatProgress = 0;
				foreach(GameObject r in GameObject.FindGameObjectsWithTag("Corpse"))
					if(food == null)
						food = r;
					else
						if((transform.position - r.transform.position).magnitude < 
							(transform.position - food.transform.position).magnitude)
							food = r;
			}
			transform.LookAt(food.transform.FindChild("Hips").transform);
			if((transform.position - food.transform.FindChild("Hips").
				transform.position).magnitude > 1)
				rigidbody.velocity = transform.forward.normalized;
			else {
				eatProgress++;
				//print(eatProgress);
				print(food);
				if(eatProgress >= eatTime) {
					Destroy(food);
					food = null;
					eatProgress = 0;
					nextMode = AIMode.Wander;
				}
			}
				
			
				
			break;
		}
		prevMode = mode;
		mode = nextMode;
		
		/*if(!Physics.Raycast(transform.position, toPlayer, toPlayer.magnitude)) {
			moveDirection = toPlayer.normalized * 5;
		} else
			moveDirection = Vector3.zero;*/
	
		
		//controller.Move(moveDirection * Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision collision) {
		destination = Vector3.zero;
		if(collision.collider.gameObject.tag == "Player") {
			GameObject.Find("Player").GetComponent<Player>().Damage(1);
			nextMode = AIMode.Eat;
		}
	}
	
	//Returns true if not moving.
	private bool CheckMovement() {
		if((Time.time - lastCheckTime) > checkTime) {
			float dist = (transform.position - lastPos).magnitude;
			lastCheckTime = Time.time;
			lastPos = transform.position;
			
			if(dist < moveDist) {
				return true;
			}
		}
		return false;
	}
	
	public bool Damage(int dmg) {
		health -= dmg;
		if(health <= 0) {
			Destroy(gameObject);
			return true;
		}
		return false;
	}
}
