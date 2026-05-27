using UnityEngine;

public interface IChaser
{
	public GameObject CurrentTarget { get; protected set; }
}