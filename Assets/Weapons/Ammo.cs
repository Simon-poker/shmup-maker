﻿using UnityEngine;

public class Ammo : Vehicle
{
    [SerializeField] float damage;
    [SerializeField] float lifeTime = 5f;

    public void SetOwner(Team owner)
    {
        gameObject.layer = 3 + (int)owner;
    }

    void OnCollisionEnter(Collision coll)
    {
        Life otherLife = coll.gameObject.GetComponent<Life>();

        if (otherLife != null)
        {
            otherLife.Hurt(damage);
            hull.Kill();
        }
    }

    protected override void FixedUpdate()
    {
        var angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        MoveTowardRelative(-Mathf.Sin(angle), Mathf.Cos(angle));
        base.FixedUpdate();
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0)
            hull.Kill();
    }
}
