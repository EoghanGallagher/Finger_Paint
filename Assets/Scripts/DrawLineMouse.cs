using UnityEngine;
using Vectrosity;
using System.Collections.Generic;

public class DrawLineMouse : MonoBehaviour 
{
	[SerializeField] private Texture2D lineTex;
	[SerializeField] private int maxPoints = 5000;
	[SerializeField] private float lineWidth = 4.0f;
	[SerializeField] private int minPixelMove = 5;	// Must move at least this many pixels per sample for a new segment to be recorded
	[SerializeField] private bool useEndCap = false;
	[SerializeField] private Texture2D capLineTex;
	[SerializeField] private Texture2D capTex;
	[SerializeField] private float capLineWidth = 20.0f;
// If line3D is true, the line is drawn in the scene rather than as an overlay. Note that in this demo, the line will look the same
// in the game view either way, but you can see the difference in the scene view.
	[SerializeField] private bool line3D = false;
	[SerializeField] private float distanceFromCamera = 1.0f;
	[SerializeField] private VectorLine line;
	[SerializeField] private Vector3 previousPosition;
	[SerializeField] private int sqrMinPixelMove;
	[SerializeField] private bool canDraw = false;
	[SerializeField] private int pointCount;

	[SerializeField] private Camera renderCamera;
 	public int PointCount { get{ return pointCount; } set{ value = pointCount;} }

	
	[SerializeField] private List<Vector3> successLine = new List<Vector3>();


	void OnEnable()
	{

	}

	void OnDisable()
	{

	}

	void Start()
	{
		CreateLine();
	}

	void Update () 
	{
		Vector3 newPoint = GetMousePos();
		
		// Mouse button clicked, so start a new line
		if ( Input.GetMouseButtonDown( 0 ) ) 
		{
			if( line == null )
			{
				CreateLine();
			}	
			//RedrawLine();
			if( !line.collider )
				line.collider = true;
		
			if ( line3D ) 
			{
				line.points3.Clear();
			}
			else 
			{
				line.points2.Clear();
			}
			line.Draw();
			previousPosition = Input.mousePosition;
			if ( line3D ) 
			{
				line.points3.Add ( newPoint );
			}
			else 
			{
				line.points2.Add ( newPoint );
			}
			canDraw = true;

		}
		// Mouse button held down and mouse has moved far enough to make a new point
		else if ( Input.GetMouseButton( 0 ) && ( Input.mousePosition - previousPosition ).sqrMagnitude > sqrMinPixelMove && canDraw ) 
		{
			previousPosition = Input.mousePosition;
			if ( line3D ) 
			{
				line.points3.Add ( newPoint );
				pointCount = line.points3.Count;
				line.Draw3D();
			}
			else 
			{
				if( line != null )
				{
				
					line.points2.Add ( newPoint );
					pointCount = line.points2.Count;
					successLine.Add( newPoint );
				
					line.Draw();

				}
			}
			if ( pointCount >= maxPoints ) 
			{
				canDraw = false;
			}
		}
	}

	Vector3 GetMousePos () 
	{
		var p = Input.mousePosition;

		if (line3D) 
		{
			p.z = distanceFromCamera;
			return Camera.main.ScreenToWorldPoint (p);
		}
		return p;
	}


	void EndLine()
	{
		canDraw = false;
	}


	void StartLine()
	{
		canDraw = true;
	}

	

	//Draw a custom line.
	public void DrawLine( int lastSuccessPoint  )
	{
		
		Debug.Log( "----Redrawing Line" );
		Debug.Log( lastSuccessPoint );

		VectorLine newLine = new VectorLine("DrawnLine", new List<Vector2>(), capLineTex, lineWidth, LineType.Continuous, Joins.Weld);		

		for( int i = 0; i < lastSuccessPoint-1; i++ )
		{
			
			if( line.points2[i] != null )
			{	
				Debug.Log( line.points2[i] );
				newLine.points2.Add( line.points2[i] );
			}
				
		}

		//line.points2.RemoveRange( 0, line.points2.Count );
		//line.active = false;
		
		newLine.Draw();

		//line.active = true;

		
	}


	public void EraseLine( )
	{
		for( int i = 0; i < line.points2.Count; i++ )
		{
		    line.points2.Remove( line.points2[i] );
		}

		line.Draw();
	}

	public void ClearLine()
	{
		line.points2.Clear();
		//line.Draw();
	}


	public void DestroyLine()
	{
		VectorLine.Destroy( ref line );
	}

	private void CreateLine()
	{
			Texture2D tex = null;
		float useLineWidth = 0;

		//Get the scene camera
		//renderCamera = GetComponent<Camera>();
		
		//Set the Vector Line canvas camera
		//VectorLine.SetCanvasCamera( renderCamera );
		
		//Set the render mode
		//VectorLine.canvas.renderMode = RenderMode.ScreenSpaceCamera;
		
		//Set the default sorting order for the line. In this case make it appear behind objects.
		VectorLine.canvas.sortingOrder = 20;
		


		if ( useEndCap ) 
		{
			VectorLine.SetEndCap ( "RoundCap", EndCap.Mirror, capLineTex, capTex );
			tex = capLineTex;
			useLineWidth = capLineWidth;
		}
		else 
		{
			tex = lineTex;
			useLineWidth = lineWidth;
		}
	
		if (line3D) 
		{
		
			line = new VectorLine("DrawnLine3D", new List<Vector3>(), tex, useLineWidth, LineType.Continuous, Joins.Weld);
		}
		else 
		{
			line = new VectorLine("DrawnLine", new List<Vector2>(), tex, useLineWidth, LineType.Continuous, Joins.Weld);		
		}
		line.endPointsUpdate = 1;	// Optimization for updating only the last point of the line, and the rest is not re-computed
		if (useEndCap) 
		{
			line.endCap = "RoundCap";
		}
		// Used for .sqrMagnitude, which is faster than .magnitude
		sqrMinPixelMove = minPixelMove*minPixelMove;
		
		line.collider = false;
	}

}
