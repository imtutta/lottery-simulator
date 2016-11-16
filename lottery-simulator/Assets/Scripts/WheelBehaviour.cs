using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WheelBehaviour : MonoBehaviour {

	#region Constants
	const int TEXTNUM = 20;
	const int LEADING_ZERO = 4;
	#endregion

	#region Public Fields
	public int highestNum;
	public GameObject textNumberSource;
	#endregion

	#region Protected Fields
	protected float angularStep = 0.0f;
	protected List<GameObject> textNumbers;
	protected float cSpeed = 0f;
	protected float tSpeed = 0f;
	protected bool moving = false;
	protected bool slowingDown = false;
	#endregion

	#region Public functions
	public void SetNumbers(int selected)
	{
		int low = -Mathf.CeilToInt(TEXTNUM * 0.5f);
		int high = Mathf.FloorToInt(TEXTNUM * 0.5f);
		for (int i = low, j = 0; i < high; ++i, ++j) {
			int value = selected + i;
			if (value < 1)
				value = highestNum + i + 1;
			textNumbers [j].GetComponent<TextMesh> ().text = FormatLeadingZero (value, LEADING_ZERO);
		}
			
	}
	#endregion

	#region Unity Callbacks
	void Start ()
	{
		angularStep = 2 * Mathf.PI / (float)TEXTNUM;
		textNumbers = new List<GameObject> ();
		for (int i = 0; i < TEXTNUM; ++i) {			
			var go = GameObject.Instantiate (textNumberSource);
			go.name = "TextNumber_" + i;
			go.transform.position = ComputePosOnCircle (transform.position, transform.localScale.x * 0.5f, i);
			Vector3 forward = transform.position - go.transform.position;
			Vector3 up = Vector3.Cross (forward, -transform.up); 
			go.transform.LookAt (transform, up);
			go.transform.parent = gameObject.transform;
			textNumbers.Add (go);
		}

		SetNumbers (1);
	}

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Space) && !moving) {
			tSpeed = -360f;
			moving = true;
		}

		if (moving && Mathf.Abs (tSpeed - cSpeed) < 10) {
			if (slowingDown) {
				cSpeed = 0f;
				slowingDown = false;
				moving = false;
			} else {
				tSpeed = 0f;
				slowingDown = true;
			}
		}
		
		cSpeed = Mathf.Lerp (cSpeed, tSpeed, Time.fixedDeltaTime);
		transform.RotateAround (transform.position, transform.up, Time.fixedDeltaTime * cSpeed);
	
	}
	#endregion

	#region Functions
	protected Vector3 ComputePosOnCircle(Vector3 center, float radius, int slice) {
		float angle = angularStep * slice;
		float yPos = radius * Mathf.Sin (angle);
		float zPos = radius * Mathf.Cos (angle);
		return new Vector3 (0.0f, yPos, zPos);
	}

	protected string FormatLeadingZero(int num, int strLenght) {
		int leadingZeros = strLenght - 1;
		float n = (float)num;
		while (n * 0.1f > 1) {
			leadingZeros--;
			n *= 0.1f;
		}

		string str = "";
		for (int i = 0; i < leadingZeros; ++i)
			str += "0";
		return str + num.ToString ();
	}
	#endregion
}
