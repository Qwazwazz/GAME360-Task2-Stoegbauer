
using UnityEngine;
using UnityEngine.UI;
using TMPro; //Namesapce for textmeshpro
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManagerEx : MonoBehaviour
{
    // Singleton instance
    public static GameManagerEx Instance { get; private set; }

    [Header("Game Stats")]
    public int score = 0;//score is calculated
    public int lives = 3;
    public int enemiesKilled = 0;
    public int bossHP = 150;
    public enum gameState { Menu, Playing, GameOver };
    public gameState state;

    [Header("UI References")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text enemiesKilledText;
    public TMP_Text gameWinLose;
    public TMP_Text bossBar;
    public GameObject gameOverPanel;
    bool stateComplete;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManagerExs
        }
    }

    void Update()
    {
        if (stateComplete)
        {
            SelectState();
        }
        UpdateState();
    }

    void SelectState()
    {
        stateComplete = false;
        if (Time.timeScale == 0f)
        {
            state = gameState.GameOver;
        }
        else
        {
            state = gameState.Playing;
            StartPlaying();
        }

    }

    void UpdateState()
    {
        switch (state)
        {
            case gameState.Menu:
                break;
            case gameState.Playing:
                break;
            case gameState.GameOver:
                break;
        }
    }

    void StartPlaying()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        state = gameState.Playing;
        if (bossBar) bossBar.text = "";
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        state = gameState.Menu;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshUIReferences();
        UpdateUI();
    }

    private void Start()
    {
        //RefreshUIReferences();
        //UpdateUI();

    }

    private void RefreshUIReferences()
    {

        scoreText = GameObject.Find("Score")?.GetComponent<TMP_Text>();
        livesText = GameObject.Find("Lives")?.GetComponent<TMP_Text>();
        bossBar = GameObject.Find("BossHP")?.GetComponent<TMP_Text>();
        enemiesKilledText = GameObject.Find("EnemiesKilled")?.GetComponent<TMP_Text>();
        gameOverPanel = GameObject.Find("GameEndPanel");
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score increased by {points}. Total: {score}");
        UpdateUI();
    }


    public void LoseLife()
    {
        lives--;
        Debug.Log($"Life lost! Lives remaining: {lives}");
        UpdateUI();

        if (lives <= 0)
        {
            if (gameWinLose && lives <= 0 && bossHP >= 1) gameWinLose.text = "You Lose... \n" + "Final Score: " + score;
            GameOver();
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        AddScore(25); // 100 points per enemy
        Debug.Log($"Enemy killed! Total enemies defeated: {enemiesKilled}");
    }

    public void BossDamaged(int health)
    {
        bossHP = health;
        if (bossBar) bossBar.text = "Boss HP: " + bossHP + "/150";
    }

    public void BossKilled()
    {
        if (gameWinLose && lives >= 1 && bossHP <= 0) gameWinLose.text = "You Win! \n" + "Final Score: " + score;
        GameOver();
    }

    public void CollectiblePickedUp(int value)
    {
        AddScore(value);
        lives += 1;
        UpdateUI();
        Debug.Log($"Collectible picked up worth {value} points!");
    }

    private void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (livesText) livesText.text = "Lives: " + lives;
        if (enemiesKilledText) enemiesKilledText.text = "Enemies: " + enemiesKilled;
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!");
        if (gameWinLose && lives <= 0 && bossHP >= 1) gameWinLose.text = "You Lose... \n" + "Final Score: " + score;
        if (gameWinLose && lives >= 1 && bossHP <= 0) gameWinLose.text = "You Win! \n" + "Final Score: " + score;
        if (bossBar) bossBar.text = " ";
        if (gameOverPanel) gameOverPanel.SetActive(true);
        state = gameState.GameOver;
        Time.timeScale = 0f; // Pause the game
    }

    public void reloadGame()
    {
        //SceneManager.LoadScene("Delete");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitGame()
    {
        Debug.Log("Quitting");
        RestartGame();
        state = gameState.Menu;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Debug.Log("Restarting");
        Time.timeScale = 1f;

        // Reset all game state
        score = 0;
        lives = 3;
        enemiesKilled = 0;
        bossHP = 150;
        state = gameState.Playing;
        if (bossBar) bossBar.text = " ";
        if(gameWinLose) gameWinLose.text = "You Lose... \n" + "Final Score: " + score;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }

    private void DestroyAllGameObjects()
    {
        // Destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Destroy all bullets
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        // Destroy all collectibles
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject collectible in collectibles)
        {
            Destroy(collectible);
        }
    }
}
