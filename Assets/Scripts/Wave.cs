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
    [SerializeField] private Spawner testEnemy1;          // Verknüpfung zu einem Gegner
    [SerializeField] private Spawner testEnemy2;          // Verknüpfung zu einem Gegner
    [SerializeField] private Spawner testEnemy3;          // Verknüpfung zu einem Gegner

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
            enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD1 * 0.01f * numberOfObjectsToSpawn);
            enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD1 * 0.01f * numberOfObjectsToSpawn);
            enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD1 * 0.01f * numberOfObjectsToSpawn);
        }
        // Welle 6-10
        else if (waveNumber <= 10)
        {
            // Abrufen der Prozentwerte aus dem LevelSettings
            enemy1Count = Mathf.RoundToInt(levelSettings.Enemy1PercentageD2 * 0.01f * numberOfObjectsToSpawn);
            enemy2Count = Mathf.RoundToInt(levelSettings.Enemy2PercentageD2 * 0.01f * numberOfObjectsToSpawn);
            enemy3Count = Mathf.RoundToInt(levelSettings.Enemy3PercentageD2 * 0.01f * numberOfObjectsToSpawn);
        }
        // Welle 11-X
        else if (waveNumber > 10)
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
        testEnemy1.Spawn(distribution[0]);
        testEnemy2.Spawn(distribution[1]);
        testEnemy3.Spawn(distribution[2]);
    }
    #endregion
}
