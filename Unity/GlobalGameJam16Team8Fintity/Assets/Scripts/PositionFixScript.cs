using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson; 

public class PositionFixScript : MonoBehaviour {

	public FirstPersonController charscr;
	public Vector3 pos;
	public Vector3 RotVector;
	private Quaternion rot;
	public bool trigger;
	public Animator anim;
	public GameObject parent;
	public Transform setTrans;
	// Use this for initialization

	void Awake()
	{
		charscr.enabled = false;
	}
	void Start () {
	
		parent = transform.parent.gameObject;
		anim = parent.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		if (charscr.isActiveAndEnabled && trigger)
		{
			transform.parent = null;
			
			trigger = false;
		}
	}




	
	



}
