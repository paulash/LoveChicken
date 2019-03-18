using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode Singleton { get; private set; }
    public static GameObject chickenPrefab;

    GameObject chickenGO;
    ChickenBrain chickenBrain;
    ChickenAnimator chickenAnimator;
    ChickenSound chickenSound;

    public MoodBackground moodBackground;
    public HUD hud;

    bool winner = false;
    
    void Awake()
    {
        if (Singleton != null)
            GameObject.Destroy(Singleton.gameObject);

        Singleton = this;
        GameMode.DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        SpawnChicken(GameMode.chickenPrefab);
    }

    public void SpawnChicken(GameObject chickenPrefab)
    {
        chickenGO = GameObject.Instantiate(chickenPrefab, new Vector3(0.2f, 0.1f, 0), Quaternion.identity);
        chickenGO.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        chickenBrain = chickenGO.GetComponent<ChickenBrain>();
        chickenAnimator = chickenGO.GetComponent<ChickenAnimator>();
        chickenSound = chickenGO.GetComponent<ChickenSound>();
    }

    public void StartQuestion(Question question)
    {
        chickenAnimator.talking = true;
        chickenSound.StartBawk();

        hud.AskQuestion(question, OnTalkComplete);
    }

    void OnTalkComplete()
    {
        chickenSound.StopBawk();
        chickenAnimator.talking = false;
    }

    public void OnResponseSelected(int selected)
    {
        chickenBrain.OnResponseSelected(selected);
    }

    public void CompleteGame()
    {
        hud.chiefPanel.SetActive(false);

        float interest = 0, happiness = 0;
        chickenBrain.GetGameResult(ref interest, ref happiness);

        chickenAnimator.talking = true;
        chickenSound.StartBawk();

        if (interest > 0.5f || happiness > 0.5f) // winner!
        {
            hud.ChickenTalk(chickenBrain.WinnerResponse, OnTalkCompleteGame);
            winner = true;
        }
        else
            hud.ChickenTalk(chickenBrain.LoserResponse, OnTalkCompleteGame);
    }

    void OnTalkCompleteGame()
    {
        hud.chiefPanel.SetActive(false);
        chickenAnimator.talking = false;
        chickenSound.StopBawk();

        StartCoroutine(WaitComplete());
    }

    IEnumerator WaitComplete()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(winner ? "Winner" : "GameOver");
    }
}
