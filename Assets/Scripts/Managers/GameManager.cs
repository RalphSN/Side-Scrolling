using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Cleared,
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerHealth;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    GameObject pausedPanel;

    [SerializeField]
    LevelGoal levelGoal;

    [SerializeField]
    GameObject stageClearPanel;
    private GameState currentState = GameState.Playing;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        playerHealth.OnPlayerDied += HandlePlayerDied;
        levelGoal.OnCompleted += HandleLevelCompleted;
        controls.UI.Enable();
        controls.UI.Pause.performed += OnPause;
    }

    void OnDisable()
    {
        playerHealth.OnPlayerDied -= HandlePlayerDied;
        levelGoal.OnCompleted -= HandleLevelCompleted;
        controls.UI.Pause.performed -= OnPause;
        controls.UI.Disable();
    }

    private void HandlePlayerDied()
    {
        if (currentState != GameState.Playing)
            return;
        currentState = GameState.GameOver;
        gameOverPanel.SetActive(true);
    }

    private void HandleLevelCompleted()
    {
        if (currentState != GameState.Playing)
            return;
        currentState = GameState.Cleared;
        stageClearPanel.SetActive(true);
        playerMovement.Freeze();
    }

    public void Pause()
    {
        if (currentState != GameState.Playing)
            return;
        currentState = GameState.Paused;
        Time.timeScale = 0f;
        pausedPanel.SetActive(true);
        playerMovement.SetInputEnabled(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        if (currentState != GameState.Paused)
            return;
        currentState = GameState.Playing;
        pausedPanel.SetActive(false);
        Time.timeScale = 1f;
        playerMovement.SetInputEnabled(true);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (currentState == GameState.Paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void OnDestroy()
    {
        controls.Dispose();
    }
}
