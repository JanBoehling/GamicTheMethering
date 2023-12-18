using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Variablen 
    [Header("Connections")]
    [SerializeField] private LevelSettings levelSettings;   // Verknüpfung zu den Level Einstellungen
    private Wave wave;                                      // Verknüpfung zum Wellen Skript

    [Header("Other")]
    [SerializeField] private int waveNumber;                // Anzahl der Welle          
    #endregion

    #region Unity Methoden 
    private void Start()
    {
        // GetComponents
        wave = GetComponent<Wave>();

        waveNumber = levelSettings.StartWave;

        // Prozentwerte der Monsterverteilung prüfen
        if ((levelSettings.Enemy1PercentageD1 + levelSettings.Enemy2PercentageD1 + levelSettings.Enemy3PercentageD1) == 100
        && (levelSettings.Enemy1PercentageD2 + levelSettings.Enemy2PercentageD2 + levelSettings.Enemy3PercentageD2) == 100
        && (levelSettings.Enemy1PercentageD3 + levelSettings.Enemy2PercentageD3 + levelSettings.Enemy3PercentageD3) == 100)
        {
            // Erste Welle starten 
            StartWave(waveNumber);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("The monster distribution is invalid. All wave ranges must reach 100%. These settings can be changed in the level settings.");
#endif
        }
    }
    #endregion

    #region Methoden
    /// <summary>
    /// Startet eine bestimmte Welle.
    /// </summary>
    /// <param name="_waveNumber">Nummer der Welle die gestartet werden soll.</param>
    public void StartWave(int _waveNumber)
    {
        levelSettings.LivingEnemies = 0; 

#if UNITY_EDITOR
        Debug.Log("Start wave: " + _waveNumber);
#endif

        wave.SetWaveNumber(_waveNumber); // Setzt die Nummer der Welle 
        wave.SpawnEnemys();              // Gibt der Welle die Anweisung die Gegner zu erzeugen

        StartCoroutine(WaitForWaveCompletion());
    }

    /// <summary>
    /// Startet die nächste Welle, wenn alle Monster besiegt wurden.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForWaveCompletion()
    {
        while (levelSettings.LivingEnemies != 0)
        {
            yield return new WaitForSeconds(1);
        }

        waveNumber++;
        StartWave(waveNumber);
    }



    #endregion
}
