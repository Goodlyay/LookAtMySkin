using UnityEngine;
using System.Collections;

public class ObjectRotator : MonoBehaviour 
{
	
	public float _sensitivity;
	private Vector3 _mouseReference;
	private Vector3 _mouseOffset;
	private Vector3 _rotation;
	private bool _isRotating;

	private static bool partRotating;
	public bool rotateParent;

	public float rotLimitMaxX;
	public float rotLimitMinX;

	public float rotLimitMaxY;
	public float rotLimitMinY;

	private Vector3 originalRot;
	private int doAgain;


	
	void Start ()
	{
		_sensitivity = 0.4f;
		_rotation = Vector3.zero;
		if(rotateParent)
			originalRot = transform.parent.eulerAngles;
		else
			originalRot = transform.eulerAngles;
	}
	
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Backspace)) {
			if (!partRotating || _isRotating)
				doAgain = 2;
		}
		if (doAgain > 0){
			if(!rotateParent)
				transform.eulerAngles = originalRot;
			else
				transform.parent.eulerAngles = originalRot;
			--doAgain;
		}


		if (rotateParent && !partRotating) {
			if (Input.GetMouseButtonDown(0))
				OnMouseDown ();

			if (Input.GetMouseButtonUp(0))
				OnMouseUp ();

		}

		if(_isRotating)
		{
			// offset
			_mouseOffset = (Input.mousePosition - _mouseReference);
			
			// apply rotation
			_rotation.x = -(_mouseOffset.x) * _sensitivity;
			_rotation.y = -(_mouseOffset.y) * _sensitivity;
			
			// rotate
			if (rotateParent){

				//transform.parent.Rotate(_rotation);
				transform.parent.RotateAround(transform.parent.position,transform.parent.up,_rotation.x);
				Camera.main.transform.RotateAround(transform.parent.position,Camera.main.transform.right,_rotation.y);
			}
			else {
				if(gameObject.name == "Head"){
					transform.RotateAround(transform.position,transform.up,-_rotation.y); // This is the one that nods yes
					transform.RotateAround(transform.position,transform.parent.up,_rotation.x); //This is the one that shakes no
				}
				else{
					//transform.Rotate(_rotation);
					transform.RotateAround(transform.position,Camera.main.transform.forward,-_rotation.x);
					transform.RotateAround(transform.position,Camera.main.transform.right,-_rotation.y);
				}
			}
			
			// store mouse
			_mouseReference = Input.mousePosition;
		}
	}
	
	void OnMouseDown()
	{
		// rotating flag
		_isRotating = true;
		if (!rotateParent)
			partRotating = true;
		
		// store mouse
		_mouseReference = Input.mousePosition;
	}
	
	void OnMouseUp()
	{
		if (!rotateParent)
			partRotating = false;
		// rotating flag
		_isRotating = false;
	}
	
}