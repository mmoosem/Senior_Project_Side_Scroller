using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

	public string[] items;


	// Use this for initialization
	IEnumerator Start() {
		WWW itemsData = new WWW("https://scmarchi29.000webhostapp.com/ItemsData.php"); //add url from my db
		yield return itemsData;
		string itemsDataString = itemsData.text;
		print (itemsDataString);
		items = itemsDataString.Split(';');
		print(GetDataValue(items[0], "Name:"));
	}

	string GetDataValue(string data, string index)
	{
		string value = data.Substring(data.IndexOf(index)+index.Length);
		if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
		return value;


	}
}
