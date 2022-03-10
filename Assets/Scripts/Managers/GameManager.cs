using UnityEngine;
using UnityEngine.SceneManagement;
using EventChannels;

public class GameManager : MonoBehaviour
{
    [Header("General Properties")]
    [SerializeField] int targetFrameRate = 60;

    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;

    [Header("Waypoints")]
    [SerializeField] Waypoint initialWaypoint;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        gameEvents.OnLevelStarted();
    }

    void OnEnable()
    {
        gameEvents.OnLevelStarted += StartLevel;
        gameEvents.OnLevelFinished += FinishLevel;
    }

    void OnDisable()
    {
        gameEvents.OnLevelStarted -= StartLevel;    
        gameEvents.OnLevelFinished -= FinishLevel;
    }

    void StartLevel()
    {
        gameEvents.SetWaypoint(initialWaypoint);
    }

    void FinishLevel(bool win)
    {
        // Restart scene either way
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
