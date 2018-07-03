using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;


public class FileManager : MonoBehaviour 
{

	public static bool isTextAsset;

	void OnEnable()
	{

	}

	void OnDisable()
	{

	}

	void Start()
	{
		LoadFile( GetPath() );
	}

	public void SaveFile()
	{
		Debug.Log( "Writing file to... " + GetPath() );
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter( GetPath(), true );
    
		try
		{
			writer.WriteLine( "1,0,0,0,0,0,0,0,2,0,0,0,3" );
       
		}
		catch( Exception e )
		{
			Debug.LogException( e );
		}
		finally
		{
			writer.Close();
		}

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path); 
        //TextAsset asset = Resources.Load("test");

        //Print the text from the file
        //Debug.Log(asset.text);
	}

	public static void LoadFile( string fileName )
	{ 
		string line = "";

		try
		{
			 //Create a new StreamReader, tell it which file to read
			 //Set the encoding in this case default.
			 StreamReader reader = new StreamReader( fileName, Encoding.Default );

			 using( reader )
			 {
				 do
				 {
					 line = reader.ReadLine();
				 }
				 while( line != null );
			 };
		}
		catch( Exception e )
		{

		}
		finally
		{

		}
	}


	//Return a valid filepath for various devices...
	private static string GetPath()
	{
		
		string fileName = "test.csv";

		if( isTextAsset )
		{
			return "Assets/Resources/test.txt";
		}
		
		#if UNITY_EDITOR
			return Application.dataPath + "/CSV/" + fileName;
 		#elif UNITY_ANDROID
			return Application.persistentDataPath + fileName;
		#elif UNITY_IPHONE
			return Application.persistentDataPath + "/" + fileName;
		#else
			return Application.dataPath + "/" + fileName;
		#endif
	}
	
	
}
