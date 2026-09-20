using System.Collections.Generic;
using UnityEngine;

public enum Accessory
{
	NONE,
	HAT,
	TIE,
	BOWTIE,
	GLASSES,

}

public static class AccessoryData
{
	public static readonly Dictionary<Accessory, int> AccessoryOffsets = new Dictionary<Accessory, int>();
	public static void LoadFromJson(TextAsset constantsFile)
	{
		var data = JsonUtility.FromJson<CustomerConstants>(constantsFile.text);
		AccessoryOffsets.Clear();
		foreach (var accessoryData in data.customers.accessories)
		{
			if (System.Enum.TryParse(accessoryData.name, true, out Accessory accessory))
			{
				AccessoryOffsets[accessory] = accessoryData.yOffset;
			}
		}
	}
}

[System.Serializable]
public class CustomerConstants
{
	public CustomerData customers;
}

[System.Serializable]
public class CustomerData
{
	public AccessoryJSONData[] accessories;
}

[System.Serializable]
public class AccessoryJSONData
{
	public string name;
	public int yOffset;
}