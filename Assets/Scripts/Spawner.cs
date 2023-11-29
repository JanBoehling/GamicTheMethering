using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    #region Variablen
    [Header("Objects")]
    [SerializeField] public GameObject SpawnObject;                             // Objekt, das erzeugt werden soll.              

    [Header("Values")]
    [SerializeField] float spawnheight;                                         // Feste Höhe, auf der das Objekt erzeugt werden soll.
    /*[SerializeField, Range(1, 100)]*/ int amountOfSpawns = 1;                 // Anzahl an Objekte, die erzeugt werden sollen.
    [SerializeField, Range(1, 100)] int maxTries = 10;                          // Maximale Anzahl an Versuchen, eine gültige Spawn-Position zu finden.
    [SerializeField, Range(0.01f, 2)] float minDistanceToPlayer = 0.5f;         // Minimalste Entfernung zum Spieler.
    [SerializeField, Range(0.01f, 2)] float minDistanceToOtherObjects = 0.5f;   // Minimalste Entfernung zu anderen Objekten.                                                   

    [Header("Other")]
    [SerializeField] bool useConstantSpawnHeight;                               // Gibt an, ob eine Konstante Spawn-Höhe verwendet werden soll.
    [SerializeField] bool rotateObjects;                                        // Gibt an, ob Objekte zufällig rotiert werden sollen.
    Vector3 spawnPosition;                                                      // Position, an der ein Objekt erzeugt werden soll.
    int missingObjects;                                                         // Anzahl an Objekten, die nicht erzeugt werden konnten.
    #endregion

    #region Methoden
    /// <summary>
    /// Führt eine Schleife aus, die mithilfe der "SpawnObjectAtRandom" Methode Objekte erzeugt.
    /// </summary>
    /// <param name="amountOfSpawns">Anzahl an Objekten, die erzeugt werden sollen</param>
    /// <param name="timeBetweenSpawns">Sekunden, die zwischen den spawns gewartet werden soll</param>
    public void Spawn(int amountOfSpawns, int timeBetweenSpawns)
    {
        StartCoroutine(SpawnCoroutine(amountOfSpawns, timeBetweenSpawns));
    }

    /// <summary>
    /// Führt eine Schleife aus, die mithilfe der "SpawnObjectAtRandom" Methode Objekte erzeugt.
    /// </summary>
    /// <param name="amountOfSpawns">Anzahl an Objekten, die erzeugt werden sollen</param>
    /// <param name="timeBetweenSpawns">Sekunden, die zwischen den spawns gewartet werden soll</param>
    /// <returns></returns>
    private IEnumerator SpawnCoroutine(int amountOfSpawns, int timeBetweenSpawns)
    {
        List<BoxCollider> spawnAreas = GetSpawnAreas();

        // Die Schleife führt die Spawn Methode je nach Menge der zu erzeugen Objekten aus.
        for (int i = 0; i < amountOfSpawns; i++)
        {
            SpawnObjectAtRandom(SpawnObject, GetRandomSpawnArea(spawnAreas));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        // Sobald mindestens ein Objekt nicht erzeugt werden konnte, wird folgendes ausgeben. 
#if UNITY_EDITOR
        if (missingObjects > 0)
        {
            Debug.LogWarning($"Die Anzahl an nicht erzeugten '{gameObject.name}' beträgt {missingObjects}. Um dies zu ändern, versuche die maximalen Versuche zu erhöhen oder bearbeite die Spawn-Area.");
        }
#endif
    }

    /// <summary>
    /// Erzeugt ein Objekt, in einem vorgegebenen Bereich.
    /// </summary>
    /// <param name="spawnObject">Objekt, dass erzeugt werden soll</param>
    /// <param name="spawnArea">Bereich, in dem das Objekt erzeugt werden soll</param>
    private void SpawnObjectAtRandom(GameObject spawnObject, BoxCollider spawnArea)
    {
        // Lokale Variable
        GameObject lastObject;                                                      // Nimmt das zuletzt erzeugte Objekt an und speichert dieses.
        bool overlay = false;                                                       // Gibt an, ob das Objekt sich beim Erzeugen mit einem anderen Objekt überlagern würde.
        int currentTries = 0;                                                       // Aktuelle Anzahl an Versuchen, eine Spawn-Position zu finden.
        GameObject player = GameObject.FindGameObjectWithTag("Player");    // Verknüpfung zum Spieler.

        do
        {
            // Die Methode wird abgebrochen, sobald eine maximale Anzahl an Versuchen erreicht wurde.
            if (currentTries > maxTries)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Es konnte nach {maxTries} versuchen, keine Position '{gameObject.name}' gefunden werden!");
#endif
                missingObjects++;
                return;
            }

            // Generieren einer zufälligen Position innerhalb des Spawn-Bereichs.
            spawnPosition = GetRandomSpawnPosition(spawnArea);

            // Überprüfen, ob sich die Position im Mindestabstand zum Spieler befindet.
            if (Vector3.Distance(spawnPosition, player.transform.position) < minDistanceToPlayer && player != null)
            {
                // Die Spawn-Position ist zu nah am Spieler.
                currentTries++;
                overlay = true;
            }
            
            // Überprüfen, ob sich bereits ein Objekt an der Spawn-Position befindet.
            Collider[] colliders = Physics.OverlapSphere(spawnPosition, minDistanceToOtherObjects);

            // Überprüfen, ob sich die Position im NavMesh-Bereich befindet
            NavMeshHit navMeshHit;

            if (colliders.Length > 1 && !NavMesh.SamplePosition(spawnPosition, out navMeshHit, minDistanceToOtherObjects, NavMesh.AllAreas))
            {
                // An der Spawn-Position befindet sich bereits ein Objekt.
                currentTries++;
                overlay = true;
            }
            else
            {
                // An der Spawn-Position befindet sich kein Objekt.
                overlay = false;
            }
        } while (overlay);

        // Objekt an zufälligen Position erzeugen.
        lastObject = Instantiate(spawnObject, spawnPosition, Quaternion.identity);

        // Rotiert das Objekt in eine zufällige Richtung.
        if (rotateObjects)
        {
            lastObject.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        }
    }

    /// <summary>
    /// Erzeugt eine zufällige Position im Spawn Bereich.
    /// </summary>
    /// <param name="spawnArea">Bereich, in dem das Objekt erzeugt werden soll</param>
    /// <returns></returns>
    private Vector3 GetRandomSpawnPosition(BoxCollider spawnArea)
    {
        if (useConstantSpawnHeight)
        {
            return new Vector3(
                UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                spawnheight,
                UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
        }
        else
        {
            return new Vector3(
                 UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                 UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                 UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
        }
    }

    /// <summary>
    /// Gibt alle SpawnAreas in einer Liste zurück.
    /// </summary>
    /// <returns></returns>
    private List<BoxCollider> GetSpawnAreas() 
    {
        List<BoxCollider> spawnAreas = new List<BoxCollider>();

        // Sammle alle Objekte mit dem Tag "SpawnArea"
        GameObject[] spawnAreaObjects = GameObject.FindGameObjectsWithTag("SpawnArea");

        if (spawnAreas.Count == 0) 
        {
            Debug.LogWarning("No spawn areas with the tag \"SpawnArea\" could be found.");
        }

        // Füge die gefundenen BoxCollider zur Liste hinzu
        foreach (GameObject spawnAreaObject in spawnAreaObjects)
        {
            // Versuche den BoxCollider des GameObjects zu erhalten
            BoxCollider boxCollider = spawnAreaObject.GetComponent<BoxCollider>();

            // Füge nur hinzu, wenn ein BoxCollider gefunden wurde
            if (boxCollider != null)
            {
                spawnAreas.Add(boxCollider);
            }
        }

        return spawnAreas;
    }

    /// <summary>
    /// Gibt eine zufällige SpawnArea zurück.
    /// </summary>
    /// <param name="spawnAreas">Liste mit den spawnAreas</param>
    /// <returns></returns>
    private BoxCollider GetRandomSpawnArea(List<BoxCollider> spawnAreas)
    {
        if (spawnAreas.Count > 0)
        {
            // Generiere einen zufälligen Index
            int randomIndex = UnityEngine.Random.Range(0, spawnAreas.Count);

            // Gib den BoxCollider an der zufälligen Indexposition zurück
            return spawnAreas[randomIndex];
        }
        else
        {
            // Falls die Liste leer ist, gib null zurück
            return null;
        }
    }

    /// <summary>
    /// Wartet X sekunden bis zum nächsten Befehl. 
    /// </summary>
    /// <param name="seconds">Sekunden die gewartet werden sollen</param>
    /// <returns></returns>
    private IEnumerator WaitforSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    #endregion
}
