using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour 
{
	public Transform targetPos;
	public GameObject blockprefab;
	private GameObject block;
	public static ChangeBackground instance;
	private List<GameObject> blockList = new List<GameObject>();
	void Start()
	{
		instance = this;
		GenerateBackground();
	}
	public void GenerateBackground()
	{
		if (blockList.Count>0)
		{
			foreach (var item in blockList)
			{
				Destroy(item.gameObject);
			}
			blockList.Clear();
		}
		int randomValue = Random.Range(1, 4);
		//generate bottom
		float height;
		float width;
		switch (randomValue)
		{
			case 1:
				height = Random.Range(1.5f, 3f);
				width = Random.Range(0f, 5f);
				block = Instantiate(blockprefab);
				block.transform.position = new Vector3(targetPos.position.x + width, targetPos.position.y - height, 0);
				block.transform.localScale *= Random.Range(0.8f, 1.3f);
				blockList.Add(block);
				break;
			case 2:
				float tempValue = 0;
				for (int i = 0; i < 2; i++) 
				{
					height = Random.Range(1, 2)+i;
					width = Random.Range(0, 4)+i;
					if (i == 0)
					{
						tempValue = width;
					}
					else
					{
						if (tempValue > 2.5f)
							width = tempValue - Random.Range(2f, 4f);
						else
							width = tempValue + Random.Range(2f, 4f);
					}
					block = Instantiate(blockprefab);
					block.transform.position = new Vector3(targetPos.position.x + width, targetPos.position.y - height, 0);
					block.transform.localScale *= Random.Range(0.3f, 0.9f);
					blockList.Add(block);
				}
				break;
			case 3:
				for (int i = 0; i < 3; i++)
				{
					height = Random.Range(1, 2)+0.5f*i;
					width = Random.Range(0, 3)+i;
					block = Instantiate(blockprefab);
					block.transform.position = new Vector3(targetPos.position.x + width, targetPos.position.y - height, 0);
					block.transform.localScale *= Random.Range(0.3f, 0.7f);
					blockList.Add(block);
				}
				break;
			default:
				break;
		}	
		//generate top
		randomValue = Random.Range(1, 3);
		switch (randomValue)
		{
			case 1:
				height = Random.Range(3f, 5f);
				width = Random.Range(1f, 5f);
				block = Instantiate(blockprefab);
				block.transform.position = new Vector3(targetPos.position.x + width, targetPos.position.y + height, 0);
				block.transform.localScale *= Random.Range(0.8f,1.3f);
				blockList.Add(block);
				break;
			case 2:
				float tempValue = 0;
				for (int i = 0; i < 2; i++) 
				{
					height = Random.Range(2, 4)+i;
					width = Random.Range(0, 3)+2*i;
					if (i == 0)
					{
						tempValue = width;
					}
					else
					{
						if (tempValue > 2.5f)
							width = tempValue - Random.Range(2f, 3f);
						else
							width = tempValue + Random.Range(2f, 3f);
					}
					block = Instantiate(blockprefab);
					block.transform.position = new Vector3(targetPos.position.x + width, targetPos.position.y + height, 0);
					block.transform.localScale *= Random.Range(0.5f, 1f);
					blockList.Add(block);
				}
				break;
			case 3:
				for (int i = 0; i < 3; i++)
				{
					height = Random.Range(2, 5)+i;
					width = Random.Range(0, 3)+i;
					block = Instantiate(blockprefab);
					block.transform.position = new Vector3(targetPos.position.x + width, targetPos.position.y + height, 0);
					block.transform.localScale *= Random.Range(0.8f, 2.0f);
					blockList.Add(block);
				}
				break;
			default:
				break;
		}	
	}
}
