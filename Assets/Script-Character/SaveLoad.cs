using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SaveLoad : MonoBehaviour {
    public  Game saveGame = new Game();
	// Use this for initialization
	public static void Save (Game curr) 
    {
       // saveGame.save();
    }
	public static void Load(string file)
    {
        if (File.Exists(Application.persistentDataPath + file))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream bringin = File.Open(Application.persistentDataPath + file,FileMode.Open);
            //SaveLoad.saveGame = (List<Game>)bf.Deserialize(bringin);
            bringin.Close();
        }
    }
	
}
