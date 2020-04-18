using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 1;   
    public LayerMask targetLayers;

    [System.Serializable]
    public class AttackPoint
    {
        public float radius;
        public Vector3 offset;
        public Transform attackRoot;
    }

    public AttackPoint[] attackPoints = new AttackPoint[0];

    protected bool m_InAttack = false;
    protected GameObject m_Owner;
    protected Vector3 m_Direction;

    //whoever own the weapon is responsible for calling that. Allow to avoid "self harm"
    public void SetOwner(GameObject owner)
    {
        m_Owner = owner;
    }

    public void BeginAttack()
    {
        m_InAttack = true;
    }

    public void EndAttack()
    {
        m_InAttack = false;
    }
    
    void FixedUpdate()
    {
        if (m_InAttack)
        {
            foreach (AttackPoint pts in attackPoints)
            {
                Vector3 ptsPosition = pts.attackRoot.position + pts.attackRoot.TransformVector(pts.offset);
                Collider[] hitTargets = Physics.OverlapSphere(ptsPosition, pts.radius, targetLayers);

                foreach (Collider target in hitTargets)
                {
                    if (target != null)
                        CheckDamage(target, pts);
                }
            }
        }
    }

    private bool CheckDamage(Collider other, AttackPoint pts)
    {
        Damageable d = other.GetComponent<Damageable>();
        if (d == null)
            return false;

        Debug.Log("You hit " + other.name);

        Damageable.DamageMessage data;

        data.amount = damage;
        data.damager = this;
        data.direction = m_Direction.normalized;
        data.damageSource = m_Owner.transform.position;

        d.ApplyDamage(data);
        return true;
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        foreach (AttackPoint pts in attackPoints)
        {
            Vector3 worldPos = pts.attackRoot.TransformVector(pts.offset);
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.4f);
            Gizmos.DrawSphere(pts.attackRoot.position + worldPos, pts.radius);
        }
    }

#endif
}
