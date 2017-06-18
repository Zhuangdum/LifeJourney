using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchController : MonoBehaviour 
{
	[HideInInspector]
	public bool canSeparate = true;
	[HideInInspector]
	public bool canGrow = true;

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Block")
		{
			HideSelf();
		}
	}
	void GetPower(float power)
	{
		//TODO: call gamemanager to add power
		Debug.Log("call success"+power);
	}
	void HideSelf()
	{
		canSeparate = false;
		canGrow = false;
		this.gameObject.SetActive(false);
	}

}
