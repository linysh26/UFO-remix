using UnityEngine;
using System.Collections;

public class Bullet {

	public float speed;
	public GameObject bullet;
	public bool enable;
	// Use this for initialization
	public Bullet(){
		this.speed = 100.0f;
		enable = true;
	}

	void Start () {
		
	}

	public void initialize(Transform player){
		bullet.transform.position = player.position;
		bullet.transform.rotation = player.rotation;
		bullet.SetActive (true);
		bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		bullet.GetComponent<Rigidbody> ().angularDrag = 0;
		bullet.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
		bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		bullet.GetComponent<Rigidbody> ().isKinematic = false;
	}

	public void Update(){
		this.bullet.GetComponent<Rigidbody> ().velocity = this.bullet.transform.TransformDirection (Vector3.forward * speed);
		if (bullet.transform.position.z > 20)
			enable = false;
	}

	public void setGameObject(GameObject bullet){
		this.bullet = bullet;
	}

	public GameObject getBullet(){
		return this.bullet;
	}

	public void beCollect(){
		enable = true;
		this.bullet.transform.position = new Vector3 (0, 0, 0);
		bullet.GetComponent<Rigidbody> ().isKinematic = true;
		bullet.SetActive (false);
	}
}
