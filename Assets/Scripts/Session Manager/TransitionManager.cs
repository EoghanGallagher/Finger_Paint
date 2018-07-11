using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSessions;

public class TransitionManager : MonoBehaviour 
{

	private Transition transition;
	private float startTime;
	private string minutes;
	private string seconds;

	private bool isTimerRunning = false;

	
	[SerializeField] private bool isTransitionOpen = false; 
	public bool IsTransitionOpen { get{ return isTransitionOpen; } set{ isTransitionOpen = value; } }

	public string TransitionSource { set{ transition.Source = value; } get{ return transition.Source; }  }
	public string TransitionDestination { set{ transition.Destination = value; } get{ return transition.Destination; }  }

	private void Start()
	{
		//startTime = Time.time;
	}


	void Update()
	{
		if( isTimerRunning )
		{
			float t = Time.time - startTime;

			 minutes = ( (int) t / 60 ).ToString(  ) ;
			 seconds = ( t % 60 ).ToString( "F2" );

		}
	}


	
	public void CreateTransition()
	{
		//Debug.Log( "Creating Transition" );
		transition = new Transition();
		isTransitionOpen = true;
		isTimerRunning = true;
		transition.TransitionComplete = false;

		SetTimer();

	}

	public Transition EndTransition()
	{
		//Debug.Log( "Ending Transition" );
		
		if( transition != null )
			isTransitionOpen = false;
		
		isTimerRunning = false;

		transition.TransitionComplete = true;

		SetTransitionDuration();

		return transition;	
	}

	private void SetTimer()
	{
		startTime = Time.time;
	}

	private void SetTransitionDuration()
	{
		transition.TransitionTime = minutes + ":" + seconds;
	}

	public void AddError(  Error err  )
	{
		transition.transitionErrors.Add( err );
	}


	
}
