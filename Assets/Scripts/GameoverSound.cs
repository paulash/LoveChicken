using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverSound : MonoBehaviour
{
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayClip());
    }

    IEnumerator PlayClip()
    {
        yield return new WaitForSeconds(2.8f);
        if (audioClip != null)
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);

        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
