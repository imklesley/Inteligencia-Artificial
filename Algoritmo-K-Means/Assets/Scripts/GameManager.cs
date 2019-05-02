using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject kMeans;
    public GameObject instancias;

    // Botões
    public Button playBtn;
    public Button resetBtn;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Play()
    {
        playBtn.gameObject.SetActive(false);
        instancias.gameObject.SetActive(true);
        GetComponent<AudioSource>().enabled = true;

        kMeans.GetComponent<KMeans>().Executar();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
