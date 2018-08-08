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

	public string TimeTaken { get { return timeTaken; } }

	private float t;

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
			t = Time.time - startTime;

			string minutes = "";
			string seconds = "";
			
			if( ((int)t / 60 ) < 10 )
			{
				minutes = "0" + ( (int) t / 60 ).ToString(  ) ;
			}
			else
			{
				minutes = ( (int) t / 60 ).ToString( "F2" ) ;
			}


			if( ( t % 60 ) < 10 )
			{
				seconds = "0" + ( (int) t % 60 ).ToString(  );
			}
			else
			{
				seconds = ( (int)t % 60 ).ToString(  );
			}

			//timerText.text = minutes + ":" + seconds;

			
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
