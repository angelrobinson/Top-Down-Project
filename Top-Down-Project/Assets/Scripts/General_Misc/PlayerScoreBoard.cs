using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerScoreBoard : MonoBehaviour
{
    public string playerName;
    public int score;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        playerName = PlayerPrefs.GetString("currentPlayer");
        score = GameManager.Instance.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetName()
    {
        
    }
}
