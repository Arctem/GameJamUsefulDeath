using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	CharacterController controller;
	GameObject player;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 destination = Vector3.zero;
	
	public int health;
	
	enum AIMode {None, Wander, Circle, Charge, Eating};
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
		//rigidbody.AddForce(toPlayer.normalized * 5);
		//transform.LookAt(player.transform);
		print (toPlayer.magnitude);
		
		switch(mode) {
		case AIMode.None:
			mode = AIMode.Wander;
			break;
		case AIMode.Wander:
			Vector3 toDestination = destination - transform.position;
			if(destination == Vector3.zero || prevMode != AIMode.Wander
				|| toDestination.magnitude < 1) {
				destination = transform.position +
					new Vector3(Random.Range(-10, 10), 0,
						Random.Range(-10, 10));
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
			
			if(Physics.Raycast(transform.position, transform.forward, 1))
				reverseCircle = !reverseCircle;
			break;
		case AIMode.Charge:
			transform.LookAt(player.transform);
			rigidbody.velocity = toPlayer.normalized * 20;
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
