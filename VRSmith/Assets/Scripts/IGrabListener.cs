using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IGrabListener : IEventSystemHandler
{
    void OnGrab (Collider collider);
    void OnRelease (Collider collider);
}

