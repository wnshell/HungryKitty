using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class endgame : MonoBehaviour {

	public GameObject endGame;
	public GameObject dog;
	public GameObject pop;


	void Start(){
		dog = GameObject.Find ("Dog");
	}


	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Cat") {
			dog.SetActive (false);
			AudioSource s = GameObject.Find ("Applause").GetComponent<AudioSource> ();
			s.Play();
			GameObject go = Instantiate (pop, dog.transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			GameObject go2 = Instantiate (endGame, coll.gameObject.transform.position,Quaternion.Euler(0, 0, 0)) as GameObject;
			StartCoroutine (Die ());
		}
	}
	IEnumerator Die(){
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene ("TitleScreen");

	}
}
