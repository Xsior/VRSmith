using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlTest : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>()) {
            return;
        }
        
        collidingObject = col.gameObject;
    }
    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }
    private void GrabObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2
        objectInHand.transform.SetParent(transform, false);
        objectInHand.transform.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
        objectInHand.transform.localPosition = Vector3.forward * 1f;
        objectInHand.GetComponent<Rigidbody>().useGravity = false;
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;

        SetControllerVisible(false);
    }

    private void ReleaseObject()
    {
        var rb = objectInHand.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        objectInHand.transform.SetParent(null);
        objectInHand = null;

        SetControllerVisible(true);
    }
    
    void SetControllerVisible(bool visible)
    {
        foreach (var model in GetComponentsInChildren<SteamVR_RenderModel>()) {
            //model.gameObject.SetActive(visible);
            foreach (var child in model.GetComponentsInChildren<MeshRenderer>())
                child.enabled = visible;
        }
    }


    void Update()
    {
        if (!Controller.GetHairTriggerDown()) {
            return;
        }

        if (objectInHand == null && collidingObject) {
            GrabObject();
        } else if (objectInHand != null) {
            ReleaseObject();
        }
    }
    
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

}
