using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; //Json Library
using System.IO;
using UnityEngine.Networking;


public class FileUploadHandler : MonoBehaviour 
{

	private string path = "";
	private string destinationPath;

	private string jsonString;

	private static readonly string POSTSessionURL = "http://localhost:8080/api/session";



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
			jsonString = JsonConvert.SerializeObject( obj );

			//Display the file
			Debug.Log( jsonString );

			//Upload the file
			var res =  StartCoroutine( POST() );

			Debug.Log( res );
			

			//TODO

			//Move Uploaded Files to sent directory
			//Only if they were successfully uploaded..
			File.Move( path + f.Name, destinationPath + f.Name );
		}		


		
	}

	private IEnumerator POST()
	{
	
		Debug.Log( "Posting Json to server..." + jsonString );

		UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/session", jsonString );
		www.SetRequestHeader("Accept", "application/json");
		yield return www.SendWebRequest();

		Debug.Log( "Got this far...." + www.downloadHandler.text );

			
		
	}



	IEnumerator WaitForRequest( WWW data )
	{
		Debug.Log( "Uploading Json...." );
		yield return data;

		Debug.Log( "Got this far ..." );

		if( data.error != null )
		{
			Debug.Log( data.error );
		}
		else
		{
			Debug.Log( "WWW Request : " + data.text );
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
