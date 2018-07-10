using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSessions
{

	[System.Serializable]
	public class Session : IPersistence
	{

		public string SessionName { get; set; } // Do i need this ?
		public int SessionNumber { get; set; } //unique number
		public string PlayerID { get; set; } //Which player?
		public string GameID { get; set; } //which game ?
		public string DeviceName { get; set; } //Device Name
		public string DeviceModel { get; set; } //Device Model
		public string DeviceType { get; set; } //Device Type
		public string DeviceUniqueIdentifier { get; set; } //Device Unique Identifier
		public string SessionDuration { get; set; } //Duration of session
		public string TimeStamp { get; set; } //Replace string with date time.
		public bool SessionCompleted { get; set; } //Did the player complete the session

		public int TransitionCount { get; set; }

		public  List<Transition> transistions = new List<Transition>(); //Number of transitions that occured during a session
	
		 public const string nameOfFile = "session.dat";
    	public string FileName { get { return nameOfFile; } }
	
	
	}


}
