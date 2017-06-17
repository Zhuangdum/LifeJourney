using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateApple : MonoBehaviour 
{
	public GameObject applePrefab;
	void Start () 
	{
		
	}
	
	void Update () 
	{
		Vector3 targetPos;
		MathfHelper.GetScreenPos(out targetPos, transform.position);

		if (Input.GetMouseButtonDown(1))
		{
			GameObject go = Instantiate(applePrefab);
			go.transform.position = targetPos;
		}

	}
}
