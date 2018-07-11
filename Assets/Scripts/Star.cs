using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSessions;

public class Star : MonoBehaviour 
{

	[SerializeField] private int starValue = 0;
	[SerializeField] private bool isStarLetter;

	public bool IsStarLetter { get{ return isStarLetter; } set{ isStarLetter = value; } }

	private LinkHandler linkHandler;
	private Transform _transform;
	private Material _material;
	private Color originalColour;
	private Color errorColour = new Color( 0.9433f, 0.1382f, 0.0934f );
	private Color successColour;
	private DrawLineMouse _drawLineHandler;
	private Collider2D _collider2D;
	private bool isOkToDrawLine;
	private bool isOkToColor = false;
	private GameManager gameManager;
	private SessionManager sessionManager;
	private TransitionManager transitionManager;
	private SpriteRenderer starSpriteRenderer;
	private Transform star;
	
	void Start()
	{
		_transform = transform;
		
		//Find Sibling Star . The star in question has a sprite renderer that needs to be accessed
		star = _transform.parent.Find( "Star" );

		if( star != null )
			starSpriteRenderer = star.GetComponent<SpriteRenderer>();
		
		linkHandler = Object.FindObjectOfType<LinkHandler>();
		gameManager = Object.FindObjectOfType<GameManager>();

		if( gameManager != null )
		{
			sessionManager = gameManager.GetComponent<SessionManager>();
			transitionManager = gameManager.GetComponent<TransitionManager>();
		}


		_material = GetComponent<Renderer>().material;

		_drawLineHandler = GameObject.Find( "Main Camera" ).GetComponent<DrawLineMouse>();

		_collider2D = GetComponent<Collider2D>();

		originalColour = starSpriteRenderer.color;
		
		isOkToDrawLine = true; //A check to prevent the player from drawing a line to the wrong selection twice
	}

	//Detect when line intersects star 
	void OnTriggerEnter2D( Collider2D other )
	{	
	
	   if( other.name.Equals( "DrawnLine" ) )
			StartCoroutine( "StarSequence" );

	}

	IEnumerator StarSequence()
	{
		yield return new WaitForSeconds( 0.1f );

		//Check if last star clicked is one less than current star
		if( StarManager.previousStar == ( starValue - 1 ) )
		{
			//Check if Transition manager exists and check if a transition is already active
			if( transitionManager != null &&  !transitionManager.IsTransitionOpen )
			{
				//If a transition is not active then Create one
				transitionManager.CreateTransition();

				//Set the source for the transition 
				transitionManager.TransitionSource = gameObject.name;
			}
			else
			{
				//Set the transitions destination ..only if its the correct destination
				transitionManager.TransitionDestination = gameObject.name;
				
				//End the transition and add it to the transition list
				Transition currentTransition = transitionManager.EndTransition(); 

				if( currentTransition != null )
				{
					sessionManager.AddTransition( currentTransition );	
				}
				
				//Start a new Transition.
				transitionManager.CreateTransition();
				//Set the source for the transition
				transitionManager.TransitionSource = gameObject.name;
			}
			
			//Set the star materials color to gold ( indicating success )
			_material.color = successColour;

			if( _drawLineHandler )
			{
				StarManager.lastSuccessPointCount = _drawLineHandler.PointCount;
				_drawLineHandler.DrawLine( _drawLineHandler.PointCount );
				_drawLineHandler.LastNode = _transform;
				gameManager.UpdateScore();

				//Punch animation when correct star is encountered
				iTween.PunchScale( starSpriteRenderer.gameObject, iTween.Hash( "x",-2, "y",-2, "time",0.75f));

				starSpriteRenderer.color = new Color( 0.9716f, 0.8722f, 0.1512f, 1 );

				originalColour = starSpriteRenderer.color;
				
				if( _drawLineHandler.IsMouseUp )
				{
					_drawLineHandler.CanDraw = false;
				}

			}

			StarManager.previousStar = starValue;
			StarManager.previousStarObject = this;

		}
		if( StarManager.previousStar == starValue )
		{
			//Do Absolutely nothing for the moment...
		}
		else //Error E.G. Player clicked on wrong star
		{
			
			Error err = new Error(); //Create new instance of error class

			err.Source = StarManager.previousStarObject.name;
			err.Destination = this.name;
			err.ErrorTimeStamp = System.DateTime.Now.ToString();

			//Check the type of error that occured
			if( StarManager.previousStarObject.IsStarLetter && this.IsStarLetter ) //letter to letter error
			{
				err.ErrorType = "Preservative Error"; //Set the type of error that occured
			}
			else if( !StarManager.previousStarObject.IsStarLetter && !this.IsStarLetter ) //number to number error
			{
				err.ErrorType = "Preservative Error"; //Set the type of error that occured
			}
			else if( !StarManager.previousStarObject.IsStarLetter && this.IsStarLetter ) //number to letter error
			{
				err.ErrorType = "Number to Letter Error";
			}
			else if( StarManager.previousStarObject.IsStarLetter && !this.IsStarLetter ) //letter to number error
			{
				err.ErrorType = "Letter to number error";
			}

			
			transitionManager.AddError( err ); //Add the error to the error list for this transition

			//Consequences of picking the wrong star . Make star shake 
			iTween.ShakePosition( _transform.parent.gameObject, new Vector2( 0.2f, 0.2f ), 0.75f );
		
			//Tween between stars original colour and red and back
			isOkToColor = true;
			    iTween.ValueTo (gameObject, iTween.Hash (
					"from", errorColour, 
					"to", originalColour, 
					"time", 1.25f, 
					"easetype", "easeInCubic", 
					"onUpdate","UpdateColor"));
			
			_drawLineHandler.DestroyLine();
		}
	
	}

	void UpdateColor(Color newColor)
 	{
    	 starSpriteRenderer.color = newColor;
 	}

	
	//Dont let the player start a line in open space.
	//Force them to start drawing form the last success node.

	void OnMouseDown()
	{
		if( _drawLineHandler.LastNode.name.Equals( gameObject.name ) )
		{
			_drawLineHandler.CanDraw = true;
		}
	}

}
