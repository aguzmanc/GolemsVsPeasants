using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour 
{
	public delegate void DestroyingDelegate (Explodable source);
	public event DestroyingDelegate OnDestroying;

	void Start () {
	}

	void Update () {
	}

	protected virtual void OnDestroy()
	{
		if (OnDestroying!=null)
			OnDestroying(this);
	}




	public void NotifyExplossion(Explossion explossion)
	{
		//afdasdfasd
		// NOTIFICAR EXPLOSION
		// ELIMINAR DE LISTA DE explodables si es que se sale del trigger
		// 
	}
}
