using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CustomerObject", menuName = "Scriptable Objects/CustomerObject")]
public class CustomerObject : ScriptableObject
{
	public List<Sprite> customerSprites;
	public List<Sprite> accessorySprites;
}
