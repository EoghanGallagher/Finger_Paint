using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; //Json Library
using System.IO;


public class FileUploadHandler : MonoBehaviour 
{

	string path = "";
	string destinationPath;

	void Start()
	{
		
		path = GetPath() + "upload/";

		destinationPath = GetPath() + "sent/";
		Debug.Log( path );
			
	}

	//Upload Current Session File to Server
	public void UploadFile()
	{
		
		//Search  directory for files
		DirectoryInfo dir = new DirectoryInfo( path );
		FileInfo[] info = dir.GetFiles( "*.dat" );

		//Find all .dat files in the upload directory
		foreach( FileInfo f in info )
		{

			Debug.Log( f.Name );
			//Load the file 
			System.Object obj = PersistenceManager.Instance.Load( f.Name );
			
			//Convert the file to JSON
			string jsonString = JsonConvert.SerializeObject( obj );

			//Display the file
			Debug.Log( jsonString );

			//Upload the file

			//TODO

			//Move Uploaded Files to sent directory
			//Only if they were successfully uploaded..
			File.Move( path + f.Name, destinationPath + f.Name );
		}		


		
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
