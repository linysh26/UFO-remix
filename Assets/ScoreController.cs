using UnityEngine;
using System.Collections;

public class ScoreController : Singleton<ScoreController> {


	public int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}

	public void addScore(int bonus){
		this.score += bonus;
	}
	public void Restart(){
		score = 0;
	}
}
