using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	
	
	
	// Use this for initialization
	void Start () {
		//timerText = GetComponent<GUIText>();
		//if( timerText != null )
		//	Debug.Log( "works ok" );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartTimer()
	{
		// Debug.Log( "Starting Timer" );
		// timerText = GetComponent<TextMesh>();
		// if( timerText != null )
		// 	Debug.Log( "works ok" );
	}

	public void StopTimer()
	{
		Debug.Log( "Stopping Timer" );
	}
}
