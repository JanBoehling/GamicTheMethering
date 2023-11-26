using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings")]

public class LevelSettings : ScriptableObject
{
    [Header("DEBUG")]
    [SerializeField] private int startWave = 1; 

    [Header("Settings")]
    [SerializeField] private float enemyWavesMultiplier = 1.2f;

    [Header("Enemy Distribution Percentages (W 1-5)")]
    [SerializeField, Range(0, 100)] private int enemy1PercentageD1 = 100;
    [SerializeField, Range(0, 100)] private int enemy2PercentageD1 = 0;
    [SerializeField, Range(0, 100)] private int enemy3PercentageD1 = 0;

    [Header("Enemy Distribution Percentages (W 6-10)")]
    [SerializeField, Range(0, 100)] private int enemy1PercentageD2 = 70;
    [SerializeField, Range(0, 100)] private int enemy2PercentageD2 = 30;
    [SerializeField, Range(0, 100)] private int enemy3PercentageD2 = 0;

    [Header("Enemy Distribution Percentages (W 11-X)")]
    [SerializeField, Range(0, 100)] private int enemy1PercentageD3 = 50;
    [SerializeField, Range(0, 100)] private int enemy2PercentageD3 = 40;
    [SerializeField, Range(0, 100)] private int enemy3PercentageD3 = 10;

    #region Getter / Setter
    public int StartWave { get { return startWave; } }
    public float EnemyWavesMultiplier { get { return enemyWavesMultiplier; } }

    #region Enemy Distribution Percentages
    public int Enemy1PercentageD1 { get { return enemy1PercentageD1; } }
    public int Enemy2PercentageD1 { get { return enemy2PercentageD1; } }
    public int Enemy3PercentageD1 { get { return enemy3PercentageD1; } }

    public int Enemy1PercentageD2 { get { return enemy1PercentageD2; } }
    public int Enemy2PercentageD2 { get { return enemy2PercentageD2; } }
    public int Enemy3PercentageD2 { get { return enemy3PercentageD2; } }

    public int Enemy1PercentageD3 { get { return enemy1PercentageD3; } }
    public int Enemy2PercentageD3 { get { return enemy2PercentageD3; } }
    public int Enemy3PercentageD3 { get { return enemy3PercentageD3; } }
    #endregion

    #endregion
}
