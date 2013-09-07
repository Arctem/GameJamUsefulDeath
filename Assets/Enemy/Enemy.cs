using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	CharacterController controller;

	// Use this for initialization
	void Start() {
		controller = transform.root.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update() {
		controller.SimpleMove(new Vector3(3, 0, 0));
	}
}
