using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Hikanyan_Assets.ActionGame.Script
{
    public class Health : HealthComponent
    {
        enum target
        {
            Player,
            Enemy,
            None
        }
        [SerializeField] target m_Target = target.None;
        [SerializeField] HealthComponent healthComponent;
        [SerializeField] Text healthText;
        private void Start()
        {
            healthComponent = GetComponent<HealthComponent>();
        }

        private void Update()
        {
            Debug.Log($"{m_Target}HP:{healthComponent.Life}");
            if (m_Target == target.Player)
            {
                healthText.text = $"{m_Target}HP:{healthComponent.Life}";
            }
        }
    }
}