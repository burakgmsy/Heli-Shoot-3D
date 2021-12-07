using System;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    #region UIManager
    [Header("UI Manager")]

    //Shoot button component
    [SerializeField] private GameObject ShootAllImage;
    [SerializeField] private GameObject shootButtonObj;
    [SerializeField] private Image shootInImage;
    [SerializeField] private Button shootButton;

    //Move button component

    [SerializeField] private GameObject MoveAllImage;
    [SerializeField] private GameObject MoveUpButtonObj;
    [SerializeField] private GameObject MoveDownButtonObj;
    [SerializeField] private GameObject moveUp;
    [SerializeField] private GameObject moveDown;

    //TapScreen component
    [SerializeField] private GameObject tapScreen;
    [SerializeField] private GameObject tapfastly;
    [SerializeField] private Image tapfastlyImage;
    //OtherScreens
    [SerializeField] private GameObject inGame;
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    #endregion

    #region InGameValues
    [Header("In Game Values")]
    [SerializeField] private float speed = 0;
    [SerializeField] private float speedYAxis = 4f;
    [SerializeField] private int enemyDamage = 25;
    [SerializeField] private int heliDamage = 25;
    [SerializeField] private float gemScore = 0;
    [SerializeField] private float finalGemScore = 0;
    [SerializeField] private float ButtonTimerCount;

    #endregion

    #region ScoreStateManager
    [Header("ScoreState Manager")]
    [SerializeField] private float maxScore = 10;
    [SerializeField] private float currentScore;
    [SerializeField] private float scoreIncrease;
    [SerializeField] private float scoreDecrease;
    [SerializeField] private bool isTab;

    #endregion

    #region CapsuleVariable
    public float CurrentScore
    {
        get { return currentScore; }
        set { currentScore = value; }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public float SpeedYAxis
    {
        get { return speedYAxis; }
        set { speedYAxis = value; }
    }
    public int HeliDamage
    {
        get { return heliDamage; }
        set { heliDamage = value; }
    }
    public int EnemyDamage
    {
        get { return enemyDamage; }
        set { enemyDamage = value; }
    }
    #endregion
    public event Action<float> OnScorePctChanged = delegate { };

    Tween tween;
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1);
        ShotButtonTimer(ButtonTimerCount);
        if (isTab && Input.GetMouseButtonDown(0) && currentScore < 10)
        {
            ModifyScore(scoreIncrease);
        }
        if (isTab && currentScore > 0)
        {
            ModifyScore(scoreDecrease);
        }
    }
    public void ModifyScore(float amount)
    {
        currentScore += amount;
        float currentScorePct = (float)currentScore / (float)maxScore;
        OnScorePctChanged(currentScorePct);
    }
    private void OnEnable()
    {
        currentScore = 0;
    }
    public void AddGem()
    {
        gemScore += 3;
    }

    public int FinalGemScore()
    {
        return (int)gemScore * (int)currentScore;
    }
    public void FinalScoreText()
    {
        var score = (int)FinalGemScore();
        //show final score text and panel
    }


    public void StartGame()
    {
        MoveUI(true);
        inGame.SetActive(true);
        mainScreen.SetActive(false);
        InputHandler.Instance.isGaming = true;
        speed = 4f;
    }
    public void LoseGame()
    {
        MoveUI(false);
        inGame.SetActive(false);
        loseScreen.SetActive(true);
        InputHandler.Instance.isGaming = false;
        ShootAllImage.SetActive(false);
        speed = 0;
    }
    public void WinGame()
    {
        MoveUI(false);
        inGame.SetActive(false);
        winScreen.SetActive(true);
        InputHandler.Instance.isGaming = false;
        ShootAllImage.SetActive(false);
    }


    public void OpenTabScreen()
    {
        HelicopterController.Instance.isDown = false;
        HelicopterController.Instance.isUp = false;
        MoveUI(false);
        inGame.SetActive(false);
        tapfastly.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo);
        tapfastlyImage.DOColor(Color.red, 0.7f).SetLoops(-1, LoopType.Yoyo);
        InputHandler.Instance.isGaming = false;
        ShootAllImage.SetActive(false);
        tapScreen.SetActive(true);
        isTab = true;
    }
    public void CloseTabScreen()
    {
        HelicopterController.Instance.isUp = true;
        isTab = false;
        tapScreen.SetActive(false);
    }


    public void ShotButtonTimer(float value)
    {
        if (shootInImage.fillAmount == 1)
        {
            shootButton.interactable = true;
        }
        if (shootInImage.fillAmount < 1)
        {
            shootButton.interactable = false;
        }
        shootInImage.fillAmount += 0.1f * Time.deltaTime * value;
    }

    public void SetButtonFillAmount()
    {
        shootInImage.fillAmount = 0;
    }

    public void ShootScreen(bool value)
    {
        ShootAllImage.SetActive(value);
        shootButtonObj.SetActive(value);
        //shootButton activied
    }

    public void MoveDownUI(bool value)
    {
        moveDown.SetActive(value);
        moveUp.SetActive(!value);

    }
    public void MoveUpUI(bool value)
    {
        moveUp.SetActive(value);
        moveDown.SetActive(!value);
    }
    public void MoveUpDownFree(bool value)
    {
        moveUp.SetActive(value);
        moveDown.SetActive(value);
    }
    public void MoveUI(bool value)
    {
        MoveAllImage.gameObject.SetActive(value);
        MoveDownButtonObj.gameObject.SetActive(value);
        MoveUpButtonObj.gameObject.SetActive(value);
    }
    public void LoadScreen()
    {
        SceneManager.LoadScene("GameScene");
    }
}

