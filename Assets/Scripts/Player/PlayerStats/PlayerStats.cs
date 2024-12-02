using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerDeath pd;

    internal float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            if (maxHealth != value)
            {
                float difference = value - maxHealth;
                maxHealth = value;

                currentHealth += difference;
            }
        }
    }

    [Header("Player Stats")]
    internal float maxHealth;
    internal float regenPerSec;

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

        pd = GetComponentInParent<PlayerDeath>();
    }

    protected virtual void Start()
    {
        GetStats();

        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        healthBar.SetHealth((int)currentHealth);

        StartCoroutine(RegenHealth());

        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged += GetStats;
        }
        else
        {
            Debug.LogError("PlayerStatsManager.Instance is null in Start!");
        }
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
                pd.Die();
            }
        }
    }

    public void GainHealth(float amount)
    {
        currentHealth = Math.Min(currentHealth + amount, maxHealth);
    }

    protected IEnumerator RegenHealth()
    {
        while(gameObject != null)
        {
            yield return new WaitForSeconds(1f);

            currentHealth += regenPerSec;
        }
    }

    public virtual IEnumerator IFrames()
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
        MaxHealth = PlayerStatsManager.Instance.health;
        regenPerSec = PlayerStatsManager.Instance.regenPerSec;
    }

    protected virtual void OnDestroy()
    {
        StopCoroutine(IFrames());
        StopCoroutine(RegenHealth());

        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged -= GetStats;
        }
    }
}
