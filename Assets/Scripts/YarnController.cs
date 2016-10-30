using UnityEngine;
using System.Collections;

public class YarnController : MonoBehaviour {

	public GameObject yarnPrefab;
	private Vector3 target;
	public PlayerMovement S;

	void Start(){
		S = GameObject.Find ("Cat").GetComponent<PlayerMovement> ();
	}


	void Update () {
		target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		target.y = 3.0f;
		GameObject go;
		go = GameObject.Find ("Yarn(Clone)");
		if (Input.GetMouseButtonDown (0) && S.yarnDown == false && S.canDropNip == true) {
			go = Instantiate(yarnPrefab, target, Quaternion.Euler(0, 0, 0)) as GameObject;
		}
		if (Input.GetMouseButton (0) && S.yarnDown == false && S.canDropNip == true) {
			if (go != null) {
				go.transform.position = target;
			}
		}
		if (Input.GetMouseButtonUp (0) && go != null) {
			S.yarnDown = true;
		}
	}
}
