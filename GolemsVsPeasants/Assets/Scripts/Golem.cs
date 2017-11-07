using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Golem : OVRGrabbable 
{
	NavMeshAgent _agent;
	Rigidbody _rb;
	bool _falling = false;
	Coroutine _dropCoroutine;


	void Awake()
	{
		_agent = GetComponent<NavMeshAgent> ();
		_rb = GetComponent<Rigidbody> ();
	}


	
	void Update () 
	{
		if(_falling && Mathf.Abs(_rb.velocity.y) < 0.1f ){
			_falling = false;
		}
	}

	public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
	{
		m_grabbedBy = hand;
		m_grabbedCollider = grabPoint;
		_falling = false;
		_agent.enabled = false;
	}


	public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
	{
		m_grabbedBy = null;
		m_grabbedCollider = null;

		if (_dropCoroutine != null)
			StopCoroutine (_dropCoroutine);

		_dropCoroutine = StartCoroutine (_DropCoroutine());
	}


	IEnumerator _DropCoroutine()
	{
		_falling = true;

		_rb.isKinematic = false;
		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;

		yield return new WaitWhile (() => _falling); // wait to hit ground

		_agent.enabled = true;
		_rb.isKinematic = true;

		yield return new WaitForSeconds (0.4f);

		_agent.SetDestination (transform.position + transform.forward*100); // just walk forward
	}
}
