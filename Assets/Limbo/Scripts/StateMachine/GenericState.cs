using UnityEngine;
using System.Collections;

public class GenericState<T> : BaseState where T : AIStateMachine
{
	//in our example T is the unit class
	public T sm {
		//we get the state machine object and we cast it to T
		get {
			return (T)_sm;
		}
	}
}
