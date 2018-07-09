using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SessionManager
{

	[System.Serializable]
	public class Session  
	{

		public string SessionName { get; set; }
		public  List<Transition> transistions = new List<Transition>();
	}

	public class Transition
	{
		public int TransitionNo { get; set; }
		public string TransistionName { get; set; }

		public string srcButton { get; set; }
		public string destButton{  get; set; }
	}

}
