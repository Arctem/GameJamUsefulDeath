﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public int lifetime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;
		if(lifetime <= 0)
			Destroy (gameObject);
	}
	
	void OnCollisionEnter(Collision collision) {
		if(!(collision.collider is Player))
			Destroy(gameObject);
	}
}
