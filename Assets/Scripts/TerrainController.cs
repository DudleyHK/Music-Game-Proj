using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
	public GameObject RoadPrefab;
	public Vector3 InstantiationPosition;
	public List<Road> RoadPieces = new List<Road>();
	public float Speed = 10f;

	public float CoolDown = 1f;
	public float timer = 0f;

	private void OnEnable()
	{
			PlayerMovement.onChangeSpeed += OnChangeSpeed;
	}

	private void OnDisable()
	{
			PlayerMovement.onChangeSpeed += OnChangeSpeed;
	}

	private	void Update()
	{
				foreach(var road in RoadPieces)
				{
					if(Vector3.Distance(road.transform.position, new Vector3(0f, 0f, -515.7f)) < 5f)
						{
							if(RoadPieces.Count <= 3)
							{
								var nr = Instantiate(RoadPrefab, InstantiationPosition, RoadPrefab.transform.rotation).GetComponent<Road>();

								nr.Rigidbody.velocity = RoadPieces[0].Rigidbody.velocity;

								RoadPieces.Add(nr);

								Destroy(road.gameObject);
								RoadPieces.Remove(road);


								break;
							}
						}
				}
	}


	// private IEnumerator OnChangeSpeed(PlayerMovement.SpeedChangeType _type, float _val)
	private void OnChangeSpeed(PlayerMovement.SpeedChangeType _type, float _val)
	{
		foreach(var road in RoadPieces)
		{
			if(_type == PlayerMovement.SpeedChangeType.Accelerate)
				road.Rigidbody.velocity -= transform.forward * Speed * Time.fixedDeltaTime;

				if(_type == PlayerMovement.SpeedChangeType.Deccelerate)
					road.Rigidbody.velocity += transform.forward * Speed * Time.fixedDeltaTime;
		}
	}


	private IEnumerator ChangeSpeed(PlayerMovement.SpeedChangeType _type, float _val)
	{
			foreach(var road in RoadPieces)
			{
					road.Speed = _val * 1000 * Time.fixedDeltaTime;
					yield return null;
			}
	}
}
