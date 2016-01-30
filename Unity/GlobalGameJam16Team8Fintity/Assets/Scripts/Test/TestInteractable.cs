using UnityEngine;
using System.Collections;
using System;

public class TestInteractable : MonoBehaviour, IInteract {
    public Material material;
    public String woord;
    //public SoundManager soundManager;
    //public AudioClip audioClip;

    public void interact()
    {
        DaySequence.NextSequence(woord);

        /*if (audioClip != null)
        {
            soundManager.PlaySingle(audioClip);
        }*/
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
