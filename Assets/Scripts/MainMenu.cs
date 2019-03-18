using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject topMenu;
    public GameObject listMenu;

    public AudioClip bawk;
    public AudioClip[] baks;

    AudioSource audioSource;
    float pitch;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        pitch = audioSource.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(GameObject chickenPrefab)
    {


        audioSource.clip = bawk;
        audioSource.Play();

        GameMode.chickenPrefab = chickenPrefab;

        GameObject.DontDestroyOnLoad(gameObject);
        topMenu.SetActive(false);
        listMenu.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
        GameObject.Destroy(gameObject, 0.5f);
    }



    public void ShowTop()
    {
        topMenu.SetActive(true);
        listMenu.SetActive(false);
        PlaySound();
    }

    public void ShowList()
    {
        topMenu.SetActive(false);
        listMenu.SetActive(true);
        PlaySound();
    }
    

    void PlaySound()
    {
        audioSource.pitch = pitch + Random.Range(-0.2f, 0.2f);
        audioSource.clip = baks[Random.Range(0, baks.Length)];
        audioSource.Play();
    }
}
