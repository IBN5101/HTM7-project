using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParticleGenrator : MonoBehaviour
{
    public static ParticleGenrator Instance { get; private set; }

    [SerializeField] private Transform winParticle;
    [SerializeField] private Transform loseParticle;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateWinParticle(Vector3 position)
    {
        Instantiate(winParticle, position, Quaternion.identity);
    }

    public void GenerateLoseParticle(Vector3 position)
    {
        Instantiate(loseParticle, position, Quaternion.identity);
    }
}
