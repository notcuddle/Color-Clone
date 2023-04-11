using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{


    [SerializeField] private float verticalForce = 400f;
    [SerializeField] private Color orangeColor;
    [SerializeField] private Color violetColor;
    [SerializeField] private Color cyanColor;
    [SerializeField] private Color pinkColor;
    [SerializeField] private ParticleSystem playerParticles;

    private string currentColor;
    private float restartDelay = 1f;


    Rigidbody2D playerRb;

    SpriteRenderer playerSr;




    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

        playerSr = GetComponent<SpriteRenderer>();
        ChangeColor();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up *  verticalForce);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("ColorChanger"))
        {
            ChangeColor();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            AnimationParticlePlayer();
            Invoke("LoadNextScene", restartDelay);
            return;
        }

        if(!collision.gameObject.CompareTag(currentColor))
        {
            AnimationParticlePlayer();
            Invoke("RestartScene",restartDelay);
        }
    }

    void RestartScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1);
    }

    void AnimationParticlePlayer()
    {
        gameObject.SetActive(false);
        Instantiate(playerParticles, transform.position, Quaternion.identity);
    }

    void ChangeColor()
    {
        int randomNumber = UnityEngine.Random.Range(0,4);
        if(randomNumber == 0)
        {
            playerSr.color = orangeColor;
            currentColor = "Orange";
        }
        else if(randomNumber == 1)
        {
            playerSr.color = violetColor;
            currentColor = "Violet";

        }
        else if(randomNumber == 2)
        {
            playerSr.color = cyanColor;
            currentColor = "Cyan";

        }
        else if (randomNumber == 3)
        {
            playerSr.color = pinkColor;
            currentColor = "Pink";
        }
    }
}
