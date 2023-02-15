using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 游戏进行控制器，主要用来控制敌人的生成和血量变化
/// </summary>
public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject explosion;
    public Text playerHealthText;
    public Text scroeText;
    public GameObject menu;

    // 单例
    private static GameController _instance;
    private GameObject _explosion;
    private int score = 0;
    private Health _playerHealth;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("Error");
        }
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = player.GetComponent<Health>();
        playerHealthText.text = _playerHealth.CurHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void OnDamage(Health health)
    {
        if (health == _playerHealth)
        {
            playerHealthText.text = health.CurHealth.ToString();
        }
    }

    public void OnDeath(GameObject go)
    {
        if (go == player)
        {
            player.SetActive(false);
            Time.timeScale = 0;
            return;
        }

        _explosion = Instantiate(explosion);
        _explosion.transform.position = go.transform.position;
        score++;
        scroeText.text = score.ToString();
        go.SetActive(false);
    }
    public void OnPause()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnResume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnRestart()
    {
        SceneManager.LoadScene("Picture");
        Time.timeScale = 1f;
    }
    public void ReturnTitle()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
