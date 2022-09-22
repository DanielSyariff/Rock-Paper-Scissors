using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public RectTransform resultPanel;
    public RectTransform setupPanel;
    public Text Result;
    public TextMeshProUGUI scoreText;
    public Image yourChoice;
    public Image AIChoice;

    private int yourScore;
    private int enemyScore;

    public string[] Choices;
    public Sprite Rock, Paper, Scissors, Interogation;

    [Header("Sound Effect")]
    public AudioSource audioSource;
    public AudioClip sfx_buttonClicked;
    public AudioClip sfx_win;
    public AudioClip sfx_lose;

    public void Play(string myChoice)
    {
        audioSource.PlayOneShot(sfx_buttonClicked);
        string randomChoice = Choices[Random.Range(0, Choices.Length)];

        switch(randomChoice)
        {
            case "Rock":
                switch(myChoice)
                {
                    case "Rock":
                        StartCoroutine(TieCondition());
                        yourChoice.sprite = Rock;
                        break;

                    case "Paper":
                        //Result.text = "The paper covers the rock, you win!";
                        StartCoroutine(WinCondition());
                        yourChoice.sprite = Paper;
                        break;

                    case "Scissors":
                        //Result.text = "The rock destroys the scissors, you lose!";
                        StartCoroutine(LoseCondition());
                        yourChoice.sprite = Scissors;
                        break;
                    
                }

                AIChoice.sprite = Rock;
                StartCoroutine(ShowResult());
                break;

            case "Paper":
                switch (myChoice)
                {
                    case "Rock":
                        //Result.text = "The paper covers the rock, you lose!";
                        StartCoroutine(LoseCondition());
                        yourChoice.sprite = Rock;
                        break;

                    case "Paper":
                        StartCoroutine(TieCondition());
                        yourChoice.sprite = Paper;
                        break;

                    case "Scissors":
                        //Result.text = "The scissors cuts the paper, you win!";
                        StartCoroutine(WinCondition());
                        yourChoice.sprite = Scissors;
                        break;

                }

                AIChoice.sprite = Paper;
                StartCoroutine(ShowResult());
                break;

            case "Scissors":
                switch (myChoice)
                {
                    case "Rock":
                        //Result.text = "The rock destroys the scissors, you win!";
                        StartCoroutine(WinCondition());
                        yourChoice.sprite = Rock;
                        break;

                    case "Paper":
                        //Result.text = "The scissors cuts the paper, you lose!";
                        StartCoroutine(LoseCondition());
                        yourChoice.sprite = Paper;
                        break;

                    case "Scissors":
                        StartCoroutine(TieCondition());
                        yourChoice.sprite = Scissors;
                        break;

                }

                AIChoice.sprite = Scissors;
                StartCoroutine(ShowResult());
                break;

        }
    }

    public IEnumerator WinCondition()
    {
        Result.text = "You Win!";
        yourScore++;
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(sfx_win);
    }

    public IEnumerator LoseCondition()
    {
        Result.text = "You Lose!";
        enemyScore++;
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(sfx_lose);
    }

    public IEnumerator TieCondition()
    {
        Result.text = "Tie!";
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(sfx_win);
    }

    IEnumerator ShowResult()
    {
        scoreText.text = yourScore + " - " + enemyScore;
        yourChoice.transform.localRotation = Quaternion.Euler(yourChoice.transform.localRotation.x, yourChoice.transform.localRotation.y, 90f);
        AIChoice.transform.localRotation = Quaternion.Euler(yourChoice.transform.localRotation.x, yourChoice.transform.localRotation.y, 90f);
        yourChoice.transform.DORotate(Vector3.zero, 0.30f).SetEase(Ease.OutBack);
        AIChoice.transform.DORotate(Vector3.zero, 0.30f).SetEase(Ease.OutBack);
        setupPanel.DOScale(Vector3.zero, 0.15f);//.SetEase(Ease.OutBack);
        yield return new WaitForSeconds(2f);
        resultPanel.DOScale(Vector3.one, 0.30f).SetEase(Ease.OutBack);
    }

    public void playAgain()
    {
        yourChoice.sprite = Interogation;
        AIChoice.sprite = Interogation;
        resultPanel.DOScale(Vector3.zero, 0.15f);//.SetEase(Ease.OutBack);
        setupPanel.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack);
    }

    public void LoadSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
