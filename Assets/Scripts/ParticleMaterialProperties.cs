using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaterialProperties : MonoBehaviour
{
    private ParticleSystemRenderer psRenderer;
    private MaterialPropertyBlock propBlock;

    void Start()
    {
        // Obtén el ParticleSystemRenderer del objeto
        psRenderer = GetComponent<ParticleSystemRenderer>();
        // Crea una nueva instancia de MaterialPropertyBlock
        propBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        // Cambia las propiedades del material del ParticleSystem
        psRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("Black", new Color(0, 0, 0));
        psRenderer.SetPropertyBlock(propBlock);
    }
}
