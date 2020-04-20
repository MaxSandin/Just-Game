using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: может быть ещё нужно оставлять namespace Gamekit3D?
namespace Message
{
    public enum MessageType
    {
        DAMAGED,
        DEAD
    }

    public interface IMessageReceiver
    {
        void OnReceiveMessage(MessageType type, object sender, object msg);
    }
}
