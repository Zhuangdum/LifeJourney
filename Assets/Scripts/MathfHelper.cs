using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathfHelper
{
	public static void GetScreenPos(out Vector3 mousePositionInWorld, Vector3 origin)
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(origin);  
		Vector3 mousePositionOnScreen = Input.mousePosition;   
		mousePositionOnScreen.z = screenPosition.z;  
		mousePositionInWorld =  Camera.main.ScreenToWorldPoint(mousePositionOnScreen);  
	}

}
