using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch
{
	public GameObject gameObject;
	public float growTime;
	public bool canGrow = true;
	public bool canSeparate = true;
	public Vector3 direction = Vector3.up;
	public Branch(GameObject gameObject)
	{
		this.gameObject = gameObject;
		this.growTime = 0;
	}
}
