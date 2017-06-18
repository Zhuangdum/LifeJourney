using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintGravity : MonoBehaviour 
{
	public GameObject guid;
	private GameObject _guid;
	public float rollSpeed = 1f;
	void Start () 
	{
		_guid = Instantiate(guid);
		_guid.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	}
	
	void Update () 
	{
		Debug.Log("xxx" + Input.acceleration.x + "   yyy: " + Input.acceleration.y);
		_guid.transform.position += new Vector3(Input.acceleration.x, Input.acceleration.y, 0)*rollSpeed;
		if(_guid.transform.position.x<0)
			_guid.transform.position = new Vector3(0, Input.acceleration.y, 0);
		else if(_guid.transform.position.x>Screen.width)
			_guid.transform.position = new Vector3(Screen.width, Input.acceleration.y, 0);

		if(_guid.transform.position.y<0)
			_guid.transform.position = new Vector3(Input.acceleration.x, 0, 0);
		else if(_guid.transform.position.y>Screen.width)
			_guid.transform.position = new Vector3(Input.acceleration.y, Screen.height, 0);


			
	}
}
