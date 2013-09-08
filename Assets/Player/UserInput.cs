using UnityEngine;
using System.Collections;
using FPS;

public class UserInput : MonoBehaviour {
	private Player player;
	public Bullet bullet;
	public Enemy enemy;
	private Camera camera;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent<Player>();
		camera = transform.root.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
			LeftMouseClick();
		
		if(Input.GetKeyDown(KeyCode.L))
			player.Damage(1);
		
		RaycastHit[] hits;
		hits = Physics.CapsuleCastAll(camera.transform.position,
			camera.transform.position + camera.transform.forward * 0.5f,
			1f, camera.transform.forward, 2f);
		
		player.gameObject.GetComponent<Player>().setIsGun(false);
		for(int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];

			if(hit.collider.tag == "Gun") {
				player.gameObject.GetComponent<Player>().setIsGun(true);
				if(Input.GetKeyDown("e")) {
					Destroy(hit.collider.gameObject);
					player.hasGun++;
					Vector3 spawnLocation;
					int worldSize = GameObject.Find("Ground").GetComponent<GenerateWorld>().worldSize;
					do {
						spawnLocation = new Vector3(Random.Range(-worldSize, worldSize),
							2, Random.Range(-worldSize, worldSize));
					} while((spawnLocation - player.transform.position).magnitude < 60);
					
					Instantiate(enemy, spawnLocation, Quaternion.identity);
					
					player.gameObject.GetComponent<Player>().setIsGun(false);
				}
			} //end if gun in hits
		} //end checking what it hit
	}
	
	private void LeftMouseClick() {
		if(player.hasGun >= player.maxGun && !player.Dead()) {
			Bullet clone;
			clone = (Bullet) Instantiate(bullet,
				camera.transform.position + camera.transform.forward,
				camera.transform.rotation);
			clone.rigidbody.velocity = camera.transform.forward * 40;
		}
	}
}
