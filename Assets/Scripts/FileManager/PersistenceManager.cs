using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using GameSessions;
using Newtonsoft.Json;


public class PersistenceManager : MonoBehaviour 
{
	public static PersistenceManager Instance;

	private string fileName;
	public string FileName { get{ return fileName; } set{ fileName = value; Debug.Log( fileName ); } }

	void Awake()
	{
		Environment.SetEnvironmentVariable( "MONO_REFLECTION_SERIALIZER", "yes" );
		Instance = this;
	}

	void Start()
	{
		
		fileName = "sessionInfo.dat";

		Debug.Log( fileName );
		
		System.Object obj = Load( fileName );
		
		string jsonString = JsonConvert.SerializeObject( obj );

		Debug.Log( jsonString );

		// foreach( Transition t in obj.transistions )
		// {
		// 	Debug.Log( t.TransistionName );
		// 	Debug.Log( t.TransitionTime );
		// }
	}

	public void Save( System.Object objectToSave ) 
	{
    	Debug.Log("Saving " +  GetPath() + fileName );
		
		BinaryFormatter formatter = new BinaryFormatter();
    	FileStream file = File.Open(  GetPath() + fileName, FileMode.OpenOrCreate );
    	formatter.Serialize( file, objectToSave );
    	file.Close();
	}

	public System.Object Load( string nameOfFile ) 
	{
    	var serializedObject = new System.Object();

		Debug.Log( GetPath() + nameOfFile );
	
    	if( File.Exists( GetPath()  + nameOfFile ) ) 
		{
        	BinaryFormatter formatter = new BinaryFormatter();
        	FileStream file = File.Open( GetPath() + nameOfFile, FileMode.Open );
        	serializedObject = formatter.Deserialize( file );
        	file.Close();
    	}
		else
		{
			Debug.Log( "File not Found..." );
		}
    	return serializedObject;
	}


	//Return a valid filepath for various devices...
	private static string GetPath()
	{
	
		#if UNITY_EDITOR
			return Application.dataPath + "/";
 		#elif UNITY_ANDROID
			return Application.persistentDataPath;
		#elif UNITY_IPHONE
			return Application.persistentDataPath + "/";
		#else
			return Application.dataPath + "/";
		#endif
	}

	
	

	
	
}
