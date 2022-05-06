using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    [Header("Images")]
    [SerializeField]
    private Image drawPercentageForground;
    [SerializeField]
    private Image drawResultsForground;

    [Header("Sliders")]
    [SerializeField]
    private Slider progressSlider;

    [Header("Texts")]
    [SerializeField]
    private Text gemResultsText;
    [SerializeField]
    private Text drawResultsText;
    [SerializeField]
    private Text playerSortResultText;
    [SerializeField]
    private Text playerSortText;
    [SerializeField]
    private Text drawTimeText;
    [SerializeField]
    private Text drawPercentageText;
    [SerializeField]
    private Text gemPointText;
    [SerializeField]
    private Text countdownText;

    [Header("Objects")]
    [SerializeField]
    private GameObject gemDisplay;
    [SerializeField]
    private GameObject sortDisplay;
    [SerializeField]
    private GameObject tutorial;
    [SerializeField]
    private GameObject drawPercentage;
    [SerializeField]
    private GameObject resultsMenu;

    private int gemPoint;

    bool isTouchedTheScreen;
    public static UserInterfaceManager userInterfaceManager;

    private void Awake()
    {
        if (userInterfaceManager == null)
        {
            userInterfaceManager = this;
        }
        drawPercentageText.text = 0 + "%";
        drawPercentageForground.fillAmount = 0;
    }

    void Update()
    {
        if (!isTouchedTheScreen)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isTouchedTheScreen = true;
                tutorial.SetActive(false);
                countdownText.gameObject.SetActive(true);
                StartCoroutine(GameStartCoroutine());
            }
        }
        else
        {
            ShowPlayerSort();
        }
    }

    private void ShowPlayerSort()
    {
        playerSortText.text = SortingManager.sortingManager.PlayerSort().ToString();
        playerSortResultText.text = SortingManager.sortingManager.PlayerSort().ToString();
    }

    IEnumerator GameStartCoroutine()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Start";
        PlayerMovement.playerMovement.CanMove();
        OpponentManager.opponentManager.OpponentsCanMove();
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }
    public void IncreaseGemPoint()
    {
        gemPoint++;
        ShowGemPoint();
    }

    private void ShowGemPoint()
    {
        gemPointText.text = gemPoint.ToString();
        gemResultsText.text = gemPoint.ToString();
    }

    public int GetGemPoint()
    {
        return gemPoint;
    }

    public void VisibleDrawPercentage()
    {
        drawPercentage.SetActive(true);
    }
    public void HideGameUI()
    {
        progressSlider.gameObject.SetActive(false);
        gemDisplay.gameObject.SetActive(false);
        sortDisplay.gameObject.SetActive(false);
    }
    public void ShowDrawPercentage(float drawPercentage)
    {
        drawPercentageText.text = (int)drawPercentage + "%";
        drawPercentageForground.fillAmount = drawPercentage / 100; 

        drawResultsText.text = (int)drawPercentage + "%";
        drawResultsForground.fillAmount = drawPercentage / 100;
    }
    public void ShowDrawTime(float drawTime)
    {
        drawTimeText.text = (int)(drawTime + 0.25f) + " sn";
    }
    public void ShowProgress(float progress)
    {
        progressSlider.value = progress;
    }

    public void ShowResults()
    {
        resultsMenu.SetActive(true);
    }
}
