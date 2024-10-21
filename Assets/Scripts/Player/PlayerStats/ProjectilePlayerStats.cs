using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerStats : PlayerStats
{
    [SerializeField]
    private ProjectilePlayerScriptableObject pPlayerSO;

    internal float projectileSpeed;

    protected override void Awake()
    {
        base.Awake();

        projectileSpeed = pPlayerSO.projectileSpeed;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override IEnumerator IFrames()
    {
        return base.IFrames();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
