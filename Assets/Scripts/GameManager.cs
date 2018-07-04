using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

	// Use this for initialization
	public static GameManager instance = null;
	void Awake()
	{
		if( instance == null )
		{
			instance = this;
		}
		else if( instance != this )
		{
			Destroy( gameObject );
		}

		DontDestroyOnLoad( gameObject );
	}
	void Start () 
	{
		StartCoroutine( "RunGameLoop" );
	}

	IEnumerator RunGameLoop()
	{
		Debug.Log( "RunGameLoop" );
		yield return StartCoroutine( "StartSplashRoutine" );
		yield return StartCoroutine( "StartLevelRoutine" );
		yield return StartCoroutine( "PlayLevelRoutine" );
		yield return StartCoroutine( "EndLevelRoutine" );
	}

	IEnumerator StartSplashRoutine()
	{
		Debug.Log( "Logo fading in" );
		yield return new WaitForSeconds( 1.0f );
		Debug.Log( "Waiting for 2 seconds before" );
		yield return new WaitForSeconds( 2.0f );

		SceneManager.LoadSceneAsync( "TrailMakingIntro" );


	}

	IEnumerator StartLevelRoutine()
	{
		Debug.Log( "Start Level Routine..." );
		yield return null;
	}

	IEnumerator PlayLevelRoutine()
	{
		Debug.Log( "Play Level Routine..." );
		yield return null;
	}

	IEnumerator EndLevelRoutine()
	{
		Debug.Log( "End Level Routine..." );
		yield return null;
	}


	public void StartGame()
	{
		SceneManager.LoadSceneAsync( "Trail_Making_Test_Part_A" );
	}
	

}
