using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] public List<GameObject> enemies;

    public float cinematicTime;
    private Camera _camera;

    public CanvasGroup mainMenu;
    public CanvasGroup pauseMenu;


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

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void Pause()
    {
        if (mainMenu.interactable) return;
        
        DebugController.instance.paused = !DebugController.instance.paused;

        if (DebugController.instance.paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
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

    IEnumerator StartGameCoroutine()
    {
        var time = cinematicTime;
        var basePos = _camera.transform.position;
        var baseRot = _camera.transform.rotation;
        
        while (time > 0)
        {
            time -= Time.unscaledDeltaTime;

            var lerp = 1 - ((cinematicTime - time) / cinematicTime);
            _camera.transform.position = Vector3.Lerp(DebugController.instance.refCam.position,
                basePos, lerp);
            _camera.transform.rotation = Quaternion.Lerp(DebugController.instance.refCam.rotation, baseRot,lerp);
            mainMenu.alpha = Mathf.Lerp(0, 1, lerp);
            yield return null;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;
        DebugController.instance.paused = false;
        yield return null;
    }
}
