using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSessions;
using Newtonsoft.Json; //JSON NET Plugin

public class SessionManager : MonoBehaviour 
{

	[SerializeField] private string deviceName;
	[SerializeField] private string deviceModel;
	[SerializeField] private string deviceType;
	[SerializeField] private string deviceUniqueIdentifier;

	private Session session;

	[SerializeField] private string sessionDuration;
	public string SessionDuration { get{ return sessionDuration; } set{ sessionDuration = value; }  }

	[SerializeField] private string transitionDuration;
	public string TransitionDuration { get{ return transitionDuration; } set{ transitionDuration = value; }  }
	
	void Start()
	{
		deviceName = SystemInfo.deviceName;
		deviceModel = SystemInfo.deviceModel;
		deviceType = SystemInfo.deviceType.ToString();
		deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
	}
	
	// Use this for initialization
	//Create 


	//Create a new Session
	//
	public void CreateSession()
	{
	
		session =  new Session();

		session.DeviceType = deviceType;
		session.DeviceModel = deviceModel;
		session.DeviceModel = deviceModel;
		session.DeviceUniqueIdentifier = deviceUniqueIdentifier;
		session.GameID = "0001";
		session.PlayerID = "player00001";
		session.TimeStamp = System.DateTime.Now.ToString();
		session.SessionDuration = sessionDuration;
		session.SessionName = "Trail Maker";
		session.SessionNumber = 22;

		//PersistenceManager.Instance.FileName = "sessionInfo.dat";
	
	}

	public void AddTransition( Transition t )
	{
		if( t != null )
		{
			session.TransitionCount ++;
			t.TransitionNo = session.TransitionCount;
			t.TransistionName = t.Source + "-" + t.Destination;
			session.transistions.Add( t );
			//Debug.Log( session.transistions.Count );

		//	PersistenceManager.Instance.Save( session );
			
			

		}
		else
		{
			Debug.Log( "Transition not set !" );
		}

		// foreach( Transition tr in session.transistions )
		// {
		// 	Debug.Log( "Source: " + tr.Source );
		// 	Debug.Log( "Destination: " + tr.Destination );
		// 	Debug.Log( "Duration: " + tr.TransitionTime );
		// }
	}

	public void EndSession()
	{
		
		session.SessionDuration = sessionDuration;
		
		//PersistenceManager.Instance.Save( session );

		string jsonString = JsonConvert.SerializeObject( session );

		Debug.Log( jsonString );

		//SessionToJson();
	}


	//Check if the session was completed successfully
	//Paramater : bool 
	//Return : nothing
	//Triggered by end level event
	public void SessionCompleted( bool b )
	{
		session.SessionCompleted = b;
	}


	

	
}
