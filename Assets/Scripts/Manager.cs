using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject bloodScreen;
    public RawImage healthImage;
    public float bloodScreenTime = 1f;
    public string targetLayer = "Ball";
    public int health = 10;
    public Texture2D[] healthTextures;
    
    public GameObject gameOver;

    public CreateBallScript createBallScript;
    public Text scoreText;
    public Text pointsText;


    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        pointsText.text = "Score: " + createBallScript.points;
    }

    public void ResetGame()
    {
        healthImage.texture = healthTextures[healthTextures.Length - 1];
        healthImage.enabled = true;

        health = 10;

        bloodScreen.SetActive(false);

        gameOver.SetActive(false);

        createBallScript.StartSpawner();
    }

    /// <summary>
    /// Show a bloodscreen for n seconds
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private IEnumerator ShowBloodscreen(float seconds)
    {
        if (bloodScreen != null)
        {
            bloodScreen.SetActive(true);
            yield return new WaitForSeconds(seconds);
            if (health > 0) // if the player has been hit again while we wait, don't disable the bloodscreen
                bloodScreen.SetActive(false);
            Debug.Log("disable bloodscreen");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.tag == "Ball")
        {
            health--;

            if (health > 0)
            {
                StartCoroutine(ShowBloodscreen(bloodScreenTime));
                int index = health - 1;
                Debug.Log(health);
                Debug.Log(index);
                healthImage.texture = healthTextures[index];
            }

            if (health <= 0 && gameOver != null)
            {
                // Set the health to 0 to avoid negative health
                health = 0;

                healthImage.enabled = false;

                gameOver.SetActive(true);
                bloodScreen.SetActive(true);
                createBallScript.StopSpawner();
                scoreText.text = "Score: " + createBallScript.points;

                Debug.Log("game over");
            }
            // Destroy the ball after it hit us
            Destroy(collision.gameObject);
        }
    }
}