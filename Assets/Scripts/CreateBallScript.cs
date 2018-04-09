using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBallScript : MonoBehaviour
{
    public Rigidbody prefab;
    public int startWait;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public bool stop;
    public float speed;
    public float speedMost;
    public float speedLeast;
    public Color colorActual;
    public Color[] colors;
    public Text pointsText;

    public int points = 0;

    /// <summary>
    /// Used to instantiate
    /// </summary>
    void Start()
    {
        startWait = 1;
        spawnMostWait = 8f;
        spawnLeastWait = 1f;
        speedMost = 10f;
        speedLeast = 1f;
        colors = new Color[] {Color.red, Color.yellow, Color.white,Color.magenta,
            Color.green, Color.gray, Color.cyan, Color.blue, Color.black};

        StartSpawner();
        
    }

    /// <summary>
    /// Creates a new Ball and throws the ball to the user
    /// </summary>
    private void CreateNewBall()
    {
        TurnToPerson();
        int colorIndex = UnityEngine.Random.Range(0, colors.Length);
        colorActual = colors[colorIndex];

        Rigidbody newObj = Instantiate(prefab);
        newObj.position = transform.position;       
        Renderer renderer = newObj.GetComponent<Renderer>();
        renderer.material.color = colorActual;

        Vector3 pos = Camera.main.transform.position;
        Vector3 dir = (this.transform.position - Camera.main.transform.position).normalized;
        Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);
        if (dir.x == 0 && dir.y == 0 && dir.z == 0)
            dir = Vector3.one;
        speed = UnityEngine.Random.Range(speedLeast, speedMost);
        newObj.AddForce(dir * (-1 * speed));
       
    }
    /// <summary>
    /// Let the balls spawn after a randomly time
    /// </summary>
    /// <returns>IEnumerator for Coroutine</returns>
    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startWait);
        while (!stop)
        {
            CreateNewBall();
            points++;
            yield return new WaitForSeconds(spawnWait);
        }
    }

    public void StartSpawner()
    {
        stop = false;
        points = 0;
        StartCoroutine(Spawner());      //Starts a routine for the spawning balls
    }

    public void StopSpawner()
    {
        stop = true;
    }

    private void TurnToPerson()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    /// <summary>
    /// Updates every frame
    /// </summary>
    void Update ()
    {
        spawnWait = UnityEngine.Random.Range(spawnLeastWait, spawnMostWait);    //calculates a random time that is between the spawnLeastWait and the spawnMostWait -1
    }
}
