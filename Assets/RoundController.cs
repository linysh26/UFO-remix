using UnityEngine;
using System.Collections;

public class RoundController : Singleton<RoundController> {


	public int current_round { set; get; }
	public int rest { set; get; }

	void Start(){
		this.rest = 10;
		this.current_round = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (rest == 0) {
			ClayPigeonFirer.instance.fire_rate *= 0.8f;
			current_round++;
			rest = 10 + current_round;
		}
	}

	void OnGUI(){
		GUI.Label (new Rect (Screen.width/2 - 20, 0, 60, 20), "Round: " + current_round);
		GUI.Label (new Rect (Screen.width / 2 - 20, 20, 40, 20), "" + Time.time);
		GUI.Label (new Rect (Screen.width / 2 - 20, 40, 40, 20), "" + ClayPigeonFirer.instance.next_fire);
	}

	public int getNumber(){
		return Random.Range (0, Mathf.Min(current_round/3 + 1, rest));
	}

	public void Restart(){
		this.current_round = 1;
		this.rest = 10;
	}
}
