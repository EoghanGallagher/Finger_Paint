using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	[SerializeField] private int score;
	// Use this for initialization

	[SerializeField] GameObject canvas;

	[SerializeField] private bool isLevelOver;
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
		//canvas = GameObject.Find( "YouWon" );
		canvas.SetActive( false );
		
		StartCoroutine( "RunGameLoop" );
	}

	IEnumerator RunGameLoop()
	{
		Debug.Log( "RunGameLoop" );
		//yield return StartCoroutine( "StartSplashRoutine" );
		//yield return StartCoroutine( "StartLevelRoutine" );
		yield return StartCoroutine( "PlayLevelRoutine" );
		yield return StartCoroutine( "EndLevelRoutine" );
		yield return null;
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
		//Show start screen

		//User presses button to start
		//HasLevelStarted = true;
			
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

		while( !isLevelOver )
		{
			yield return null;
		}

		Debug.Log( "Level is over ...well done ...." );
		isLevelOver = false;
		Time.timeScale = 0;
		canvas.SetActive( true );
	}

	


	public void StartGame()
	{
		SceneManager.LoadScene( "Trail_Making_Test_Part_A" );
	}


	public void UpdateScore()
	{
		score++;

		Debug.Log( "Current Score : " + score );

		if( score == 25 )
		{
			Debug.Log("Yay Player completed the level. Trigger end of level sequence....");
			isLevelOver = true;
			score = 0;
		}
	}
	

}
