using UnityEngine;
using System.Collections;

public class dogFollow : MonoBehaviour {

	public GameObject cat;
	private Vector3 target;
	public float speed;
	public float minspeed;
	public float maxspeed;


	// Use this for initialization
	void Start () {
		cat = GameObject.Find ("Cat");
	}

	// Update is called once per frame
	void Update () {
		target = cat.transform.position;
		target.y = 7.0f;
		Vector3 diff = target - transform.position;
		float mag = diff.magnitude;

		if (mag < minspeed) {
			mag = minspeed;
		} else if (mag > maxspeed) {
			mag = maxspeed;
		}

		GetComponent<Rigidbody> ().transform.eulerAngles = new Vector3 (0, Mathf.Atan2 ((target.x - transform.position.x), (target.z - transform.position.z)) * Mathf.Rad2Deg, 0);
		transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime * mag);
	}
}
