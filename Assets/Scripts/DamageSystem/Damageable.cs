using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message;

public partial class Damageable : MonoBehaviour
{
    public int maxHitPoints = 10;
    private int currentHitPoints;

    [Tooltip("When this gameObject is damaged, these other gameObjects are notified.")]
    [EnforceType(typeof(Message.IMessageReceiver))]
    public List<MonoBehaviour> onDamageMessageReceivers;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public void ApplyDamage(DamageMessage data)
    {
        currentHitPoints -= data.amount;

        var messageType = currentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;

        for (var i = 0; i < onDamageMessageReceivers.Count; ++i)
        {
            var receiver = onDamageMessageReceivers[i] as IMessageReceiver;
            receiver.OnReceiveMessage(messageType, this, data);
        }
    }
}
