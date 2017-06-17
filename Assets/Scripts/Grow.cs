using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一颗生长在地下的树苗，随着时间的流逝不断长大，
/// 最后经过不断的调整生长的方向冲破地下的种种困难， 沐浴到地面上的阳光
/// if there is light, the hope will also be there
/// 
/// </summary>

public enum Direction
{
	North,
	South,
	West,
	East
}
public enum Type
{
	Branch,
	Root
}

public class Grow : MonoBehaviour 
{
	public GameObject applePrefab;
	public Transform bornPos;
	public GameObject guidPrefab;
	private GameObject guidSprite;
	public Transform rootUI;
	public Transform continueUI;
	public Transform overUI;
	public float maxAcceleration = 10f;

	[Header("Branch")]
	public float trunkGrowSpeed = 1f;
	public float trunkStrongSpeed = 0.1f;
	public float trunkSeperateTime = 1f;
	public GameObject trunkPrefab;
	private Dictionary<int, List<Branch>> trunkDic = new Dictionary<int, List<Branch>>();
	private int trunkCount;
	public Direction trunkDir = Direction.North;
	private List<Transform> trunkParentList = new List<Transform>();


	[Header("Root")]
	public float rootGrowSpeed = 1f;
	public float rootStrongSpeed = 0.1f;
	public float rootSeperateTime = 1f;
	public GameObject rootPrefab;
	public Direction rootDir = Direction.South;
	private int rootCount;
	private List<Transform> rootParentList = new List<Transform>();
	private Dictionary<int, List<Branch>> rootDic = new Dictionary<int, List<Branch>>();

	public int maxNum;
	private int _maxNum;
	private List<GameObject> appleList = new List<GameObject>();
	void Start () 
	{
		guidSprite = Instantiate(guidPrefab);
	}
	private bool canGenApple;
	private bool isReachTop;
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.acceleration.magnitude>maxAcceleration)
		{
			if(isReachTop)
				ShowOverUI(true);
			else 
				ShowContinueUI(true);
			
			Pause(true);
			FallApple();
		}

		if (isPause)
			return;

		Vector3 targetPos;

		MathfHelper.GetScreenPos(out targetPos, transform.position);
		guidSprite.transform.position = targetPos;

		if (Input.GetMouseButtonDown(0) && _maxNum<maxNum)
		{
			_maxNum++;

			trunkParentList.Add( new GameObject("Tree" + trunkCount.ToString()).transform);
			GameObject go = Instantiate(trunkPrefab);
			go.transform.position = new Vector3(targetPos.x, bornPos.position.y, 0);

			go.transform.SetParent(trunkParentList[trunkCount]);
			Branch branch = new Branch(go);
			List<Branch> branchList = new List<Branch>();
			branchList.Add(branch);
			trunkDic.Add(trunkCount, branchList);
			trunkCount++;

			//========================================

			rootParentList.Add( new GameObject("Root" + rootCount.ToString()).transform);
			GameObject root = Instantiate(rootPrefab);
			root.transform.position = new Vector3(targetPos.x, bornPos.position.y, 0);

			root.transform.SetParent(rootParentList[rootCount]);
			Branch rootBranch = new Branch(root);
			List<Branch> rootList = new List<Branch>();

			rootList.Add(rootBranch);
			rootDic.Add(rootCount, rootList);
			rootCount++;

		}
		ControlPlants(trunkDic, Type.Branch);
		ControlPlants(rootDic, Type.Root);

	}
	private float growSpeed;
	private float strongSpeed;
	private float seperateTime;
	private GameObject prefab;
	private Direction dir;
	private void ControlPlants(Dictionary<int, List<Branch>> dictionary, Type type)
	{
		switch (type)
		{
			case Type.Branch:
				growSpeed = trunkGrowSpeed;
				strongSpeed = trunkStrongSpeed;
				seperateTime = trunkSeperateTime;
				prefab = trunkPrefab;
				dir = trunkDir;
				break;
			case Type.Root:
				growSpeed = rootGrowSpeed;
				strongSpeed = rootStrongSpeed;
				seperateTime = rootSeperateTime;
				prefab = rootPrefab;
				dir = rootDir;
				break;
			default:
				break;
		}

		foreach(var item in dictionary)
		{
			for (int i = 0; i < item.Value.Count; i++)
			{
				if (item.Value[i]!=null)
				{
					if (item.Value[i].canGrow)
					{
						Vector3 origin = item.Value[i].gameObject.transform.localScale;
						item.Value[i].gameObject.transform.localScale += new Vector3(0, growSpeed * Time.deltaTime, 0);
						item.Value[i].growTime += Time.deltaTime;
					}
					else
					{
						Vector3 origin = item.Value[i].gameObject.transform.localScale;
						item.Value[i].gameObject.transform.localScale += new Vector3(origin.x * strongSpeed * Time.deltaTime, 0, 0);	
					}

					if (item.Value[i].growTime >= seperateTime && item.Value[i].canSeparate)
					{
						float angle = Mathf.Atan(item.Value[i].direction.y / item.Value[i].direction.x);
						float length = item.Value[i].gameObject.transform.localScale.y;
						Vector3 rebornPos = item.Value[i].gameObject.transform.FindChild("Top").transform.position;
						if (rebornPos.y > 6)
						{
							SetAppleList();
							Pause(true);
							isReachTop = true;
						}


						//get random num
						int num = Random.Range(1, 3);
						for (int k = 0; k < num; k++) {
							// create direction
							Vector2 randomDir;
							switch (dir)
							{
								
								case Direction.North:
									randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
									break;
								case Direction.South:
									randomDir = new Vector2(Random.Range(-1f, 1f), -Random.Range(0.5f, 1f));
									break;
								default:
									randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
									break;
							}

							GameObject go = Instantiate(prefab);
							go.transform.position = rebornPos;
							go.transform.Rotate(new Vector3(0, 0, 180f*Mathf.Atan(randomDir.x / randomDir.y)/Mathf.PI));

							go.transform.SetParent(trunkParentList[item.Key]);
							Branch branch = new Branch(go);
							branch.direction = new Vector3(randomDir.x, randomDir.y, 0);
							item.Value.Add(branch);
						}

						item.Value[i].canGrow = false;
						item.Value[i].canSeparate = false;
					}
					else
					{

					}
				}
			}
		}
	}


	private bool isPause = false;
	public void Pause(bool b)
	{
		isPause = b;
	}
	public void Restart()
	{
		//=============================
		for (int i = 0; i < trunkParentList.Count; i++)
		{
			Destroy(trunkParentList[i].gameObject);
		}
		trunkDic.Clear();
		trunkCount = 0;
		trunkParentList.Clear();

		//=============================
		for (int j = 0; j < rootParentList.Count; j++)
		{
			Destroy(rootParentList[j].gameObject);
		}
		rootDic.Clear();
		rootCount = 0;
		rootParentList.Clear();

		//=============================
		for (int k = 0; k < appleList.Count; k++)
		{
			Destroy(appleList[k].gameObject);
		}
		appleList.Clear();

		_maxNum = 0;
		ShowContinueUI(false);
		Pause(false);
		isReachTop = false;

	}
	public void Continue()
	{
		ShowContinueUI(false);
		Pause(false);
	}

	private void ShowContinueUI(bool b)
	{
		if (rootUI.gameObject.activeSelf != b)
		{
			rootUI.gameObject.SetActive(b);
		}
		if (continueUI.gameObject.activeSelf != b)
		{
			continueUI.gameObject.SetActive(b);
		}
	}
	private void ShowOverUI(bool b)
	{
		if (rootUI.gameObject.activeSelf != b)
		{
			rootUI.gameObject.SetActive(b);
		}
		if (overUI.gameObject.activeSelf != b)
		{
			overUI.gameObject.SetActive(b);
		}
	}
	public void ChangeNext()
	{
		Restart();
		ShowOverUI(false);
	}
	private void FallApple()
	{
		if (appleList.Count>0)
		{
			for (int i = 0; i < appleList.Count; i++)
			{
				appleList[i].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			}
		}
	}

	private void SetAppleList()
	{
		if (appleList.Count>0)
			return;
		if (trunkParentList.Count>0)
		{
			List<GameObject> tempAppleList = new List<GameObject>();
			foreach (var item in trunkDic.Values)
			{
				for (int i = 0; i < item.Count; i++)
				{
					float temY = item[i].gameObject.transform.position.y;
					if (temY>4f && temY<6f)
					{
						tempAppleList.Add(trunkDic[0][i].gameObject);
					}
				}
			}
			if (tempAppleList.Count>0)
			{
				int tempValue = Random.Range(10, 20);
				tempValue = tempValue > tempAppleList.Count ? tempAppleList.Count : tempValue;
				Debug.Log(tempValue + "     value");
				for (int j = 0; j < tempValue; j++)
				{
					GameObject apple = Instantiate(applePrefab);
					apple.transform.position = tempAppleList[Random.Range(1, tempAppleList.Count)].transform.position;
					appleList.Add(apple);
				}
			}
		}
	}
}
