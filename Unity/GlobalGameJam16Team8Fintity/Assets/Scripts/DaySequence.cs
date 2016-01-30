using UnityEngine;
using System.Collections;

public class DaySequence : MonoBehaviour {

	private enum Sequences { Opstaan, Eten, TandenPoetsen, TvKijken, Slapen};
	[SerializeField] static private Sequences sequences;// {get; set;}

	static private int currentSequence;
	static public int CurrentSequence
	{get; private set;}

	public string SequencePhase;
    public static Light directionLight; // you are here
	void awake()
	{
		currentSequence = 0;
        directionLight = GetComponent<Light>();
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
        if (tag == "Slapen" && sequences.ToString() == "Slapen")
        {
            int levelToLoad = Application.loadedLevel + 1;
            Application.LoadLevel(levelToLoad);
        }
        else if(tag == "TvKijken" && sequences.ToString() == "TvKijken")
        {
            //directionLight.GetComponent<DayNightTransition>().DayToNight();
        }
        if (tag == sequences.ToString())
        {
            Debug.Log(tag);
            CurrentSequence += 1;
            sequences = (Sequences)CurrentSequence;
        }

    }
}
