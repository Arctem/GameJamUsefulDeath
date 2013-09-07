﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	CharacterController controller;
	GameObject player;
	private Vector3 moveDirection = Vector3.zero;
	
	public int health;
	

	// Use this for initialization
	void Start() {
		controller = transform.root.GetComponent<CharacterController>();
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update() {
		Vector3 toPlayer = player.transform.position - transform.position;
		transform.LookAt(player.transform);
		
		if(!Physics.Raycast(transform.position, toPlayer, toPlayer.magnitude)) {
			moveDirection = toPlayer.normalized * 5;
		} else
			moveDirection = Vector3.zero;
	
		
		controller.Move(moveDirection * Time.deltaTime);
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
