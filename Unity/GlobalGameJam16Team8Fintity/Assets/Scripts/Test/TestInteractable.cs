using UnityEngine;
using System.Collections;
using System;

public class TestInteractable : MonoBehaviour, IInteract {
    public Material material;
    public String woord;

    public void interact()
    {
        DaySequence.NextSequence(woord);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
