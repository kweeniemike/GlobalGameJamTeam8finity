using UnityEngine;
using System.Collections;
using System;

public class TestInteractable : MonoBehaviour, IInteract {
    public Material material;

    public void interact()
    {
        Debug.Log("Stuff");
        if (material.color == Color.blue)
        {
            material.SetColor("_Color", Color.red);
        }
        else
        {
            material.SetColor("_Color", Color.blue);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
