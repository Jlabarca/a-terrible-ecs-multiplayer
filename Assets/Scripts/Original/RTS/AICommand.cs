using System;
using LeoECS.Monobehaviours;
using UnityEngine;

[Serializable]
public class AICommand
{
	public CommandType commandType;

	public Vector3 destination;
	public UnitView target;

	public AICommand(CommandType ty, Vector3 v, UnitView ta)
	{
		commandType = ty;
		destination = v;
		target = ta;
	}

	public AICommand(CommandType ty, Vector3 v)
	{
		commandType = ty;
		destination = v;
	}

	public AICommand(CommandType ty, UnitView ta)
	{
		commandType = ty;
		target = ta;
	}

	public AICommand(CommandType ty)
	{
		commandType = ty;
	}

	public enum CommandType
	{
		GoToAndIdle,
		GoToAndGuard,
		AttackTarget, //attacks a specific target, then becomes Guarding
		Stop,
		//Flee,
		Die,
	}
}
