using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGameOver { get; private set; }

    public TextMeshProUGUI gameOver;
    public GameObject Enemy;
    public GameObject powerItem;
    private float ItemTimer = 10f;
    private float iTimer = 0f;
    private float MonsterTimer = 2f;
    private float mTimer = 0f;

    public List<Transform> point = new List<Transform>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("GameManager instance already exists, destroy this one");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        iTimer = ItemTimer;
        mTimer = MonsterTimer;
        IsGameOver = false;
        gameOver.enabled = false;
    }

    private void Update()
    {
        CreateItem();
        CreateEnemey();

        if(IsGameOver)
        {
            Restart();
        }
    }

    private void CreateItem()
    {
        iTimer -= Time.deltaTime;
        if(iTimer < 0f)
        {
            iTimer = ItemTimer;
            var pos = transform.position;
            pos.x = Random.Range(-9f, 9f);
            Instantiate(powerItem, pos, Quaternion.identity);
        }
    }

    private void CreateEnemey()
    {
        mTimer -= Time.deltaTime;
        if(mTimer < 0f)
        {
            StartCoroutine(CreateManyEnemy());

            mTimer = ItemTimer;
        }
    }

    IEnumerator CreateManyEnemy()
    {
        Vector3 start = point[Random.Range(1, point.Count-1)].transform.position;
        Vector3 target;

        while (true)
        {
            target = point[Random.Range(1, point.Count-1)].transform.position;
            if (start != target) break;
        }

        for (int i = 0; i <= 6; i++)
        {
            var obj = Instantiate(Enemy, start, Quaternion.identity);
            obj.GetComponent<EnemyController>().SetTarget(target, point[0].transform.position);

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOver.enabled = true;
    }

    private void Restart()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.N))        
        {
            Application.Quit();
        }
    }
}