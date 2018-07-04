using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour 
{
	[SerializeField] private float speed = 0.1f;

	private Transform _transform;
	private Vector3 offset = new Vector3( 0.2f, 0.2f, 0 );
	

	void Start ()
	{
		_transform = transform;
	}
	// Update is called once per frame
	void Update () 
	{
		
		if ( Input.GetMouseButton( 0 ) ) 
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
         	
			 _transform.rotation = Quaternion.LookRotation( Vector3.forward, ( mousePos - transform.position ) * speed );

			Vector3 curScreenPoint = new Vector3( Input.mousePosition.x, Input.mousePosition.y, 1 );
			 
			Vector3 curPosition = Camera.main.ScreenToWorldPoint( curScreenPoint );
 			transform.position = curPosition;
		}
	}
}
