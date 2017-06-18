using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchController : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Block")
		{
//			coll.gameObject.SendMessage();
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
		this.gameObject.SetActive(false);
		Debug.Log("hit block");

	}

}
