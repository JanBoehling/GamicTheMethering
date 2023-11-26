using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Variablen 
    [Header("Connections")]
    [SerializeField] private LevelSettings levelSettings;   // Verknüpfung zu den Level Einstellungen
    private Wave wave;                                      // Verknüpfung zum Wellen Skript
    #endregion

    #region Unity Methoden 
    private void Start()
    {
        // GetComponents
        wave = GetComponent<Wave>();

        // Erste Welle starten 
        StartWave(levelSettings.StartWave);
    }
    #endregion

    #region Methoden
    /// <summary>
    /// Startet eine bestimmte Welle.
    /// </summary>
    /// <param name="_waveNumber">Nummer der Welle die gestartet werden soll.</param>
    public void StartWave(int _waveNumber)
    {
        wave.SetWaveNumber(_waveNumber); // Setzt die Nummer der Welle 
        wave.SpawnEnemys();              // Gibt der Welle die Anweisung die Gegner zu erzeugen
    }
    #endregion
}
