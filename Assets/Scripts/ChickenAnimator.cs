using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ChickenMood
{
    Passive,
    Shocked,
    Angry,
    Happy,
    Interested,
    Depressed,
    Disgusted,
    Count
}

public class ChickenAnimator : MonoBehaviour
{
    public Transform mouth;

    public Sprite[] moods = new Sprite[(int)ChickenMood.Count];

    public SpriteRenderer moodRenderer;
    public SpriteRenderer eyeRenderer;
    public SpriteRenderer mouthRenderer;
    public SpriteRenderer headRenderer;

    public Sprite eyeClosed;
    public Sprite eyeOpened;

    public float maxHeadRotation = 0.1f;

    bool resetMount = false;
    float nextMouthMove = 0;

    bool resetBlink = false;
    float nextBlinkTime = 0;

    public bool talking = true;
    public ChickenMood currentMood;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        headRenderer.transform.eulerAngles = new Vector3(0, 0, Mathf.Pow(Mathf.Sin(Time.time), 2));

        if (talking)
        {
            headRenderer.enabled = true;
            mouthRenderer.enabled = true;
            eyeRenderer.enabled = true;
            moodRenderer.enabled = false;

            nextMouthMove -= Time.deltaTime;
            if (nextMouthMove <= 0)
            {
                resetMount = !resetMount;
                if (resetMount)
                    mouth.localPosition = Vector2.zero;
                else
                {
                    float randomMouthPosition = Random.Range(0.02f, 0.04f);
                    randomMouthPosition -= randomMouthPosition % 0.01f;

                    mouth.localPosition = new Vector2(0, 1) * -randomMouthPosition;
                }
                nextMouthMove = Random.Range(0.1f, 0.2f);
            }

            nextBlinkTime -= Time.deltaTime;
            if (nextBlinkTime <= 0)
            {
                resetBlink = !resetBlink;
                eyeRenderer.sprite = resetBlink ? eyeClosed : eyeOpened;

                if (resetBlink)
                    nextBlinkTime = 0.1f;
                else
                    nextBlinkTime = Random.Range(0.6f, 1f);
            }
        }
        else
        {
            headRenderer.enabled = false;
            mouthRenderer.enabled = false;
            eyeRenderer.enabled = false;
            moodRenderer.enabled = true;
            moodRenderer.sprite = moods[(int)currentMood];
        }
    }
}
