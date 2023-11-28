using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyEmitter : MonoBehaviour
{
    private void Awake() => Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
}
