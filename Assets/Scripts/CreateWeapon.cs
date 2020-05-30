using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWeapon : MonoBehaviour
{
    public GameObject hand;
    public GameObject prefab;
    private GameObject weapon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weapon = Instantiate(prefab);

            weapon.transform.SetParent(other.gameObject.transform);
            //weapon.transform.localPosition = Vector3.zero;
            //weapon.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.zero);

            weapon.GetComponent<FixedUpdateFollow>().toFollow = hand.transform;
            MeleeWeapon weaponScript = weapon.GetComponent<MeleeWeapon>();
            weaponScript.SetOwner(other.gameObject);

            other.gameObject.GetComponent<PlayerController>().SetWeapon(weaponScript);

            //// оружие левой руки
            //weaponL = CreateBaseWeapon(handL, prefabL);

            //// оружие правой руки
            //weaponR = CreateBaseWeapon(handR, prefabR);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(weapon);
            other.gameObject.GetComponent<PlayerController>().SetWeapon(null);
        }
    }

    private GameObject CreateBaseWeapon(GameObject hand, GameObject prefab)
    {
        if (!hand || !prefab)
            return null;

        var weapon = Instantiate(prefab);

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.zero);
        weapon.transform.localScale = new Vector3(1f, 1f, 1f);

        return weapon;
    }
}
