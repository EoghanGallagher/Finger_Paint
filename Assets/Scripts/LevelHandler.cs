using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour 
{

	public void LoadLevel()
	{
		SceneManager.LoadSceneAsync( "Trail_Making_Test_Part_A" );
	}
	
}
