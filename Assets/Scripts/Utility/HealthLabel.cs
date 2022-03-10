using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class HealthLabel : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] bool hideOnZero = true;
    TextMeshPro textMesh;
    
    int hp;
    int maxHp;

    void Awake() {
        textMesh = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        maxHp = health.Hp;
        hp = maxHp;
        UpdateLabel();
    }

    void OnEnable()
    {
        health.OnHpUpdated += OnHpUpdated;
    }

    void OnDisable()
    {
        health.OnHpUpdated -= OnHpUpdated;
    }

    void OnHpUpdated(int hp)
    {
        this.hp = hp;
        UpdateLabel();
    }

    void UpdateLabel()
    {
        textMesh.text = $"{hp}/{maxHp}";
        if (hideOnZero)
            textMesh.enabled = hp > 0;
    }
}
