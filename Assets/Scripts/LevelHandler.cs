using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour 
{
	[SerializeField] private  GameObject[] levelList;
	[SerializeField] int currentLevel = 0;


	void Start()
	{
		//PlayerPrefs.DeleteAll();
	
		if( PlayerPrefs.HasKey( "CurrentLevel" ) )
		{
			currentLevel = PlayerPrefs.GetInt( "CurrentLevel" );
			Debug.Log( currentLevel );
		}

		SetActiveLevel();
	}


	public void LoadLevel()
	{
		SceneManager.LoadSceneAsync( "Trail_Making_Test_Part_A" );
	}

	private void SetActiveLevel()
	{
		foreach( GameObject level in levelList )
		{
			level.SetActive( false );
		}

		levelList[ currentLevel ].SetActive( true );
	}


	public void SetNextLevel(  )
	{
		
		if( currentLevel < levelList.Length -1  )
			currentLevel ++;
		else
			currentLevel = 0;


		PlayerPrefs.SetInt( "CurrentLevel", currentLevel );		

		
	}


	
}
