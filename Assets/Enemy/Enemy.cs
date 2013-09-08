using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	CharacterController controller;
	GameObject player;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 destination = Vector3.zero;
	
	public int health;
	
	enum AIMode {None, Wander, Circle, Charge, Eating};
	AIMode nextMode = AIMode.Wander;
	AIMode mode = AIMode.Wander;
	AIMode prevMode = AIMode.None;
	

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
			
			if(toPlayer.magnitude < 30 && 
				!Physics.Raycast(transform.position, toPlayer, toPlayer.magnitude)) {
				nextMode = AIMode.Circle;
			}
			break;
		case AIMode.Circle:
			Vector3 route = Quaternion.Euler(0, -90, 0) * toPlayer;
			
			//If too far away, wander. Otherwise, try to reach distance of 25.
			//If we get too close... I dunno. Might panic or something.
			if(toPlayer.magnitude > 40)
				nextMode = AIMode.Wander;
			if(toPlayer.magnitude < 40 && toPlayer.magnitude > 10)
				route = Quaternion.Euler(0, -90 - 3 * (35 - toPlayer.magnitude), 0) * toPlayer;
			transform.LookAt(transform.position + route);
			rigidbody.velocity = route.normalized * 6;
			break;
		case AIMode.Charge:
			transform.LookAt(player.transform);
			rigidbody.velocity = toPlayer.normalized * 6;
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
