using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
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
    protected Color flashColor;
    [SerializeField]
    protected Color regularColor;
    [SerializeField]
    protected float flashDuration;
    [SerializeField]
    protected int numberOfFlashes;
    protected SpriteRenderer sr;

    protected float currentHealth;

    protected bool canLoseHealth = true;

    protected virtual void Awake()
    {
        sr = GetComponentInParent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        GetStats();

        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        healthBar.SetHealth(currentHealth);

        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged += GetStats;
        }
        else
        {
            Debug.LogError("PlayerStatsManager.Instance is null in Start!");
        }
    }

    void Update()
    {

    }

    protected virtual void FixedUpdate()
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

    protected virtual IEnumerator IFrames()
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

    private void GetStats()
    {
        maxHealth = PlayerStatsManager.Instance.health;
    }

    protected virtual void OnDestroy()
    {
        StopCoroutine(IFrames());

        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged -= GetStats;
        }
    }
}
