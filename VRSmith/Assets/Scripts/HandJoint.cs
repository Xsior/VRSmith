using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class HandJoint : MonoBehaviour, IGrabListener
{
    private FixedJoint joint;

    public void OnGrab (Collider collider)
    {
        joint.connectedBody = collider.GetComponent<Rigidbody>();
    }

    public void OnRelease (Collider collider)
    {
        joint.connectedBody = null;
    }

    private void Awake ()
    {
        joint = GetComponent<FixedJoint>();
    }
}

