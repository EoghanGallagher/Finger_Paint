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
	private Color errorColour = new Color( 0.9433f, 0.1382f, 0.0934f );
	private Color successColour;

	private DrawLineMouse _drawLineHandler;

	private Collider2D _collider2D;

	private bool isOkToDrawLine;
	private bool isOkToColor = false;

	private GameManager gameManager;

	SpriteRenderer starSpriteRenderer;
	Transform star;
	
	void Start()
	{
		_transform = transform;
		
		//Find Sibling Star . The star in question has a sprite renderer that needs to be accessed
		star = _transform.parent.Find( "Star" );

		if( star != null )
			starSpriteRenderer = star.GetComponent<SpriteRenderer>();
		
		linkHandler = Object.FindObjectOfType<LinkHandler>();
		gameManager = Object.FindObjectOfType<GameManager>();

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
			_material.color = successColour;

			if( _drawLineHandler )
			{
				StarManager.lastSuccessPointCount = _drawLineHandler.PointCount;
				_drawLineHandler.DrawLine( _drawLineHandler.PointCount );
				_drawLineHandler.LastNode = _transform;
				gameManager.UpdateScore();

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

		
		
			//Disable Collider after it has been succssfully selected
			//if( _collider2D )
				//_collider2D.enabled = false;

			
		
		}
		if( StarManager.previousStar == starValue )
		{
			//Do Absolutely nothing for the moment...
		}
		else
		{
			//Consequences of picking the wrong star
			iTween.ShakePosition( _transform.parent.gameObject, new Vector2( 0.2f, 0.2f ), 0.75f );
		
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

	void OnMouseDown()
	{
		Debug.Log( "Clicked on " + gameObject.name );
		if( _drawLineHandler.LastNode.name.Equals( gameObject.name ) )
		{
			Debug.Log( "This is the correct starting point. ...." );
			_drawLineHandler.CanDraw = true;
		}
	}

	

}
