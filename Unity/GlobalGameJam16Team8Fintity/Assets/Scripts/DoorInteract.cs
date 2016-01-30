using UnityEngine;
using System.Collections;

public class DoorInteract : MonoBehaviour, IInteract {


	[SerializeField]private GameObject Door;
	[SerializeField]private Rigidbody DoorRb;
	private Transform closeTransform;
	[SerializeField]private bool isOpen;
	private Quaternion StartRot;
	[SerializeField]private GameObject Player;
	bool openIsRunning;
	private int pickfix;

	// Use this for initialization
	void Start () {
		openIsRunning = false;
		GetDoor();
		StartRot = transform.rotation;
		closeTransform = this.transform;
		isOpen = false;
		DoorRb = Door.GetComponent<Rigidbody>();
		DoorRb.isKinematic = true;
		Player = GameObject.FindGameObjectWithTag("Player") as GameObject;


	}

	void GetDoor()
	{
		GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
		foreach (GameObject d in doors)
		{
			if (d.transform.parent == this.transform)
			{Door = d;}
		}
	}


	public void interact()
	{	
		float xfix = Player.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x ;
		int xfixfactor = xfix > 0 ? 1 : -1 ;
		float zfix = Player.GetComponent<Transform>().position.z - GetComponent<Transform>().position.z ;
		int zfixfactor = zfix > 0 ? 1 : -1 ;

		float xCheck = Mathf.Abs(Door.transform.position.x - transform.position.x);
		float zCheck = Mathf.Abs(Door.transform.position.z - transform.position.z);
		pickfix = xCheck < zCheck
			 ? xfixfactor : zfixfactor;
		if (!isOpen)
		{
			if (!openIsRunning)
			{
			StartCoroutine(OpenDoor());
				openIsRunning = true;
			}

		} else {

			StartCoroutine(CloseDoor());
		}
	}

	IEnumerator OpenDoor()
	{

	

		Vector3 rot = StartRot.eulerAngles + new Vector3(0, 90 *pickfix ,0);
		Quaternion rotation = Quaternion.Euler(rot) ;

		                   BoxCollider col = Door.GetComponent<BoxCollider>();
		                   col.isTrigger = true;

		                   

		while (transform.rotation !=  rotation && !isOpen)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 4 * Time.deltaTime);
			yield return null;
		}
		if (transform.rotation == rotation)
		{
			col.isTrigger = false;

		isOpen = true;
			openIsRunning =false;

		}
	}

	IEnumerator CloseDoor()
	{
		BoxCollider col = Door.GetComponent<BoxCollider>();
			col.isTrigger = true;
		while ( transform.rotation != StartRot && isOpen)
		{
		
		Quaternion rotation = transform.rotation;
		transform.position = closeTransform.position;
		transform.rotation = Quaternion.Slerp(rotation, StartRot,4 * Time.deltaTime);
			
		yield return null;
		}

		if (transform.rotation == StartRot)
			{
				col.isTrigger = false;

		isOpen = false;
			}
	}
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown("Fire1"))
		{
			interact();
	
		}

	}
}
