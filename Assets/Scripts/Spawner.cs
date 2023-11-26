using System;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    #region Variablen
    [Header("Objects")]
    [SerializeField] public GameObject SpawnObject;                          // Objekt, das erzeugt werden soll.
    [SerializeField] public BoxCollider SpawnArea;                           // Bereich, indem die Objekte erzeugt werden sollen.                 

    [Header("Values")]
    [SerializeField] float spawnheight;                                      // Feste H�he, auf der das Objekt erzeugt werden soll.
    /*[SerializeField, Range(1, 100)]*/ int amountOfSpawns = 1;              // Anzahl an Objekte, die erzeugt werden sollen.
    [SerializeField, Range(1, 100)] int maxTries = 10;                       // Maximale Anzahl an Versuchen, eine g�ltige Spawn-Position zu finden.
    [SerializeField, Range(0.01f, 2)] float distanceToOtherObjects = 0.5f;   // Minimalste Entfernung zu anderen Objekten.                                                   

    [Header("Other")]
    [SerializeField] bool useConstantSpawnHeight;                            // Gibt an, ob eine Konstante Spawn-H�he verwendet werden soll.
    [SerializeField] bool rotateObjects;                                     // Gibt an, ob Objekte zuf�llig rotiert werden sollen.
    [SerializeField] bool runAtStartup;                                      // F�hrt das Skript beim Start aus. Alternativ, muss die "Spawn" Methode von einem anderen Objekt aus, ausgef�hrt werden.
    Vector3 spawnPosition;                                                   // Position, an der ein Objekt erzeugt werden soll.
    int missingObjects;                                                      // Anzahl an Objekten, die nicht erzeugt werden konnten.
    #endregion

    #region Unity Methoden
    /// <summary>
    /// Die Start-Methode beinhaltet eine Schleife, die mithilfe der "SpawnObjectAtRandom" Methode Objekte erzeugt.
    /// </summary>
    private void Start()
    {
        if (runAtStartup)
        {
            // Die Schleife f�hrt die Spawn Methode je nach Menge der zu erzeugen Objekten aus.
            for (int i = 0; i < amountOfSpawns; i++)
            {
                SpawnObjectAtRandom(SpawnObject, SpawnArea);
            }

            // Sobald mindestens ein Objekt nicht erzeugt werden konnte, wird dies hier ausgeben. 
#if UNITY_EDITOR
            if (missingObjects > 0)
            {
                Debug.LogWarning($"Die Anzahl an nicht erzeugten '{gameObject.name}' betr�gt {missingObjects}. Um dies zu �ndern, versuche die maximalen Versuche zu erh�hen oder bearbeite die Spawn-Area.");
            }
#endif
        }
    }
    #endregion

    #region Methoden
    /// <summary>
    /// F�hrt eine Schleife aus, die mithilfe der "SpawnObjectAtRandom" Methode Objekte erzeugt.
    /// </summary>
    /// <param name="amountOfSpawns">Anzahl an Objekten, die erzeugt werden sollen</param>
    public void Spawn(int amountOfSpawns)
    {
        // Die Schleife f�hrt die Spawn Methode je nach Menge der zu erzeugen Objekten aus.
        for (int i = 0; i < amountOfSpawns; i++)
        {
            SpawnObjectAtRandom(SpawnObject, SpawnArea);
        }

        // Sobald mindestens ein Objekt nicht erzeugt werden konnte, wird dies hier ausgeben. 
#if UNITY_EDITOR
        if (missingObjects > 0)
        {
            Debug.LogWarning($"Die Anzahl an nicht erzeugten '{gameObject.name}' betr�gt {missingObjects}. Um dies zu �ndern, versuche die maximalen Versuche zu erh�hen oder bearbeite die Spawn-Area.");
        }
#endif
    }

    /// <summary>
    /// Erzeugt ein Objekt, in einem vorgegebenen Bereich.
    /// </summary>
    /// <param name="spawnObject">Objekt, dass erzeugt werden soll</param>
    /// <param name="spawnArea">Bereich, in dem das Objekt erzeugt werden soll</param>
    public void SpawnObjectAtRandom(GameObject spawnObject, BoxCollider spawnArea)
    {
        // Lokale Variable
        GameObject lastObject;                                               // Nimmt das zuletzt erzeugte Objekt an und speichert dieses.
        bool overlay = false;                                                // Gibt an, ob das Objekt sich beim Erzeugen mit einem anderen Objekt �berlagern w�rde.
        int currentTries = 0;                                                // Aktuelle Anzahl an Versuchen, eine Spawn-Position zu finden.

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

            // Generieren einer zuf�lligen Position innerhalb des Spawn-Bereichs.
            spawnPosition = GetRandomSpawnPosition(spawnArea);


            // �berpr�fen, ob sich bereits ein Objekt an der Spawn-Position befindet.
            Collider[] colliders = Physics.OverlapSphere(spawnPosition, distanceToOtherObjects);

            // �berpr�fen, ob sich die Position im NavMesh-Bereich befindet
            NavMeshHit navMeshHit;

            if (colliders.Length > 1 && !NavMesh.SamplePosition(spawnPosition, out navMeshHit, distanceToOtherObjects, NavMesh.AllAreas))
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

        // Objekt an zuf�lligen Position erzeugen.
        lastObject = Instantiate(spawnObject, spawnPosition, Quaternion.identity);

        // Rotiert das Objekt in eine zuf�llige Richtung.
        if (rotateObjects)
        {
            lastObject.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        }
    }

    /// <summary>
    /// Erzeugt eine zuf�llige Position im Spawn Bereich.
    /// </summary>
    /// <param name="spawnArea">Bereich, in dem das Objekt erzeugt werden soll</param>
    /// <returns></returns>
    public Vector3 GetRandomSpawnPosition(BoxCollider spawnArea)
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
    #endregion
}
