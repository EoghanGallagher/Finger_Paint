using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class Transition
	{
	public int TransitionNo { get; set; }
	public string TransistionName { get; set; }

	public string TransitionTime{ get; set; }

	public string Source { get; set; }
	public string Destination{  get; set; }

	public List <Error> transitionErrors = new List<Error>(); 
}
