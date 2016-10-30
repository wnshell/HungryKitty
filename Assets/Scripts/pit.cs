using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pit : MonoBehaviour {

	public GameObject pop;

	public GameObject deathScreen;


	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Cat") {
			coll.gameObject.SetActive (false);
			GameObject go2 = Instantiate (deathScreen, coll.gameObject.transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			GameObject go = Instantiate (pop, coll.gameObject.transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			StartCoroutine (Die ());
		}
	}

	IEnumerator Die(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
