using UnityEngine;
using System.Collections;

public class Guide : MonoBehaviour {
	
	private Gun objective;
	public Player player;
	private Vector3 station;
		
	// Use this for initialization
	void Start () {
		objective = (Gun) GameObject.FindObjectOfType(typeof(Gun));
		station = Vector3.zero;
		rigidbody.detectCollisions = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 toPlayer = player.transform.position - transform.position;
		Vector3 toStation = station - transform.position;
		print (toPlayer.magnitude);
		print (toStation.magnitude);
		
		if(station == Vector3.zero || (toStation.magnitude < 1 &&
			(toPlayer.magnitude > 50 || toPlayer.magnitude < 20)) ||
			objective == null) {
			if(objective == null)
				objective = (Gun) GameObject.FindObjectOfType(typeof(Gun));
			
			Vector3 playerToObjective = objective.transform.position -
				player.transform.position;
			
			station = Quaternion.Euler(0, Random.Range(-10, 10), 0) *
				playerToObjective.normalized *
					Mathf.Min(50, (int) playerToObjective.magnitude) +
					player.transform.position;
			
			station.y = 5;
		} else if(toStation.magnitude > 1) {
			rigidbody.velocity = toStation.normalized *
				Mathf.Min(20, (int) toStation.magnitude);
		} else
			rigidbody.velocity = Vector3.zero;
	}
}
