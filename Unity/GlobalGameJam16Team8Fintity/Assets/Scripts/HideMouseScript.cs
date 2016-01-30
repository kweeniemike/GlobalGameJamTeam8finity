using UnityEngine;
using System.Collections;

public class HideMouseScript : MonoBehaviour {

	public CursorLockMode wantedMode;
	private bool onPause;

	void Start()
	{
		wantedMode = CursorLockMode.Locked;
		Cursor.visible = false;
	}


	void CursorStateHandler()
	{
		/*if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!onPause)
			{
				Cursor.visible = true;
				wantedMode = CursorLockMode.Confined;
				onPause = true;
			} else {
				Cursor.visible = false;
				wantedMode = CursorLockMode.Locked;
				onPause = false;

			}
		}*/
		Cursor.lockState = wantedMode;
	}

	void Update()
	{
		CursorStateHandler();
	}

}
