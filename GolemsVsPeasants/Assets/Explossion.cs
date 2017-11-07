using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explossion : MonoBehaviour
{
	public float Magnitude = 5f;

	List<Explodable> _explodables;


	void Awake()
	{
		_explodables = new List<Explodable> ();
	}


	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}


	void OnTriggerEnter(Collider other)
	{
		Explodable explodable = other.GetComponent<Explodable> ();
		if (explodable != null) {
			_explodables.Add (explodable);
		}
	}

	void OnTriggerExit(Collider other)
	{
		Explodable explodable = other.GetComponent<Explodable> ();
		if (explodable != null) {
			_explodables.Remove(explodable);
		}
	}


	IEnumerator _ExplossionCoroutine()
	{
		yield return new WaitForSeconds (0.1f);

		foreach (Explodable x in _explodables) {
			;//x.e
		}
	}



}
