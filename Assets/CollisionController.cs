using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	public bool flag = false;
	public Collision collision;
	// Use this for initialization
	void Start () {}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name [0] == 'b' || collision.gameObject.name[0] == 'w') {
			flag = true;
			this.collision = collision;
			Vector3 position = this.transform.position;
			Debug.Log (position.x + ", " + position.y + ", " + position.z);
			Instantiate(Resources.Load("explosion"), new Vector3(position.x - 1, position.y, position.z), new Quaternion(0, 0, 90, 0));
		}
	}
}
