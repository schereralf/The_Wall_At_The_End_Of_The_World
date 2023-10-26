using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int rayScope = 5;
    [SerializeField] private LayerMask layerMaskInteraction;
    [SerializeField] private string excludedLayerNames = null;

    private DoorController raycastedThingy;

    [SerializeField] private KeyCode openDoor = KeyCode.Mouse0;
    [SerializeField] private Image target = null;
    private bool isTargetActive;
    private bool doOnce;

    private const string interactingTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludedLayerNames) | layerMaskInteraction.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayScope, mask))
        {
            if (hit.collider.CompareTag(interactingTag))
            {
                if (!doOnce)
                {
                    raycastedThingy = hit.collider.gameObject.GetComponent<DoorController>();
                    TargetChange(true);
                }
                isTargetActive = true;
                doOnce = true;


                if (Input.GetKeyDown(openDoor))
                {
                    raycastedThingy.PlayAnimation();
                }
            }
        }

        else
        {
            if(isTargetActive)
            {
                TargetChange(false);
                doOnce = false;
            }
        }
    }

    void TargetChange(bool on)
    {
        if(on && !doOnce)
        {
            target.color = Color.red;
        }
        else 
        {
            target.color = Color.white;
            isTargetActive = false;
        }
    }
}
