﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Damageable : MonoBehaviour
{
    public struct DamageMessage
    {
        // отсавил те, которы думаю пригодятся
        public MonoBehaviour damager;
        public int amount;
        public Vector3 direction;
        public Vector3 damageSource;
        //public bool throwing;

        //public bool stopCamera;
    }
}
