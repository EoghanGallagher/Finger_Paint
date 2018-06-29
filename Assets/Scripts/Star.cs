using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour 
{

	[SerializeField] private int starValue = 0;

	private LinkHandler linkHandler;
	private Transform _transform;
	private Material _material;

	private Color originalColour;
	private Color errorColour;
	private Color successColour;

	private DrawLineMouse _drawLineHandler;


	void Start()
	{
		linkHandler = Object.FindObjectOfType<LinkHandler>();
		_transform = transform;
		_material = GetComponent<Renderer>().material;

		_drawLineHandler = GameObject.Find( "Main Camera" ).GetComponent<DrawLineMouse>();



		originalColour = _material.color;
		successColour = Color.green;
		errorColour = Color.red;
	}

	// void OnMouseDown()
    // {
    //     Debug.Log( "Clicked " + gameObject.name );
		
	// 	StartCoroutine( "StarSequence" );
    // }

	void OnTriggerEnter2D( Collider2D other )
	{

		Debug.Log( "Trigger :  " + other.name );	
	   if( other.name.Equals( "DrawnLine" ) )
			StartCoroutine( "StarSequence" );
	}

	IEnumerator StarSequence()
	{
		Debug.Log( "Starting Star Sequence" );
		
		yield return new WaitForSeconds( 0.2f );

		//Check if last star clicked is one less than current star
		if( StarManager.previousStar == ( starValue - 1 ) )
		{
			Debug.Log( "Valid: Draw a line between these two points." );
			_material.color = successColour;

			if( _drawLineHandler )
			{
				Debug.Log( "Current Point Count of line is : " + _drawLineHandler.PointCount );
			}



			// if( StarManager.previousStar != -1 )
			// 	Debug.Log( "Starting Position ...Ignore" );

			//Update the StarManager with the current star
			//This will be used as a source for the line.
	
			//Draw link between stars
			// if( linkHandler && StarManager.previousStar != -1  )
			// {
			// 	//Debug.Log( StarManager.previousStarObject.name );
			// 	linkHandler.InitLink( StarManager.previousStarObject.transform.position, _transform.position );

			// }

			StarManager.previousStar = starValue;
			StarManager.previousStarObject = this;
			StarManager.score ++;
			Debug.Log( StarManager.score );


			if( starValue == 24 )
			{
				Debug.Log( "End Line..." );
			}

			

		}
		else
		{
			Debug.Log( "Invalid: Not the correct Sequence" );
			_material.color = errorColour;
		}
	

		//Debug.Log( "Ending Star Sequence" );
	}

}
