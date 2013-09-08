using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int health = 1;
	private bool isGun;

	// Use this for initialization
	void Start () {
		isGun = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		print (isGun);
		if(isGun)
			GUI.TextArea(new Rect(10, 10, 150, 100), "Press E to pick up gun part");
	}
	
	public void setIsGun(bool b) {
		print ("setIsGun method");
		isGun = b;
		return;
	}
}
