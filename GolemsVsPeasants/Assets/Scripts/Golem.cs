using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Golem : OVRGrabbable 
{
	public float Speed = 0.4f;

	NavMeshAgent _agent;
	Rigidbody _rb;
	bool _falling = false;
	bool _advancing = false;
	Coroutine _dropCoroutine;

	void Awake()
	{
		_agent = GetComponent<NavMeshAgent> ();
		_rb = GetComponent<Rigidbody> ();
	}


	
	void Update () 
	{
		if(_falling && _rb.velocity.y > 0f ){
			_falling = false;
		}

		if (_advancing) {
			_agent.transform.Translate (0, 0, Speed * Time.deltaTime);
		}
	}

	public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
	{
		m_grabbedBy = hand;
		m_grabbedCollider = grabPoint;
		_falling = false;
		_agent.enabled = false;
		_advancing = false;

		if (_dropCoroutine != null)
			StopCoroutine (_dropCoroutine);
	}


	public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
	{
		m_grabbedBy = null;
		m_grabbedCollider = null;

		_dropCoroutine = StartCoroutine (_DropCoroutine());
	}


	IEnumerator _DropCoroutine()
	{
		_falling = true;

		transform.forward = new Vector3 (transform.forward.x, 0, transform.forward.z);

		_rb.isKinematic = false;
		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;

		yield return new WaitWhile (() => _falling); // wait to hit ground

		_agent.enabled = true;
		_rb.isKinematic = true;

		yield return new WaitForSeconds (0.4f);

		_advancing = true;
	}
}
