using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour 
{
	public float maxDeep = -10f;
	void Update()
	{
		if (transform.position.y<maxDeep)
		{
			gameObject.SetActive(false);
		}
	}
}
