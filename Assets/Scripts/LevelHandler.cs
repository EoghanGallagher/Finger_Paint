using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Resonsible for managing the trail maker tests 
//As of this build there are 4 test or levels
//A level is a prefab that contains the assets and layout for a Trailmaker test
public class LevelHandler : MonoBehaviour 
{
	[SerializeField] private  GameObject[] levelList; //List of levels
	[SerializeField] private GameObject[] demoList; //List of demo levels
	[SerializeField] int currentLevel = 0;
	[SerializeField] bool demoMode;


	void Start()
	{
		PlayerPrefs.DeleteAll();
	
		//When the level is loaded
		//Check what value the current level has 0 - 3 
		if( PlayerPrefs.HasKey( "CurrentLevel" ) )
		{
			currentLevel = PlayerPrefs.GetInt( "CurrentLevel" );
			Debug.Log( currentLevel );
		}

		//Activate the current level
		SetActiveLevel();
		
		    
	}


	//Load the scene level
	public void LoadLevel()
	{
		SceneManager.LoadSceneAsync( "Star_Racer_Main" );
	}



	//Deactivate all levels
	//Activate the current level
	private void SetActiveLevel()
	{
		//Disable all level prefabs contained in the list
		foreach( GameObject level in levelList )
		{
			level.SetActive( false );
		}

		if( !demoMode )
			//Enable the currently selected level
			levelList[ currentLevel ].SetActive( true );
		else
			demoList[0].SetActive( true );
	}

	//Called by the next level buttons On click event 
	public void SetNextLevel(  )
	{
		if( currentLevel < levelList.Length -1  )
			currentLevel ++;
		else
			currentLevel = 0;


		PlayerPrefs.SetInt( "CurrentLevel", currentLevel );		

		
	}


	
}
