using UnityEngine;
using System.Collections;

public class CameraAngleController : MonoBehaviour {

	public float speedX;

	FirstController scene_controller;
	// Use this for initialization
	void Start () {
		speedX = 100;
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
	}
	
	// Update is called once per frame
	void Update () {
		if (!scene_controller.isGameOver) {
			float translationX = Input.GetAxis ("Mouse X") * speedX;
			translationX *= Time.deltaTime;
			this.transform.RotateAround (new Vector3(0, 0, 0), Vector3.up, translationX);
		}
	}
}
