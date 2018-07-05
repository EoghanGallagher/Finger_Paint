﻿using System.Collections;
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
		Debug.Log( "Starting Star Sequence" );
		
		yield return new WaitForSeconds( 0.2f );

		//Check if last star clicked is one less than current star
		if( StarManager.previousStar == ( starValue - 1 ) )
		{
			Debug.Log( "You clicked on the correct star...." );
			_material.color = successColour;

			if( _drawLineHandler )
			{
				StarManager.lastSuccessPointCount = _drawLineHandler.PointCount;
				_drawLineHandler.DrawLine( _drawLineHandler.PointCount );
				gameManager.UpdateScore();

				starSpriteRenderer.color = new Color( 0.9716f, 0.8722f, 0.1512f, 1 );
			
			}

			StarManager.previousStar = starValue;
			StarManager.previousStarObject = this;
		
			//Disable Collider after it has been succssfully selected
			if( _collider2D )
				_collider2D.enabled = false;
		
		}
		else
		{
			Debug.Log( "Invalid: Not the correct Sequence " + isOkToDrawLine );

			iTween.ShakePosition( _transform.parent.gameObject, new Vector2( 0.2f, 0.2f ), 0.75f );
		
			isOkToColor = true;
			    iTween.ValueTo (gameObject, iTween.Hash (
					"from", errorColour, 
					"to", originalColour, 
					"time", 2.5f, 
					"easetype", "easeInCubic", 
					"onUpdate","UpdateColor"));
			
			_drawLineHandler.DestroyLine();
		}
	
	}

	void UpdateColor(Color newColor)
 	{
    	 starSpriteRenderer.color = newColor;
 	}

}
