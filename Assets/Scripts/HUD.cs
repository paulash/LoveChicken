using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void OnTalkComplete();

public class HUD : MonoBehaviour
{
    public Text[] responseText;
    public Text chickenText;
    public GameObject chiefPanel;
    public GameObject chickenPanel;

    public float textTypeSpeed = 0.1f;

    bool talking = false;

    // Start is called before the first frame update
    void Awake()
    {
        chickenPanel.SetActive(false);
        chiefPanel.SetActive(false);
    }

    public void AskQuestion(Question question, OnTalkComplete onComplete)
    {
        chickenPanel.SetActive(true);

        if (talking) return;
        ChickenTalk(question.questionText, onComplete);

        for (int i = 0; i < responseText.Length; i++)
        {
            string response = question.GetResponseIndex(i);
            responseText[i].text = response;

            responseText[i].transform.parent.gameObject.SetActive(response != "");
        }
    }

    public void ChickenTalk(string text, OnTalkComplete onComplete)
    {
        talking = true;
        chiefPanel.SetActive(false);
        chickenText.text = "";

        StartCoroutine(TypeText(text, onComplete));
    }

    IEnumerator TypeText(string remainingText, OnTalkComplete onComplete)
    {
        yield return new WaitForSeconds(textTypeSpeed);
        string characterTxt = remainingText[0] + "";
        remainingText = remainingText.Remove(0, 1);

        chickenText.text += characterTxt;
        if (remainingText.Length > 0)
            StartCoroutine(TypeText(remainingText, onComplete));
        else
        {
            talking = false;
            chiefPanel.SetActive(true);

            if (onComplete != null)
                onComplete();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickedResponse(int index)
    {
        GameMode.Singleton.OnResponseSelected(index);
    }
}
