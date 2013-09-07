using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	CharacterController controller;
	GameObject player;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 destination = Vector3.zero;
	
	public int health;
	
	enum AIMode {None, Wander, Circle, Charge};
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
		
		switch(mode) {
		case AIMode.Wander:
			Vector3 toDestination = destination - transform.position;
			if(destination == Vector3.zero || prevMode != AIMode.Wander || toDestination.magnitude < 1) {
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
			transform.LookAt(player.transform);
			rigidbody.velocity = toPlayer.normalized * 6;
			break;
		case AIMode.Charge:
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
