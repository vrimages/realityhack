using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlanesDemo : MonoBehaviour
{
    /*
    #region Public Variables
    public GameObject Camera;
    public Material FocusedMaterial, NonFocusedMaterial;
    #endregion

    #region Private Variables
    private Vector3 _heading;
    private MeshRenderer _meshRenderer;
    #endregion

    #region Unity Methods

    */



    public GameObject blueCyl;

    public Transform BBoxTransform;
    public Vector3 BBoxExtents;
    public GameObject PlaneGameObject;

    private MLPlanes.QueryParams _queryParams = new MLPlanes.QueryParams();
    public MLPlanes.QueryFlags QueryFlags;


    private float timeout = 5f;
    private float timeSinceLastRequest = 0f;
    private List<GameObject> _planeCache = new List<GameObject>();
    private List<GameObject> _cylCache = new List<GameObject>();

    void Start()
    {
        /*MLEyes.Start();
        transform.position = Camera.transform.position + Camera.transform.forward * 2.0f;
		// Get the meshRenderer component
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        */



        MLPlanes.Start();
    }

    /*
    private void OnDisable() {
        MLEyes.Stop();
    }
    */

    private void OnDestroy()
    {
        MLPlanes.Stop();
    }


    void Update()
    {
        timeSinceLastRequest += Time.deltaTime;
        if (timeSinceLastRequest > timeout)
        {
            timeSinceLastRequest = 0f;
            RequestPlanes();
        }

        /*
        if (MLEyes.IsStarted) {
            RaycastHit rayHit;
            _heading = MLEyes.FixationPoint - Camera.transform.position;
            // Use the proper material
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 10.0f)) {
                //_meshRenderer.material = FocusedMaterial;
            }
            else {
                _meshRenderer.material = NonFocusedMaterial; 
            }
        }*/
    }


    void RequestPlanes()
    {
        _queryParams.Flags = QueryFlags;
        _queryParams.MaxResults = 100;
        _queryParams.BoundsCenter = BBoxTransform.position;
        _queryParams.BoundsRotation = BBoxTransform.rotation;
        _queryParams.BoundsExtents = BBoxExtents;

        MLPlanes.GetPlanes(_queryParams, HandleOnReceivedPlanes);
    }

    private void HandleOnReceivedPlanes(MLResult result, MLPlanes.Plane[] planes, MLPlanes.Boundaries[] boundaries)
    {
       for (int i = _planeCache.Count - 1; i >= 0; --i)
       {
           Destroy(_planeCache[i]);
           _planeCache.Remove(_planeCache[i]);
       }

       GameObject newPlane;
       for (int i = 0; i < planes.Length; ++i)
       {
           newPlane = Instantiate(PlaneGameObject);
           newPlane.transform.position = planes[i].Center;
           newPlane.transform.rotation = planes[i].Rotation;
           newPlane.transform.localScale = new Vector3(planes[i].Width, planes[i].Height, 1f); // Set plane scale
           _planeCache.Add(newPlane);
       }

       GenerateCyls(planes);
    }


    void GenerateCyls(MLPlanes.Plane[] planes){
        GameObject thisBlueCyl;

        for(int i =0; i < 5; i++){
            thisBlueCyl = Instantiate(blueCyl);
            thisBlueCyl.transform.position = planes[i].Center;
            thisBlueCyl.transform.rotation = planes[i].Rotation;
            _cylCache.Add(thisBlueCyl);
        }
    }

    //#endregion
}