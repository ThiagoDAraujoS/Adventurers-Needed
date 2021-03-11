using UnityEngine;
using System.Collections;

public class BaseState
{
	//getting a refrence to our statemachine
	public AIStateMachine _sm;

	//making base methods to be overridden by other states
	public virtual void OnStateEnter ()
	{

	}

	public virtual void OnStateUpdate ()
	{

	}

	public virtual void OnStateExit ()
	{

	}
}
