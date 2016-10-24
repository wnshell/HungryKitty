using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pit : MonoBehaviour {

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Cat") {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}
}
