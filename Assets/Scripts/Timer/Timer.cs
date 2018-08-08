using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour 
{
	
	[SerializeField] private bool isTimerRunning = false;
	[SerializeField] private float startTime;

	[SerializeField] private string timeTaken;
	[SerializeField] private string timeTakenSeconds;

	[SerializeField] private TextMeshProUGUI timerText;

	public string TimeTaken { get { return timeTaken; } }
	public string TimeTakenSeconds{ get { return timeTakenSeconds; } }


	private float timeTakenfloat;
	public float TimeTakenfloat{ get { return timeTakenfloat; } }

	private float t;

	private SessionManager sessionManager;

	
	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		GameObject gameManager = GameObject.Find( "GameManager" );

		if( gameManager != null )
			sessionManager = gameManager.GetComponent<SessionManager>();

		timerText = GetComponent<TextMeshProUGUI>();	
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if( isTimerRunning )
		{
			timeTakenfloat = t = Time.time - startTime;

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

			timerText.text = minutes + ":" + seconds;

			
			timeTaken = minutes + ":" + seconds;


			timeTakenSeconds = minutes + ":" + ( t % 60 ).ToString( "F2" );

			

			

			

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
