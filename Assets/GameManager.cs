using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] public List<GameObject> enemies;

    public float cinematicTime;
    private Camera _camera;

    public CanvasGroup mainMenu;
    public CanvasGroup pauseMenu;
    public CanvasGroup victoryScreen;
    public CanvasGroup defeatScreen;

    public TextMeshProUGUI counter;

    public GameObject explosionPrefab;


    // Start is called before the first frame update

    private void Awake()
    {
        _camera = Camera.main;
        if (instance != null)
        {
            DestroyImmediate(this);
        }

        instance = this;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void AddEnemy(GameObject go)
    {
        enemies.Add(go);
    }

    public void RemoveEnemy(GameObject go)
    {
        enemies.Remove(go);
        var temp = Instantiate(explosionPrefab, go.transform.position, Quaternion.identity);
        temp.transform.localScale = go.transform.localScale * 0.25f;
        Destroy(temp, 3f);
        Destroy(go);
        
        counter.text = allEnemies - enemies.Count + " / " + allEnemies;

        if (enemies.Count == 0)
        {
            victoryScreen.alpha = 1;
            victoryScreen.interactable = true;
            victoryScreen.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }


    public void Lose()
    {
        defeatScreen.alpha = 1;
        defeatScreen.interactable = true;
        defeatScreen.blocksRaycasts = true;
        Time.timeScale = 0;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        if (mainMenu.interactable) return;
        
        Plane.instance.paused = !Plane.instance.paused;

        if (Plane.instance.paused)
        {
            Time.timeScale = 0;
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.alpha = 0;
            pauseMenu.interactable = false;
            pauseMenu.blocksRaycasts = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    private int allEnemies;
    IEnumerator StartGameCoroutine()
    {
        var time = cinematicTime;
        var basePos = _camera.transform.position;
        var baseRot = _camera.transform.rotation;
        
        while (time > 0)
        {
            time -= Time.unscaledDeltaTime;

            var lerp = 1 - ((cinematicTime - time) / cinematicTime);
            _camera.transform.position = Vector3.Lerp(Plane.instance.refCam.position,
                basePos, lerp);
            _camera.transform.rotation = Quaternion.Lerp(Plane.instance.refCam.rotation, baseRot,lerp);
            mainMenu.alpha = Mathf.Lerp(0, 1, lerp);
            yield return null;
        }
        
        Time.timeScale = 1;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;
        Plane.instance.paused = false;

        allEnemies = enemies.Count;
        counter.text = "0 / " + enemies.Count;
        yield return null;
    }
}
