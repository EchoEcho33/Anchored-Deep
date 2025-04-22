using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Image healthbarSprite;
    
    public void UpdateHealthBar(float maxHealth, float currentHealth) {
        healthbarSprite.fillAmount = currentHealth / maxHealth;
    }
}
