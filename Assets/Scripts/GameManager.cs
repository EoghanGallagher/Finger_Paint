﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
//using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour 
{

	//[SerializeField] private PostProcessingBehaviour blur;
	//[SerializeField] private PostProcessingBehaviour normal;
	[SerializeField] private int score;
	// Use this for initialization

	[SerializeField] private int scoreLimit = 25;
	[SerializeField] private int tutScoreLimit = 8;
	[SerializeField] GameObject canvas;
	[SerializeField] private bool isLevelOver;
	[SerializeField] private bool istutorial;
	public static GameManager Instance = null;


	[SerializeField] private bool isDemoMode;

	public bool IsDemoMode { get { return isDemoMode; } }


	  // has the user pressed start?
    bool m_hasLevelStarted = false;
    public bool HasLevelStarted { get { return m_hasLevelStarted; } set { m_hasLevelStarted = value; } }

 // have we begun gamePlay?
    bool m_isGamePlaying = false;
    public bool IsGamePlaying { get { return m_isGamePlaying; } set { m_isGamePlaying = value; } }

    // have we met the game over condition?
    bool m_isGameOver = false;
    public bool IsGameOver { get { return m_isGameOver; } set { m_isGameOver = value; } }

    // have the end level graphics finished playing?
    bool m_hasLevelFinished = false;
    public bool HasLevelFinished { get { return m_hasLevelFinished; } set { m_hasLevelFinished = value; } }

	//Unity Events
	public UnityEvent setupEvent;
	public UnityEvent startLevelEvent;
	public UnityEvent playLevelEvent;
	public UnityEvent endLevelEvent;


	void OnEnable()
	{
		Messenger<int>.AddListener( "ScoreLimit" , SetScoreLimit );
	}

	void OnDisable()
	{
		Messenger<int>.RemoveListener( "ScoreLimit" , SetScoreLimit );
	}

	void Awake()
	{
		if( Instance == null )
		{
			Instance = this;
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
		}

		//DontDestroyOnLoad( gameObject );
	}

	void Start () 
	{
		//canvas = GameObject.Find( "YouWon" );
		//canvas.SetActive( false );

		if( istutorial )
		{
			scoreLimit = tutScoreLimit;
		}
		
		StartCoroutine( "RunGameLoop" );
	}


	//Start the Game Loop
	IEnumerator RunGameLoop()
	{
		
		//Handles level setup
		yield return StartCoroutine("StartLevelRoutine");
		yield return StartCoroutine("PlayLevelRoutine");
		yield return StartCoroutine("EndLevelRoutine");
			
	}

	IEnumerator StartSplashRoutine()
	{
	
		yield return new WaitForSeconds( 1.0f );
		
		yield return new WaitForSeconds( 2.0f );

		SceneManager.LoadSceneAsync( "TrailMakingIntro" );

	}


	//Game Setup 
	IEnumerator StartLevelRoutine()
	{
		if (setupEvent != null)
        {
            setupEvent.Invoke();
        }

		while ( !m_hasLevelStarted )
        {
            //show start screen
            // user presses button to start
            // HasLevelStarted = true
            yield return null;
        }

		  // trigger events when we press the StartButton
        if (startLevelEvent != null)
        {
            startLevelEvent.Invoke();
        }
		
	}

	//The Actuall Game loop
	IEnumerator PlayLevelRoutine()
	{
		if (playLevelEvent != null)
        {
            playLevelEvent.Invoke();
        }
		
		while (!m_isGameOver)
        {
            // pause one frame
            yield return null;

            // check for level win condition
            m_isGameOver = IsWinner();

            // check for the lose condition
        }
	}

	//Game Clean Up
	IEnumerator EndLevelRoutine()
	{
		
		yield return new WaitForSeconds( 0.6f );
		// run events when we end the level
        if (endLevelEvent != null)
        {
            endLevelEvent.Invoke();
        }

        // show end screen
        while (!m_hasLevelFinished)
        {
            // user presses button to continue

            // HasLevelFinished = true
            yield return null;
        }
	}

	public void StartGame()
	{
		SceneManager.LoadScene( "Trail_Making_Test_Part_A" );
	}

	//Keep Track of player score
	public void UpdateScore()
	{
		score++;
	}

	bool IsWinner()
	{
		if( score == scoreLimit )
		{
			return true;
		}
		else
		{
			return false;
		}
	}


	// attach to StartButton, triggers PlayLevelRoutine
	public void PlayLevel()
	{
		m_hasLevelStarted = true;
	}

	//Pause Game
	public void Pause()
	{
		Time.timeScale = 0;
	}

	//Resume Game
	public void Resume()
	{
		Time.timeScale = 1;
	}


	public void SetScoreLimit( int limit )
	{
		scoreLimit = limit;
	}
	

}
