using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerSO;

    [Header("Player Stats")]
    internal float maxHealth;
    internal float moveSpeed;

    [Header("Weapon Stats")]
    internal float fireRate;
    internal float damage;

    [Header("Health Bar")]
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private TextMeshProUGUI text;

    [Header("IFrames")]
    [SerializeField]
    private Color flashColor;
    [SerializeField]
    private Color regularColor;
    [SerializeField]
    private float flashDuration;
    [SerializeField]
    private int numberOfFlashes;
    private SpriteRenderer sr;

    private float currentHealth;

    private bool canLoseHealth = true;

    void Awake()
    {
        sr = GetComponentInParent<SpriteRenderer>();

        maxHealth = playerSO.health;
        moveSpeed = playerSO.moveSpeed;
        fireRate = playerSO.fireRate;
        damage = playerSO.damage; 
    }

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        healthBar.SetHealth(currentHealth);
    }

    protected void FixedUpdate()
    {
        healthBar.SetMaxHealth(maxHealth);

        healthBar.SetHealth(currentHealth);

        text.text = $"{currentHealth}/{maxHealth}";

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void LoseHealth(float damage)
    {
        if (canLoseHealth)
        {
            StartCoroutine(IFrames());

            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator IFrames()
    {
        int temp = 0;
        canLoseHealth = false;
        while (temp < numberOfFlashes)
        {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sr.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        canLoseHealth = true;
    }

    private void OnDestroy()
    {
        StopCoroutine(IFrames());
    }
}
