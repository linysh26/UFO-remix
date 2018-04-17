using UnityEngine;
using System.Collections;

public class ClayPigeon {

	public bool isFree;
	public float speed;
	public int bonus;
	public int life;
	public int DEF;
	public float type;
	public GameObject clay_pigeon;

	public ClayPigeon(){
		isFree = false;
	}
		
	public GameObject getClayPigeon(){
		return this.clay_pigeon;
	}

	public void setLife(int life){
		this.life = life;
	}

	public void setBonus(int bonus){
		this.bonus = bonus;
	}

	public void setClayPigeon(int speed, int life, float type, Transform firer){
		this.speed = speed;
		this.life = life;
		this.type = type;
		this.bonus = (int)speed/2 + life;
		DEF = 1 + (int)type;
		isFree = false;
		clay_pigeon.transform.position = firer.position;
		clay_pigeon.transform.rotation = firer.rotation;
		this.clay_pigeon.SetActive (true);
		clay_pigeon.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		clay_pigeon.GetComponent<Rigidbody> ().angularDrag = 0;
		clay_pigeon.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
		this.clay_pigeon.GetComponent<Rigidbody> ().isKinematic = false;
	}

	public void Update(){
		this.clay_pigeon.GetComponent<Rigidbody> ().velocity = this.clay_pigeon.transform.TransformDirection (Vector3.forward * speed);
		if (this.clay_pigeon.GetComponent<CollisionController> ().flag) {
			this.OnCollisionEnter (this.clay_pigeon.GetComponent<CollisionController> ().collision);
			this.clay_pigeon.GetComponent<CollisionController> ().flag = false;
		}
	}

	public void OnCollisionEnter(Collision collision){
		Debug.Log (collision.gameObject.name [0]);

		if (collision.gameObject.name [0] == 'b') {
			this.life -= Player.Instance.ATK - this.DEF;
			Player.Instance.recollectBullet ();
			if (this.life <= 0) {
				this.isFree = true;
				ScoreController.Instance.addScore (bonus);
			}
		} 
		else {
			Player.Instance.life -= 1;
			this.isFree = true;
		}
	}

	public void setGameObject(GameObject clay_pigeon){
		this.clay_pigeon = clay_pigeon;
	}

	public void beCollect(){
		this.clay_pigeon.transform.position = new Vector3 (-10, 0, 4);
		this.clay_pigeon.SetActive (false);
		this.clay_pigeon.GetComponent<Rigidbody> ().isKinematic = true;
	}


}
