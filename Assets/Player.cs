using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : Singleton<Player> {

	public int life;
	public int level;
	public int experience;
	public int experience_progress;
	public float fire_speed;
	public float fire_rate;
	public float next_fire;
	public bool canFire;
	public int ATK;
	List<Bullet> clip_free;
	List<Bullet> clip_used;

	public float speedX;
	public float speedY;
	public float X;
	public float Y;

	FirstController scene_controller;

	public Player(){}

	// Use this for initialization
	void Start () {
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
		life = 10;
		ATK = 1;
		fire_speed = 0.5f;
		experience = 100;
		experience = 0;
		level = 1;
		canFire = true;
		clip_free = new List<Bullet> ();
		clip_used = new List<Bullet> ();
		speedX = 200;
		speedY = 200;
		X = 0;
		Y = 0;
	}

	void Update(){
		if (this.life == 0 && !scene_controller.isGameOver)
			scene_controller.isGameOver = true;
		if (!scene_controller.isGameOver) {
			float translationX = Input.GetAxis ("Mouse X") * speedX;
			float translationY = Input.GetAxis ("Mouse Y") * speedY;
			translationX *= Time.deltaTime;
			translationY *= Time.deltaTime;
			X += translationX;
			Y += translationY;
			this.transform.rotation = Quaternion.identity;
			this.transform.RotateAround (this.transform.position, Vector3.right, -Y);
			this.transform.RotateAround (this.transform.position, Vector3.up, X);
		}
		foreach (Bullet bullet in clip_used) {
			if (bullet.enable)
				bullet.Update ();
			else
				recollectBullet ();
		}
		if (Input.GetMouseButtonDown (0) && Time.time > next_fire && scene_controller.status ) {
			this.clip_free.Remove (shoot ());
			next_fire += fire_rate * Time.deltaTime;
		}
	}

	void OnGUI(){
		if(experience_progress >= experience){
			experience_progress -= experience;
			experience += 10 * level + ATK * 3;
			level++;
			GUI.Label (new Rect (), "Level Up");
			new WaitForSeconds (1);
		}
		if (!scene_controller.isGameOver) {
			GUI.Label (new Rect (Screen.width / 2 + 18, Screen.height / 2 - 15, 30, 20), "—");
			GUI.Label (new Rect (Screen.width / 2 - 32, Screen.height / 2 - 15, 30, 20), "—");
			GUI.Label (new Rect (Screen.width / 2 - 2, Screen.height / 2 + 10, 5, 30), "|");
			GUI.Label (new Rect (Screen.width / 2 - 2, Screen.height / 2 - 40, 5, 30), "|");
		}
		GUI.Box (new Rect (Screen.width - 90, 10, 80, 100), "Score " + ScoreController.instance.score);
		GUI.Label (new Rect (Screen.width - 80, 30, 60, 20), "LV: " + level);
		GUI.Label (new Rect (Screen.width - 80, 60, 60, 20), "HP: " + life);
		GUI.Label (new Rect (Screen.width - 80, 90, 60, 20), "ATK: " + ATK);
	}

	public void powerUp(){
		ATK++;
		life = 10;
	}

	public Bullet shoot(){
		Bullet bullet;
		Debug.Log ("shootinginginginginginging!");
		if (clip_free.ToArray().Length == 0) {
			bullet = new Bullet ();
			bullet.setGameObject(Instantiate(Resources.Load("bullet")) as GameObject );
			this.clip_free.Add (bullet);
		}
		bullet = this.clip_free[0];
		bullet.initialize (this.transform);
		clip_used.Add (bullet);
		return bullet;
	}

	public void recollectBullet(){
		Bullet bullet = clip_used [0];
		this.clip_used.Remove (bullet);
		this.clip_free.Add (bullet);
		bullet.beCollect ();
	}

	public void Restart(){
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
		life = 10;
		ATK = 1;
		fire_speed = 0.5f;
		experience = 100;
		experience = 0;
		level = 1;
		canFire = true;
		clip_free = new List<Bullet> ();
		clip_used = new List<Bullet> ();
		speedX = 200;
		speedY = 200;
		X = 0;
		Y = 0;
	}
}
