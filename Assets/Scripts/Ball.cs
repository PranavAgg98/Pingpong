using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    private float speed = 30;
    private Rigidbody2D rigidBody;
    private AudioSource audioSource;


	void Start () {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        //left or right paddle
        if ((col.gameObject.name == "LeftPaddle") ||
            (col.gameObject.name == "RightPaddle"))
        {
            HandlePaddleHit(col);
        }

        //up or bottom wall
        if ((col.gameObject.name == "WallTop") ||
            (col.gameObject.name == "WallBottom"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.wallBloop);
        }
        //left or right goal
        if ((col.gameObject.name == "LeftGoal") ||
        (col.gameObject.name == "RightGoal"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBloop);

            if (col.gameObject.name == "LeftGoal")
            {

                increaseTextUIScore("RightScoreUI");

            }
            else if (col.gameObject.name == "RightGoal")
            {

                increaseTextUIScore("LeftScoreUI");

            }

            transform.position = new Vector2(0, 0);
        }
    }
    void HandlePaddleHit(Collision2D col)
    {
        float y = BallHitPaddleHitWhere(transform.position,
            col.transform.position,
            col.collider.bounds.size.y);

        Vector2 dir = new Vector2();

        if (col.gameObject.name == "LeftPaddle")
        {
            dir = new Vector2(1, y).normalized;
        }

        if (col.gameObject.name == "RightPaddle")
        {
            dir = new Vector2(-1, y).normalized;
        }

            rigidBody.velocity = dir * speed;

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);

    }
    float BallHitPaddleHitWhere(Vector2 ball, Vector2 paddle, float paddleht)
    {
        return (ball.y - paddle.y) / paddleht;
    }

    void increaseTextUIScore(string textUIName)
    {

        // Find the matching text UI component
        var textUIComp = GameObject.Find(textUIName)
            .GetComponent<Text>();

        // Get the string stored in it and convert to an int
        int score = int.Parse(textUIComp.text);

        // Increment the score
        score++;

        // Convert the score to a string and update the UI
        textUIComp.text = score.ToString();
    }
}
