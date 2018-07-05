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
	
	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float t = Time.time - startTime;

		string minutes = ( (int) t / 60 ).ToString(  ) ;
		string seconds = ( t % 60 ).ToString( "F2" );

		timerText.text = minutes + ":" + seconds;
	}

	public void StartTimer()
	{
		isTimerRunning = true;
	}

	public void StopTimer()
	{
		isTimerRunning  = false;
	}
}
