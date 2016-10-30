using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class BadMouse : MonoBehaviour {

	private Vector3 startpos;

	public float speed;
	public float width;
	public float height;

	public GameObject pop;

	float timecounter = 0.0f;

	// Use this for initialization
	void Start () {
		startpos = transform.position;
		speed = 5.0f;
		width = 2.0f;
		height = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timecounter += Time.deltaTime * speed;

		float x = Mathf.Cos (timecounter) * width;
		float z = Mathf.Sin (timecounter)  * height;
		float y = 0;

		transform.position = new Vector3 (x + startpos.x, y + startpos.y, z + startpos.z);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Cat") {
			other.gameObject.SetActive (false);
			GameObject go = Instantiate (pop, other.gameObject.transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			StartCoroutine (Die ());


		}
	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown(0)){
			GameObject go = Instantiate (pop, transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			this.gameObject.SetActive(false);
		}
	}

	IEnumerator Die(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
