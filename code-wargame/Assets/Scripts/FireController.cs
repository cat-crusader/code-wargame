using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireController : MonoBehaviour
{
    public GameObject ShellTemporary;

    public event EventHandler<OnExplosionArgs> OnExplosion;
    public class OnExplosionArgs: EventArgs
    {
        public Vector3 position;
        public int power;
    }

    public void FireShot(Unit from, Unit target, WeaponSO weapon)
    {
        if (weapon.Damage < 2 && weapon.damageType == DamageType.Explosive)//firearms
        {
            if (weapon.Accuracy > Random.Range(0f, 100.0f))
            {
                target.TakeDamage((int)weapon.Damage);
                Debug.Log(from.name + " hitted " + target.name + " with " + weapon.name);
            }
        }
        else if (weapon.damageType == DamageType.Explosive)//he shells
        {
            Vector3 hit = new Vector3(CalculateShotDeviationPosition(weapon) + target.gameObject.transform.position.x, CalculateShotDeviationPosition(weapon) + target.gameObject.transform.position.y, CalculateShotDeviationPosition(weapon) + target.gameObject.transform.position.z);

            // REMOVE CODE BELOW AND DO IT WITH EVENTS;

            GameObject shell = Instantiate(ShellTemporary, from.transform.position, new Quaternion());
            ShellScipt shscrpt = shell?.GetComponent<ShellScipt>();
            shscrpt.startPosition = from.transform.position;
            shscrpt.endPosition = hit;

            // REMOVE CODE ABOVE AND DO IT WITH EVENTS;

            InstantiateExplosion(hit, (int)weapon.Damage);
        }
        else if (weapon.damageType == DamageType.Penetration&&target.unitStats.type!=UnitType.Infantry)// sabot, etc
        {
            if (weapon.Accuracy > Random.Range(0f, 100.0f))
            {

                target.TakeDamage((int)CalculatePenetrationDamage(target.unitStats.frontalArmor,weapon.Damage));
                Debug.Log(from.name + " hitted " + target.name + " with " + weapon.name);
            }

        }
    }
    public float CalculatePenetrationDamage(int armorLevel, float damage)
    {
        if (armorLevel == 0) return damage * 2;
        return damage/armorLevel;
    }
    //public float CalculateHEDamageToArmor(int)
    public float CalculateShotDeviationPosition(WeaponSO weapon)
    {
        return Random.Range(0f, 100.0f - weapon.Accuracy) / 20f;
    }
    public void InstantiateExplosion(Vector3 position, int Power)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, Power / 2f);



        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponentInParent<Unit>() == null) continue;
            Unit unit = hitCollider.gameObject.GetComponentInParent<Unit>();
            float Distance = Vector3.Distance(unit.gameObject.transform.position, position);

            unit.TakeDamage((int)(Power / Distance));
        }
        OnExplosion?.Invoke(this, new OnExplosionArgs { position = position, power= Power});
    }


}
