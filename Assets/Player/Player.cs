using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int health = 1;
	private bool dead;
	private bool isGun;
	private int deadCount = 0;
	public int hasGun = 0;
	public int maxGun = 2;
	
	public Ragdoll ragdoll;
	public Gun gun;
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		isGun = false;
		dead = false;
		controller = transform.root.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead && health <= 0) {
			dead = true;
			deadCount = 0;
			Instantiate(ragdoll, transform.position, transform.rotation);
			for(int i  = 0; i < hasGun; i++) {
				Instantiate(gun, transform.position +  transform.up * i,
					Random.rotation);
			}
			hasGun = 0;
			transform.root.GetComponent<CharacterMotor>().movement.gravity = 0;

			//controller.enabled = false;
		}
		if(dead) {
			//transform.root.GetComponentInChildren<Camera>().gameObject.
			//	transform.LookAt(transform.position - transform.up);
			controller.Move(Vector3.up / 2);
			deadCount++;
			if(deadCount > 200) {
				dead = false;
				health = 1;
				controller.Move(new Vector3(Random.Range(-40, 40),
					15, Random.Range(-40, 40)) - transform.position);
			}
		} else {
			transform.root.GetComponent<CharacterMotor>().movement.gravity = 20;
			transform.root.GetComponent<MouseLook>().enabled = true;
			transform.root.GetComponentInChildren<MouseLook>().enabled = true;
		}
	}
	
	void OnGUI() {
		if(isGun)
			GUI.TextArea(new Rect(300, 300, 180, 25), "Press E to pick up gun part");
		
		if(hasGun < maxGun)
			GUI.TextArea(new Rect(10, 10, 150, 25), "Collected "
				+ hasGun + " of " + maxGun + " parts.");
		else
			GUI.TextArea(new Rect(10, 10, 50, 25), "Gun get.");
		
		
	}
	
	public bool Damage(int dmg) {
		health -= dmg;
		if(health == 0)
			return true;
		return false;
	}
	
	public bool Dead() {
		return dead;
	}
	
	public void setIsGun(bool b) {
		isGun = b;
		return;
	}
}
