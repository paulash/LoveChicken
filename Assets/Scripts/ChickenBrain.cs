using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChickenBrain : MonoBehaviour
{
    public Question[] questions;
    int questionIndex = 0;

    ChickenAnimator chickenAnimator;
    ChickenSound chickenSound;

    public string WinnerResponse = "";
    public string LoserResponse = "";

    public Question CurrentQuestion
    {
        get
        {
            return questions[questionIndex];
        }
    }

    // highest value between both direction determines feedback.
    float interest = 0; // -1 to 1 
    float happiness = 0; // -1 to 1

    private void Awake()
    {
        chickenAnimator = GetComponent<ChickenAnimator>();
        chickenSound = GetComponent<ChickenSound>();

        List<Question> shuffledQuestions = new List<Question>(questions);
        Shuffle<Question>(shuffledQuestions);
        questions = shuffledQuestions.ToArray();
    }

    private System.Random rng = new System.Random();

    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AskQuestion();
    }

    void AskQuestion()
    {
        GameMode.Singleton.StartQuestion(CurrentQuestion);
    }

    void UpdateMood()
    {
        if (Mathf.Abs(interest) > Mathf.Abs(happiness) && Mathf.Abs(interest) >= 0.5f && happiness < 0.5f)
            chickenAnimator.currentMood = interest > 0 ? ChickenMood.Interested : ChickenMood.Disgusted;
        else if (Mathf.Abs(happiness) >= 0.5f) // happy
            chickenAnimator.currentMood = happiness > 0 ? ChickenMood.Happy : ChickenMood.Angry;
        else
            chickenAnimator.currentMood = ChickenMood.Passive;
    }

    public void OnResponseSelected(int index)
    {
        float interestMod = CurrentQuestion.GetInterestMod(index);
        float happyMod = CurrentQuestion.GetHappyMod(index);

        interest = Mathf.Clamp(interest + interestMod, -1, 1);
        happiness = Mathf.Clamp(happiness + happyMod, -1, 1);
        
        GameMode.Singleton.moodBackground.UpdateMood(chickenAnimator.currentMood);

        if (questionIndex == questions.Length-1)
        {
            StopAllCoroutines();
            GameMode.Singleton.CompleteGame();
        }
        else
            StartCoroutine(FlashResponse(interestMod, happyMod));
    }

    IEnumerator FlashResponse(float interestMod, float happyMod)
    {
        if ((interest - interestMod) > 0.5f && interestMod < 0)
        {
            chickenAnimator.currentMood = ChickenMood.Shocked;
            chickenSound.SingleBawk(0.4f);
            yield return new WaitForSeconds(0.1f);
        }
        else if (happyMod > 0.5f && happyMod < 0)
        {
            chickenAnimator.currentMood = ChickenMood.Shocked;
            chickenSound.SingleBawk(0.4f);
            yield return new WaitForSeconds(0.1f);
        }
        else if (interestMod > 0)
            chickenAnimator.currentMood = ChickenMood.Interested;
        else if (interestMod < 0)
            chickenAnimator.currentMood = ChickenMood.Disgusted;
        else if (happyMod > 0)
            chickenAnimator.currentMood = ChickenMood.Happy;
        else if (happyMod < 0)
            chickenAnimator.currentMood = ChickenMood.Angry;

        yield return new WaitForSeconds(0.333f);
        UpdateMood();

        questionIndex++;
        if (questionIndex >= questions.Length)
            questionIndex = 0;

        AskQuestion();
    }

    public void GetGameResult(ref float interest, ref float happiness)
    {
        interest = this.interest;
        happiness = this.happiness;
    }
}
