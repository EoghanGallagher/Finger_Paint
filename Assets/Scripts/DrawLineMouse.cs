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
		Texture2D tex = null;
		float useLineWidth = 0;
		
		VectorLine.canvas.sortingOrder = -1;


		if ( useEndCap ) 
		{
			VectorLine.SetEndCap ("RoundCap", EndCap.Mirror, capLineTex, capTex);
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

	void Update () 
	{
		Vector3 newPoint = GetMousePos();
		
	
		// Mouse button clicked, so start a new line
		if ( Input.GetMouseButtonDown( 0 ) ) 
		{

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
				line.points2.Add ( newPoint );
				pointCount = line.points2.Count;
				successLine.Add( newPoint );
				
				line.Draw();
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


	private void RedrawLine()
	{
		
		Debug.Log( "Redrawing Line" );
		
		VectorLine line = new VectorLine("DrawnLine", new List<Vector2>(), capLineTex, 8.0f, LineType.Continuous, Joins.Weld);		


		foreach( Vector3 point in successLine )
		{
			line.points2.Add( point );
		}

		line.Draw();
	}


}
