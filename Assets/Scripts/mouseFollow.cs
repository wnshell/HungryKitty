using UnityEngine;
using System.Collections;

public class mouseFollow: MonoBehaviour {
	private Vector3 target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((target.y - transform.position.y), (target.x - transform.position.x))*Mathf.Rad2Deg - 90);

	}
}
