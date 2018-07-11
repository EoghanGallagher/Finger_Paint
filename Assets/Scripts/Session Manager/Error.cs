using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Error
{
	public string ErrorType { get; set; }  //Error Types : Proximity , Preservative 
	public string Source{ get; set; } //Transition source
	public string Destination{ get; set; } //Transition Destination
	public string ErrorTimeStamp{ get; set; } //Time of error
	
}
