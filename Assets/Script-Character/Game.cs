using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
//using System.Runtime.Serialization.Json;

[DataContract]
public class Game : MonoBehaviour, ISerializable {
    public static Game current;

    [DataMember(Name = "time")]
    public float time;
    public float Time
    {
        get { return time; }
        set { time = value; }
    }


    [DataMember(Name = "maxHealth")]
    public int maxHealth;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    [DataMember(Name = "health")]
    public int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    [DataMember(Name = "point")]
    public int point;
    public int Point
    {
        get { return point; }
        set { point = value; }
    }

    [DataMember(Name = "x")]
    public float x;
    public float X
    {
        get { return x; }
        set { x = value; }
    }

    [DataMember(Name = "y")]
    public float y;
    public float Y
    {
        get { return y; }
        set { y = value; }
    }

    [DataMember(Name = "deaths")]
    public int deaths;
    public int Deaths
    {
        get { return deaths; }
        set { deaths = value; }
    }

    [DataMember(Name = "shoot")]
    public bool shoot;
    public bool Shoot
    {
        get { return shoot; }
        set { shoot = value; }
    }

    [DataMember(Name = "dash")]
    public bool dash;
    public bool Dash
    {
        get { return dash; }
        set { dash = value; }
    }

    [DataMember(Name = "wallJump")]
    public bool wallJump;
    public bool WallJump
    {
        get { return wallJump; }
        set { wallJump = value; }
    }

    [DataMember(Name = "doubleJump")]
    public bool doubleJump;
    public bool DoubleJump
    {
        get { return doubleJump; }
        set { doubleJump = value; }
    }

    string fileName = "GameSave.Json";


    public Game()
    {
        time = GameObject.Find("player_character").GetComponent<playerController>().timer;
        health = GameObject.Find("player_character").GetComponent<playerController>().health;
        maxHealth = GameObject.Find("player_character").GetComponent<playerController>().maxHealth;
        point = GameObject.Find("player_character").GetComponent<playerController>().points;
        deaths = GameObject.Find("player_character").GetComponent<playerController>().DeathCount;
        x = GameObject.Find("player_character").GetComponent<playerController>().saveZone.x;
        y = GameObject.Find("player_character").GetComponent<playerController>().saveZone.y;
        shoot = GameObject.Find("player_character").GetComponent<playerController>().hasGun;
        wallJump = GameObject.Find("player_character").GetComponent<playerController>().wallJumping;
        dash = GameObject.Find("player_character").GetComponent<playerController>().dashing;
        doubleJump = GameObject.Find("player_character").GetComponent<playerController>().doubleJumping;
    }
    public void save()
    {
        Game g = new Game();
        string jsonData = JsonUtility.ToJson(g, true);
        File.WriteAllText(fileName,jsonData);
     }
    public void load(Game n)
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(fileName),n); ;

        GameObject.Find("player_character").GetComponent<playerController>().timer = time;
        GameObject.Find("player_character").GetComponent<playerController>().health = health;
        GameObject.Find("player_character").GetComponent<playerController>().maxHealth = maxHealth;
        GameObject.Find("player_character").GetComponent<playerController>().points = point;
        GameObject.Find("player_character").GetComponent<playerController>().DeathCount = deaths;
        GameObject.Find("player_character").GetComponent<playerController>().transform.position = new Vector3(x, y,0);
        GameObject.Find("Main Camera").GetComponent<Camera_Follow>().transform.position = new Vector3(x, y,-10);
        GameObject.Find("player_character").GetComponent<playerController>().hasGun = shoot;
        GameObject.Find("player_character").GetComponent<playerController>().wallJumping = wallJump;
        GameObject.Find("player_character").GetComponent<playerController>().dashing = dash;
        GameObject.Find("player_character").GetComponent<playerController>().doubleJumping = doubleJump;



        /* FileStream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read);
         DataContractSerializer inputSerializer;
         inputSerializer = new DataContractSerializer(typeof(Game));
         current = (Game)inputSerializer.ReadObject(reader);

         GameObject.Find("Main Camera").GetComponent<Camera_Follow>().transform.position = new Vector3(x, y,-10);
         reader.Close();

         //////////////////////////////////////

         */
    }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("time: ", time, typeof(float));
        info.AddValue("health: ", health, typeof(int));
        info.AddValue("maxHealth: ", maxHealth, typeof(int));
        info.AddValue("point: ", point, typeof(int));
        info.AddValue("x: ", x, typeof(float));
        info.AddValue("y: ", y, typeof(float));
        info.AddValue("deaths: ", deaths, typeof(int));
        info.AddValue("shoot: ", shoot, typeof(bool));
        info.AddValue("dash: ", dash, typeof(bool));
        info.AddValue("wallJump: ", wallJump, typeof(bool));
        info.AddValue("doubleJump: ", doubleJump, typeof(bool));
        Vector2 place = new Vector2(x, y);
        GameObject.Find("player_character").GetComponent<playerController>().timer = time;
        GameObject.Find("player_character").GetComponent<playerController>().health = health;
        GameObject.Find("player_character").GetComponent<playerController>().maxHealth = maxHealth;
        GameObject.Find("player_character").GetComponent<playerController>().points = point;
        GameObject.Find("player_character").GetComponent<playerController>().DeathCount = deaths;
        GameObject.Find("player_character").GetComponent<playerController>().hasGun = shoot;
        GameObject.Find("player_character").GetComponent<playerController>().dashing = dash;
        GameObject.Find("player_character").GetComponent<playerController>().wallJumping = wallJump;
        GameObject.Find("player_character").GetComponent<playerController>().doubleJumping = doubleJump;
    }
}
