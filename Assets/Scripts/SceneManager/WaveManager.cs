using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this for TextMeshPro

public class WaveManager : MonoBehaviour
{
    // singleton instance to ensure that only one instance exist throughout the game
    // provides global access that can be referenced, i.e line 26 of SpawnManager script 
    public static WaveManager Instance { get; private set; }

    [Header("Wave Settings")]
    public int currentWave = 0;
    public int enemiesPerWave = 5;
    public int enemiesIncreasePerWave = 2;
    public float timeBetweenWaves = 10f;

    [Header("Spawn Settings")]
    public float initialSpawnInterval = 5.0f;

    // amount by which the spawn interval decreases each wave
    public float spawnIntervalDecrease = 0.5f;

    // Minimum possible spawn interval.
    public float minimumSpawnInterval = 1.0f;

    //[Header("UI Settings")]
    //public TextMeshProUGUI waveStatusText;
    //public float textDisplayDuration = 3f;
    //public Image uiPanel;

    [Header("Debug Information")]
    public int enemiesRemainingToSpawn;
    public int enemiesRemainingAlive;
    public bool isWaveActive = false;

    // delegate defined
    public delegate void WaveEvent(int waveNumber);

    // event declarations
    public event WaveEvent OnWaveStart;
    public event WaveEvent OnWaveEnd;

    private Coroutine textDisplayCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(WaveLoop());
        //if (waveStatusText == null)
        //{
        //    Debug.LogWarning("Wave Status Text is not set in the WaveManager!");
        //}
        //if (uiPanel == null)
        //{
        //    Debug.LogWarning("UI Panel is not set in the WaveManager!");
        //}
        //else
        //{
        //    waveStatusText.text = "";
        //    SetPanelAlpha(0); // Initialize panel to be invisible
        //}
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            StartNewWave();
            while (enemiesRemainingAlive > 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
            EndWave();
        }
    }

    private void StartNewWave()
    {
        currentWave++;
        int enemiesToSpawn = enemiesPerWave + (currentWave - 1) * enemiesIncreasePerWave;
        enemiesRemainingToSpawn = enemiesToSpawn;
        enemiesRemainingAlive = enemiesToSpawn;
        isWaveActive = true;
        OnWaveStart?.Invoke(currentWave);
        Debug.Log($"Wave {currentWave} started. Enemies to spawn: {enemiesRemainingToSpawn}");
        //UpdateWaveStatusText($"Wave {currentWave} Started");
        LogWaveState();
    }

    private void EndWave()
    {
        isWaveActive = false;
        OnWaveEnd?.Invoke(currentWave);
        Debug.Log($"Wave {currentWave} ended. All enemies defeated.");
        //UpdateWaveStatusText($"Wave {currentWave} Ended");
        LogWaveState();
    }

    // check if more enemies can be spawned in the current wave
    public bool CanSpawnEnemy()
    {
        if (isWaveActive && enemiesRemainingToSpawn > 0)
        {
            enemiesRemainingToSpawn--;
            return true;
        }
        return false;
    }

    // handles the event of an enemy being killed
    public void EnemyKilled()
    {
        enemiesRemainingAlive--;
        Debug.Log($"Enemy killed. Remaining enemies: {enemiesRemainingAlive}");
        LogWaveState();
    }

    // calculates the current spawn interval
    public float GetCurrentSpawnInterval()
    {
        float interval = initialSpawnInterval - (currentWave - 1) * spawnIntervalDecrease;
        return Mathf.Max(interval, minimumSpawnInterval);
    }

    // Logs the current state of the wave for debugging purposes
    public void LogWaveState()
    {
        Debug.Log($"Wave State: Wave {currentWave}, Active: {isWaveActive}, " +
                  $"Enemies to Spawn: {enemiesRemainingToSpawn}, " +
                  $"Enemies Alive: {enemiesRemainingAlive}");
    }

    //private void UpdateWaveStatusText(string message)
    //{
    //    if (waveStatusText != null)
    //    {
    //        waveStatusText.text = message;
    //        SetPanelAlpha(0.3f);
    //        if (textDisplayCoroutine != null)
    //        {
    //            StopCoroutine(textDisplayCoroutine);
    //        }
    //        textDisplayCoroutine = StartCoroutine(ClearTextAfterDelay());
    //    }
    //}

    //private IEnumerator ClearTextAfterDelay()
    //{
    //    yield return new WaitForSeconds(textDisplayDuration);
    //    if (waveStatusText != null)
    //    {
    //        waveStatusText.text = "";
    //        SetPanelAlpha(0);
    //    }
    //}

    //private void SetPanelAlpha(float alpha)
    //{
    //    if (uiPanel != null)
    //    {
    //        Color panelColor = uiPanel.color;
    //        panelColor.a = alpha;
    //        uiPanel.color = panelColor;
    //    }
    //}
}