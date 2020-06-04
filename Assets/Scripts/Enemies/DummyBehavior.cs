using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message;

public class DummyBehavior : MonoBehaviour, IMessageReceiver
{   
    public void OnReceiveMessage(Message.MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case Message.MessageType.DEAD:
                Death((Damageable.DamageMessage)msg);
                break;
            case Message.MessageType.DAMAGED:
                ApplyDamage((Damageable.DamageMessage)msg);
                break;
            default:
                break;
        }
    }

    public void Death(Damageable.DamageMessage msg)
    {
        Debug.Log(gameObject.name + ": Noooooooooo!");

        //TODO: set death animation

        gameObject.SetActive(false);
    }

    public void ApplyDamage(Damageable.DamageMessage msg)
    {
        //TODO: set recieve damage animation

        Debug.Log(gameObject.name + ": Why did you hit me?");
    }
}
