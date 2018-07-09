using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SessionManager;
using Newtonsoft.Json;

public class BeginButton : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Session sess = new Session();

		sess.SessionName = "First Session";

		for( int i = 0; i < 10; i++ )
		{
			Transition t = new Transition();
			t.TransitionNo = i;
			t.TransistionName = "tr " + i;

			sess.transistions.Add( t );
		}


		for( int i = 0; i < 10; i++ )
		{
			Debug.Log( sess.transistions[i].TransistionName );
		}

		var jsonString = JsonConvert.SerializeObject( sess );

		Debug.Log( jsonString );


		
	}
	
	
}
