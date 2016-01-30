using UnityEngine;
using System.Collections;
using System;

public class DayNightTransition : MonoBehaviour {
    public Light directionalLight;
    public float speed = 1f;
    public bool transistionHappened = false;
    private bool transistionGoing = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DayToNight()
    {
        if(!transistionHappened)
        {
            StartCoroutine(DayToNightTransition());
        }
    }

    public IEnumerator DayToNightTransition()
    {
        transistionHappened = true;
        transistionGoing = true;
        Quaternion up = Quaternion.LookRotation(Vector3.up, Vector3.forward);
        while (transistionGoing)
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0f, Time.deltaTime * 0.001f * speed);
            directionalLight.transform.rotation = Quaternion.Lerp(directionalLight.transform.rotation, up, Time.deltaTime * speed);
            if(directionalLight.intensity <= 0.1f)
            { 
                directionalLight.enabled = false;
                transistionGoing = false;
            }
            yield return null;
        }
    }
}
