using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	CharacterController controller;
	GameObject player;

	// Use this for initialization
	void Start() {
		controller = transform.root.GetComponent<CharacterController>();
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update() {
		Vector3 move = player.transform.position - transform.position;
		
		if(!Physics.Raycast(transform.position, move, move.magnitude))
			controller.SimpleMove(move.normalized);
	}
}
