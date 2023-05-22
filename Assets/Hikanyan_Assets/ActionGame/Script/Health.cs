using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : HealthComponent
{
    enum target
    {
        Player,
        Enemy,
        None
    }
    [SerializeField] target m_Target= target.None;
    [SerializeField] HealthComponent healthComponent;
    private void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void Update()
    {
        Debug.Log($"{m_Target}HP:{healthComponent.Life}");
    }
}
