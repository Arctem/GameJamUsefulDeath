using UnityEngine;
using System.Collections;
using FPS;

public class UserInput : MonoBehaviour {
	private Player player;
	public Bullet bullet;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
			LeftMouseClick();
	}
	
	private void LeftMouseClick() {
		Bullet clone;
		clone = (Bullet) Instantiate(bullet, transform.position + gameObject.transform.forward, transform.rotation);
		clone.rigidbody.velocity = transform.forward * 5;
	}
}
