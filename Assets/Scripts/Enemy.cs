using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum enemyState{
	LEFT,
	RIGHT
}

public class Enemy : MonoBehaviour {
	public float speed;

	public enemyState es;

	public Transform point1;
	public Transform point2;

	private Vector3 target;
	public GameObject pop;

	public GameObject deathScreen;


	// Use this for initialization
	void Start () {
		target = point1.position;
	}
	
	// Update is called once per frame
	void Update () {

	Vector3 diff1 = transform.position - point1.position;
	Vector3 diff2 = transform.position - point2.position;


	if(diff1.magnitude <= 0.1f){
		es = enemyState.RIGHT;
			target = point2.position;
	}	
	else if(diff2.magnitude <= 0.1f){
		es = enemyState.LEFT;
			target = point1.position;
	}

		transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
		transform.LookAt (target);
		transform.rotation *= Quaternion.Euler(0, -90,0);

	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Cat") {
			coll.gameObject.SetActive (false);
			GameObject go = Instantiate (pop, coll.gameObject.transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			GameObject go2 = Instantiate (deathScreen, coll.gameObject.transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			StartCoroutine (Die ());
		}
	}
	IEnumerator Die(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
