using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileTester : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () 
	{
		yield return new WaitForSeconds( 1.0f );
		FileManager fM = GameObject.Find("FileManager").GetComponent<FileManager>();
		fM.SaveFile();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
