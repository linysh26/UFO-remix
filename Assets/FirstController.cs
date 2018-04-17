using UnityEngine;
using System.Collections;



/**
 * Since I was going to let the very object to care about its own deal, so I let the object's controller communicate with the 
 * 
 * action manager instead dealing cases in the SceneController
 * */


public class FirstController : MonoBehaviour, SceneController{

	Director director;

	public bool status;
	public bool isGameOver;

	GameObject wall;
	GameObject explosion;

	void Awake(){
		director = Director.getInstance ();
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		isGameOver = false;
		status = true;
	}

	void OnGUI(){

		GUI.Box (new Rect (10, 10, 100, 80), "Menu");

		if (GUI.Button (new Rect (30, 30, 60, 40), "Restart")) {
			Restart ();
		}

		if (isGameOver) {
			status = false;
			GUI.Box (new Rect(Screen.width/2 - 50, Screen.height/2 - 40, 100, 80), "");
			if (GUI.Button (new Rect (Screen.width / 2 - 30, Screen.height / 2 - 20, 60, 40), "Restart")) {
				Restart ();
			}
		}
	}

	void Update(){
		if (isGameOver) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			if (explosion != null) {
				explosion = Instantiate (Resources.Load ("explosion"), wall.transform.position, Quaternion.identity) as GameObject;
				explosion.GetComponent<Explosion> ().decrement = -10;
			}
			if(wall != null)
				Destroy (wall);
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void LoadResources(){
		wall = (GameObject)Instantiate (Resources.Load("wall"), new Vector3(7, 0, 15), Quaternion.identity);
		Player initial_p = Player.Instance;
		ClayPigeonFirer initial_cpf = ClayPigeonFirer.Instance;
		ScoreController initial_s = ScoreController.Instance;
		RoundController initial_r = RoundController.Instance;
	}
	public void Restart(){
		Destroy (explosion);
		wall = (GameObject)Instantiate (Resources.Load("wall"), new Vector3(7, 0, 15), Quaternion.identity);
		RoundController.Instance.Restart ();
		ScoreController.Instance.Restart ();
		Player.Instance.Restart ();
		ClayPigeonFirer.Instance.Restart ();
		status = true;
		isGameOver = false;
	}
}
