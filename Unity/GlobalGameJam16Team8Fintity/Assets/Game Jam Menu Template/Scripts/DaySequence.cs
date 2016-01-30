using UnityEngine;
using System.Collections;

public class DaySequence : MonoBehaviour {

	

	private enum Sequences { Opstaan, TandenPoetsen, Eten, TvKijken, Slapen};
	[SerializeField] static private Sequences sequences;// {get; set;}

	static private int currentSequence;
	static public int CurrentSequence
	{get; private set;}

	public string SequencePhase;

	void awake()
	{
		currentSequence = 0;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if( Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log (sequences.ToString());
			NextSequence(SequencePhase);
			Debug.Log (sequences.ToString());
			
		}

		currentSequence = CurrentSequence;
		sequences = (Sequences)CurrentSequence;

	
	}


	static public void NextSequence(string tag)
	{
		if ( tag == "Slapen" &&  sequences.ToString() == "slapen")
		{
			int levelToLoad = Application.loadedLevel + 1;
			Application.LoadLevel(levelToLoad);
		}
		if ( tag == sequences.ToString())
		{
		CurrentSequence += 1;
		sequences = (Sequences)CurrentSequence;
		}

	}
}
