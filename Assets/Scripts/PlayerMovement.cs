using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum moveState{
	STOPPED,
	MOVING,
	LEAPING, 
	LOCKED
}

public class PlayerMovement : MonoBehaviour {

	public bool canDropNip;
	public moveState ms;
	public float speed;
	public float minspeed;
	public float maxspeed;
	public float threshold;
	private Vector3 target;
	public float detectionradius;
	public float hopHeight = 1.25f;
	private bool hopping = false;

	private GameObject lockedMouse;

	public Text nipCount;

	public bool catnipDown;
	public bool yarnDown;


	public GameObject catnip;



	void Start () {
		target = transform.position;
		ms = moveState.STOPPED;
		catnipDown = false;
		yarnDown = false;
		canDropNip = false;
		nipCount = GameObject.Find ("NipCount").GetComponent<Text> ();
	}

	void Update () {
		GameObject yarn = GameObject.Find ("Yarn(Clone)");
		//yarn movement
		if (yarn != null) {
			target = yarn.transform.position;
			target.y = 3.0f;
			Vector3 diff = target - transform.position;
			//Rotates toward the mouse
			float mag = diff.magnitude;
			if (mag > 0.0f && ms == moveState.STOPPED) {
				ms = moveState.MOVING;
			}
			if (mag < minspeed && ms == moveState.MOVING) {
				mag = minspeed;
			} else if (mag > maxspeed && ms == moveState.MOVING) {
				mag = maxspeed;
			}
			if (ms == moveState.MOVING && lockedMouse == null) {
				GetComponent<Rigidbody> ().transform.eulerAngles = new Vector3 (0, Mathf.Atan2 ((target.x - transform.position.x), (target.z - transform.position.z)) * Mathf.Rad2Deg, 0);
				transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime * mag);
			}

		}
		//catnip dropping
		int numCatNip = int.Parse(nipCount.text);
		if (Input.GetKeyDown (KeyCode.Space) && !catnipDown && ms != moveState.LOCKED && canDropNip && numCatNip > 0) {
			AudioSource s = GameObject.Find ("purring").GetComponent<AudioSource> ();
			s.Play();
			numCatNip--;
			nipCount.text = numCatNip.ToString ();
			GameObject go;
			Vector3 target2 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			target2.y = 3.0f;
			go = Instantiate (catnip, target2, Quaternion.Euler (-90, 0, 0)) as GameObject;
			catnipDown = true;
		}
		if (ms == moveState.LOCKED && lockedMouse != null) {
			StartCoroutine (lockOnToMouse (lockedMouse.transform.position, lockedMouse));
		} else {
			//catnip or badmouse detection
			Collider[] colliders = Physics.OverlapSphere (transform.position, detectionradius);
			foreach (Collider hit in colliders) {
				Vector3 diffcoll = hit.transform.position - transform.position;
				float magcoll = diffcoll.magnitude;
				if (hit.gameObject.tag == "BadMouse") {
					ms = moveState.LOCKED;
					lockedMouse = hit.gameObject;
					StartCoroutine (lockOnToMouse (lockedMouse.transform.position, lockedMouse));
					break;
				} else if (hit.gameObject.tag == "Catnip" && ms != moveState.LOCKED && magcoll <= 35.0f) {
					ms = moveState.LEAPING;
					StartCoroutine (leapToCatnip (hit.gameObject.transform.position, 0.6f));
					break;
				}
			}
		}
		//Press 'R' to restart the current level.
		if(Input.GetKeyDown (KeyCode.R)){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			SceneManager.LoadScene ("TitleScreen");
		}
		if(Input.GetKeyDown (KeyCode.S)){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}

	IEnumerator leapToCatnip(Vector3 dest, float time){
		MeshRenderer mo = GameObject.Find ("!").GetComponent<MeshRenderer>();
		mo.enabled = true;	
		GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(0,Mathf.Atan2((dest.x - transform.position.x), (dest.z - transform.position.z))*Mathf.Rad2Deg, 0);

		yield return new WaitForSeconds (1);

		if (hopping) yield break;

		hopping = true;
		var startPos = transform.position;
		var timer = 0.0f;
		AudioSource s = GameObject.Find ("Jump").GetComponent<AudioSource> ();
		s.Play();

		while (timer <= 1.0f) {
			var height = Mathf.Sin(Mathf.PI * timer) * hopHeight;
			transform.position = Vector3.Lerp(startPos, dest, timer) + Vector3.up * height; 

			timer += Time.deltaTime / time;
			yield return null;
		}
		hopping = false;
		ms = moveState.STOPPED;
		mo.enabled = false;

	}

	IEnumerator lockOnToMouse (Vector3 dest, GameObject go){
		MeshRenderer mo = GameObject.Find ("!").GetComponent<MeshRenderer>();
		mo.enabled = true;	
		GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(0,Mathf.Atan2((dest.x - transform.position.x), (dest.z - transform.position.z))*Mathf.Rad2Deg, 0);

		yield return new WaitForSeconds (1);
		if (go.activeInHierarchy) {
			transform.position = Vector3.MoveTowards (transform.position, dest, Time.deltaTime * 5);
		} else {
			lockedMouse = null;
			mo.enabled = false;
			yield return new WaitForSeconds (1);
			ms = moveState.STOPPED;
		}
	}
}
