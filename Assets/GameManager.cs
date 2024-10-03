using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private List<GameObject> enemies;

    public float cinematicTime;
    
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }

        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEnemy(GameObject go)
    {
        enemies.Add(go);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator StartGameCoroutine()
    {
        var time = cinematicTime;
        while (time > 0)
        {
            time -= Time.unscaledDeltaTime;

            yield return new WaitForFixedUpdate();
            Camera.main.transform.position = Vector3.Lerp(DebugController.instance.refCam.position,
                Camera.main.transform.position, 1);
        }
        yield return null;
    }
}
