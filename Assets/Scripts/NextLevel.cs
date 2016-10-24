using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	public string nextLevel;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Cat") {
			SceneManager.LoadScene (nextLevel);
		}
	}

}
