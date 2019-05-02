using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Botões
    public Button playBtn;
    public Button resetBtn;

    public BFS bfs;
    public DFS dfs;

    public Timer timerBfs;
    public Timer timerDfs;

    public Slider timeSlider;

    public static GameManager instance;

    public bool bfsHasFound = false;
    public bool dfsHasFound = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeSlider.onValueChanged.AddListener(delegate { ChangeTime(); });
    }

    private void Update()
    {
        if (bfsHasFound && dfsHasFound)
            End();
    }

    public void ChangeTime()
    {
        Time.timeScale = timeSlider.value;
    }

    public void BFSHasFound()
    {
        bfsHasFound = true;
        timerBfs.ClickStop();
    }

    public void DFSHasFound()
    {
        dfsHasFound = true;
        timerDfs.ClickStop();
    }

    public void Play()
    {
        playBtn.gameObject.SetActive(false);
        GetComponent<AudioSource>().enabled = true;
    }

    public void PlayBFS()
    {
        bfs.StartBFS();
        timerBfs.ClickPlay();
    }

    public void PlayDFS()
    {
        dfs.StartDFS();
        timerDfs.ClickPlay();
    }

    public void End()
    {
        resetBtn.gameObject.SetActive(true);
        timerBfs.ClickStop();
        timerDfs.ClickStop();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
