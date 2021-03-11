using UnityEngine;
using System.Collections;

public interface AIStateMachine
{
	BaseState currentState { get; set; }
}

public static class AiStateMachineExtention
{
	// the where T : BaseState is a constraint, ensuring that every T provided is a basestate object
	public static void SetState <T> (this AIStateMachine value) where T : BaseState, new()
	{
		//checking if current state is null, as the first time it will be called it is
		if (value.currentState != null) {
			value.currentState.OnStateExit ();
		}
		//assigning the base state variable to equal new T
		value.currentState = new T ();
		value.currentState._sm = value;
		value.currentState.OnStateEnter ();
	}

	public static void Update (this AIStateMachine value)
	{
		if (value.currentState != null) {
			value.currentState.OnStateUpdate ();
		}
	}

}
/*
public class AIStateMachine : MonoBehaviour
{
	private BaseState currentState;

	// the where T : BaseState is a constraint, ensuring that every T provided is a basestate object
	public void SetState <T> () where T : BaseState, new()
	{
		//checking if current state is null, as the first time it will be called it is
		if (currentState != null) {
			currentState.OnStateExit ();
		}
		//assigning the base state variable to equal new T
		currentState = new T ();
		currentState._sm = this;
		currentState.OnStateEnter ();
	}

	protected virtual void Update ()
	{
		if (currentState != null) {
			currentState.OnStateUpdate ();
		}
	}
}
*/
