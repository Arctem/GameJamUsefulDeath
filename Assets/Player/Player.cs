using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int health = 1;
	private bool isGun;
	public int hasGun = 0;
	public int maxGun = 2;

	// Use this for initialization
	void Start () {
		isGun = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if(isGun)
			GUI.TextArea(new Rect(300, 300, 180, 25), "Press E to pick up gun part");
		
		if(hasGun < maxGun)
			GUI.TextArea(new Rect(10, 10, 150, 25), "Collected "
				+ hasGun + " of " + maxGun + " parts.");
		else
			GUI.TextArea(new Rect(10, 10, 30, 25), "Gun get.");
	}
	
	public void setIsGun(bool b) {
		isGun = b;
		return;
	}
}
