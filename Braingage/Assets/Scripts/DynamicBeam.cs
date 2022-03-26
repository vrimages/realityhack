using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class DynamicBeam : MonoBehaviour
{

    [SerializeField]
    private MLControllerConnectionHandlerBehavior _controllerConnectionHandler = null;



    //private MLInput.Controller _controller;
    private LineRenderer beamLine;
    public Color startColor;
    public Color endColor;
    // Start is called before the first frame update
    void Start()
    {
        //_controller = MLInput.GetController(MLInput.Hand.Left);
        beamLine = GetComponent<LineRenderer>();
        beamLine.startColor = startColor;
        beamLine.endColor = endColor;
    }

    // Update is called once per frame
    void Update()
    {

        if (!_controllerConnectionHandler.IsControllerValid())
        {
            return;
        }

        #if PLATFORM_LUMIN
        MLInput.Controller _controller = _controllerConnectionHandler.ConnectedController;

        transform.position = _controller.Position;
        transform.rotation = _controller.Orientation;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            beamLine.useWorldSpace = true;
            beamLine.SetPosition(0, transform.position);
            beamLine.SetPosition(1, hit.point);
        }
        else
        {
            beamLine.useWorldSpace = true;
            beamLine.SetPosition(0, transform.position);
            beamLine.SetPosition(1, transform.forward * 5);
        }
        #endif
    }
}

