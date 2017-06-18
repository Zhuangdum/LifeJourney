using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Branch")
		{
			coll.gameObject.SendMessage("GetPower", 10f, SendMessageOptions.RequireReceiver);
		}	
	}
}
