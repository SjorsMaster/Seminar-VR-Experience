using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Followed a small tut on https://www.youtube.com/watch?v=cxRnK8aIUSI
/// </summary>

public class TPManager : MonoBehaviour
{
    [SerializeField]
    InputActionAsset _actionAsset;
    [SerializeField]
    XRRayInteractor _rayInteractor;
    [SerializeField]
    TeleportationProvider _tpProvider;

    InputAction _thumbStick;
    Vector2 _thumbstickDirection;
    bool _isActive;

    // Start is called before the first frame update
    void Start()
    {

        _rayInteractor.enabled = false;

        var activate = _actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += onTeleportActivate;

        var cancel = _actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += onTeleportCancel;

        _thumbStick = _actionAsset.FindActionMap("XRI LeftHand").FindAction("Move");
        _thumbStick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;

        if (_thumbStick.triggered)
        {
            var thumbstickValue = _thumbStick.ReadValue<Vector2>();
            if (thumbstickValue.sqrMagnitude >= 1)
            {
                _thumbstickDirection = thumbstickValue.normalized;
            }
            return;
        }

        if (!_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            reset();
            return;
        }

        if (hit.transform.tag != "ground") {
            reset();
            return;
        }

        TeleportRequest rq = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        _tpProvider.QueueTeleportRequest(rq);
        reset();
    }

    private void reset()
    {
        _rayInteractor.enabled = false;
        _isActive = false;
        _thumbstickDirection = Vector2.zero;
    }

    void onTeleportActivate(InputAction.CallbackContext context)
    {
        _rayInteractor.enabled = true;
        _isActive = true;
    }
    void onTeleportCancel(InputAction.CallbackContext context)
    {
        reset();
    }
}
