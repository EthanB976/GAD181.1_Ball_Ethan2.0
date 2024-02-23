using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float elapsedTime = 30;
    [SerializeField] private int addTime = 10 ;
    void Update()
    {
        elapsedTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(2);
        }

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        elapsedTime += addTime;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

    }
}
