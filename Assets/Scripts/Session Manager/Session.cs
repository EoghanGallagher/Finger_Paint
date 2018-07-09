using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSessions
{

	[System.Serializable]
	public class Session  
	{


		public string SessionName { get; set; } // Do i need this ?
		public int SessionNumber { get; set; } //unique number
		public string PlayerID { get; set; } //Which player?
		public string GameID { get; set; } //which game ?
		public string DeviceType { get; set; } //Device upon which game is running
		public float SessionDuration { get; set; } //Duration of session
		public string TimeStamp { get; set; } //Replace string with date time.
		public bool SessionCompleted { get; set; } //Did the player complete the session

		public  List<Transition> transistions = new List<Transition>(); //Number of transitions that occured during a session
	}

	public class Transition
	{
		public int TransitionNo { get; set; }
		public string TransistionName { get; set; }

		public float TransitionTime{ get; set; }

		public string SourceButton { get; set; }
		public string DestinationButton{  get; set; }

		public List <Error> transitionErrors = new List<Error>(); 
	}


	public class Error
	{
		public string ErrorType;
	}

}
