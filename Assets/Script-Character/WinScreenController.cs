using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour {

    public string MainMenuScene;
    string UserName;
    int score;
    float time;
    int timeMin;
    int timeSec;
    int timeScore;
    int deaths;
    int attempts;
    int finalScore;
    string CreateUserURL = "https://scmarchi29.000webhostapp.com/InsertGameData.php";
    string timeString;


    // Use this for initialization
    void Start () { 
        //code to get the score and time
        Game n = new Game();
        score = n.Point;
        time = n.Time;
        deaths = n.Deaths;
        attempts = deaths + 1;
        if(time > 2400)
        {
            timeScore = 2400;
        }
        else
        {
            timeScore = (int)time;
        }
        finalScore = 200 - ((timeScore / 12) + (deaths * 5)) + score;
        timeMin = (int)time / 60;
        timeSec = (int)time % 60;

        GameObject scoreText = GameObject.Find("ScoreText");
        Text scoreTxt = scoreText.GetComponent<Text>();
        scoreTxt.text = "Score: " + finalScore;

        GameObject timeText = GameObject.Find("TimeText");
        Text timeTxt = timeText.GetComponent<Text>();
        timeTxt.text = "Time: " + timeMin + ":" + timeSec;
    }


    /*
     * Submit score to database and go to the main menu
     */
    public void SubmitScore()
    {
        GameObject usernameInput = GameObject.Find("UsernameField");
        InputField usernameIn = usernameInput.GetComponent<InputField>();
        UserName = usernameIn.text;
        //Send game values when game is finished with game variables
        //comment this out if you want to use testing
        SendData(UserName, finalScore.ToString(), attempts.ToString(), timeMin.ToString() + ":" + timeSec.ToString());

        //Test sending data
        //Uncomment this and fill in data if you want to test stuff
        //SendData(UserName, "150", "6", "4:30");

        SceneManager.LoadScene(MainMenuScene);
    }

    /*
     * Go to main menu without submitting score
     */
    public void ExitWithoutSubmit()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void SendData(string name, string score, string attempts, string time)
    {
        WWWForm form = new WWWForm();
        //form.AddField("idPost", id);
        form.AddField("namePost", name);
        form.AddField("scorePost", score);
        form.AddField("attemptsPost", attempts);
        form.AddField("timePost", time);

        WWW www = new WWW(CreateUserURL, form);
    }
}
