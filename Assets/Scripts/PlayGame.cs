using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour {
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.white;
	}

	// Update is called once per frame
	void OnMouseEnter() {
		GetComponent<Renderer>().material.color = Color.black;
	}

	void OnMouseExit() {
		GetComponent<Renderer>().material.color = Color.white;
	}

	void OnMouseUp()
	{
		SceneManager.LoadScene ("Level_1");
	}
}
