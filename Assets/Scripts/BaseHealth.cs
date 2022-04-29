using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int health = 2;
    [SerializeField] Text healthText;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip damageSFX;

    int score = 0;
    private void Start()
    {
        healthText.text = health.ToString();
        scoreText.text = score.ToString();

    }

    public void GetHealth()
    {
        //AudioSource.PlayClipAtPoint(damageSFX, transform.position);
        GetComponent<AudioSource>().PlayOneShot(damageSFX);
        health--;
        healthText.text = health.ToString();
        if (health <= 0)
            NewGame();
    }
    private void NewGame()
    {
        SceneManager.LoadScene(0);
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }
}
