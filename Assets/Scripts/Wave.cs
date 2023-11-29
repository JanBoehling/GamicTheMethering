using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    #region Variablen
    [Header("Connections")]
    [SerializeField] private LevelSettings levelSettings; // Verknüpfung zu den Level Einstellungen

    [Header("Enemies")]
    [SerializeField] private Spawner enemy1;              // Verknüpfung zu einem Gegner
    [SerializeField] private Spawner enemy2;              // Verknüpfung zu einem Gegner
    [SerializeField] private Spawner enemy3;              // Verknüpfung zu einem Gegner

    [Header("Boss")]
    [SerializeField] private Spawner boss1;               // Verknüpfung zum ersten Boss
    [SerializeField] private Spawner boss2;               // Verknüpfung zum ersten Boss
    [SerializeField] private Spawner boss3;               // Verknüpfung zum ersten Boss

    [Header("Other")]
    private int waveNumber;                               // Anzahl der Welle 
    private float numberOfObjectsToSpawn;                 // Anzahl an Gegners
    #endregion

    #region Getter / Setter
    // Setter
    public void SetWaveNumber(int _waveNumber) { waveNumber = _waveNumber; }  // Setzt die Nummer der Welle
    #endregion

    #region Methoden
    /// <summary>
    /// Verteilt die zu erzeugenden Monster mithilfe der vorgegebenen Prozentwerte auf.
    /// </summary>
    /// <returns>Array mit der Anzahl der zu erzeugenden Monster</returns>
    private int[] SpawnDistribution()
    {
        // Anzahl der zu erzeugenden Monster ermitteln
        numberOfObjectsToSpawn = waveNumber * levelSettings.EnemyWavesMultiplier; 

        // Array und lokale Variablen erstellen
        int[] distribution = new int[3];
        int enemy1Count = 0;
        int enemy2Count = 0;
        int enemy3Count = 0;

        // Welle 1-5
        if (waveNumber <= 5) 
        {
            // Abrufen der Prozentwerte aus dem LevelSettings

            // Boss Welle mit Gegnern 
            if (waveNumber == 5 && levelSettings.SpawnNormalEnemiesAtBossWave)
            {
                enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD1 * 0.01f * numberOfObjectsToSpawn);
                enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD1 * 0.01f * numberOfObjectsToSpawn);
                enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD1 * 0.01f * numberOfObjectsToSpawn);
            }
            // Boss Welle ohne Gegner
            else if (waveNumber == 5 && !levelSettings.SpawnNormalEnemiesAtBossWave)
            {
                enemy1Count = 0; 
                enemy2Count = 0;
                enemy3Count = 0;
            }
            // Normale Welle 
            else
            {
                enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD1 * 0.01f * numberOfObjectsToSpawn);
                enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD1 * 0.01f * numberOfObjectsToSpawn);
                enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD1 * 0.01f * numberOfObjectsToSpawn);
            }
        }
        // Welle 6-10
        else if (waveNumber <= 10)
        {
            // Abrufen der Prozentwerte aus dem LevelSettings

            // Boss Welle mit Gegnern 
            if (waveNumber == 10 && levelSettings.SpawnNormalEnemiesAtBossWave)
            {
                enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD2 * 0.01f * numberOfObjectsToSpawn);
                enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD2 * 0.01f * numberOfObjectsToSpawn);
                enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD2 * 0.01f * numberOfObjectsToSpawn);
            }
            // Boss Welle ohne Gegner
            else if (waveNumber == 10 && !levelSettings.SpawnNormalEnemiesAtBossWave)
            {
                enemy1Count = 0;
                enemy2Count = 0;
                enemy3Count = 0;
            }
            // Normale Welle 
            else
            {
                enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD2 * 0.01f * numberOfObjectsToSpawn);
                enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD2 * 0.01f * numberOfObjectsToSpawn);
                enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD2 * 0.01f * numberOfObjectsToSpawn);
            }
        }
        // Welle 11-15
        else if (waveNumber <= 15)
        {
            // Abrufen der Prozentwerte aus dem LevelSettings

            // Boss Welle mit Gegnern 
            if (waveNumber == 15 && levelSettings.SpawnNormalEnemiesAtBossWave)
            {
                enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD3 * 0.01f * numberOfObjectsToSpawn);
                enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD3 * 0.01f * numberOfObjectsToSpawn);
                enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD3 * 0.01f * numberOfObjectsToSpawn);
            }
            // Boss Welle ohne Gegner
            else if (waveNumber == 15 && !levelSettings.SpawnNormalEnemiesAtBossWave)
            {
                enemy1Count = 0;
                enemy2Count = 0;
                enemy3Count = 0;
            }
            // Normale Welle 
            else
            {
                enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD3 * 0.01f * numberOfObjectsToSpawn);
                enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD3 * 0.01f * numberOfObjectsToSpawn);
                enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD3 * 0.01f * numberOfObjectsToSpawn);
            }
        }
        // Welle 15-X
        else if (waveNumber > 15)
        {
            // Abrufen der Prozentwerte aus dem LevelSettings
            enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD3 * 0.01f * numberOfObjectsToSpawn);
            enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD3 * 0.01f * numberOfObjectsToSpawn);
            enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD3 * 0.01f * numberOfObjectsToSpawn);
        }

        // Werte im Array setzen
        distribution[0] = enemy1Count;
        distribution[1] = enemy2Count;
        distribution[2] = enemy3Count;

        // Array zurückgeben 
        return distribution;
    }

    /// <summary>
    /// Erzeugt die Monster. 
    /// </summary>
    public void SpawnEnemys()
    {
        // Anzahl an Monster ermitteln und verteilen 
        int[] distribution = SpawnDistribution();

        // Monster erzeugen 
        enemy1.Spawn(distribution[0], levelSettings.TimeBetweenSpawns);
        enemy2.Spawn(distribution[1], levelSettings.TimeBetweenSpawns);
        enemy3.Spawn(distribution[2], levelSettings.TimeBetweenSpawns);

        // Boss erzeugen 
        if (waveNumber == 5) { boss1.Spawn(1, levelSettings.TimeBetweenSpawns); }
        if (waveNumber == 10) { boss2.Spawn(1, levelSettings.TimeBetweenSpawns); }
        if (waveNumber == 15) { boss3.Spawn(1, levelSettings.TimeBetweenSpawns); }

    }
    #endregion
}
