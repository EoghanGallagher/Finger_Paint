using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour 
{
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private bool isTimerRunning = false;
	[SerializeField] private float startTime;

	[SerializeField] private string timeTaken;

	private SessionManager sessionManager;
	
	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		GameObject gameManager = GameObject.Find( "GameManager" );

		if( gameManager != null )
			sessionManager = gameManager.GetComponent<SessionManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if( isTimerRunning )
		{
			float t = Time.time - startTime;

			string minutes = ( (int) t / 60 ).ToString(  ) ;
			string seconds = ( t % 60 ).ToString( "F2" );

			timerText.text = minutes + ":" + seconds;
		
			timeTaken = minutes + ":" + seconds;

		}

		
	}

	//Triggered when game starts
	public void StartTimer() 
	{
		isTimerRunning = true;
	}

	//Triggered when player completes a level
	public void StopTimer()
	{
		Debug.Log( "Time Taken : " +  timeTaken );
		sessionManager.SessionDuration = timeTaken;
		Debug.Log( "Time Taken  Session: " +  sessionManager.SessionDuration );
		isTimerRunning  = false;
		

	}
}
