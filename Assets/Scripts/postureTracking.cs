using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARFace))]
public class postureTracking : MonoBehaviour
{

    [SerializeField]
    private GameObject posturePrefab;

    private GameObject posture;

    private ARFace arFace;

    private Text positionTrackingText;

    private Text rotationTrackingText;


    // Start is called before the first frame update
    void Awake()
    {
        positionTrackingText = GameObject.Find("position").GetComponent<Text>();
        rotationTrackingText = GameObject.Find("rotation").GetComponent<Text>();
        arFace = GetComponent<ARFace>();
    }

    void OnEnable()
    {
        ARFaceManager faceManager = FindObjectOfType<ARFaceManager>();
        if(faceManager != null && faceManager.subsystem != null && faceManager.subsystem.SubsystemDescriptor.supportsFacePose)
        {
            arFace.updated += OnUpdated;
            
        } else
        {
            
        }

    }

    void OnDisable()
    {
        arFace.updated -= OnUpdated;
        SetVisibility(false);
    }

    void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        if (arFace.transform != null && posture == null)
        {
            posture = Instantiate(posturePrefab,arFace.transform);
            posture.SetActive(false);
        }

        bool shouldBeVisible = (arFace.trackingState == TrackingState.Tracking) && (ARSession.state > ARSessionState.Ready);
        SetVisibility(shouldBeVisible);

    }

    void SetVisibility(bool isVisible)
    {
        if(posture != null)
        {
            posture.SetActive(isVisible);
        }
    }

    private void Update()
    {
        positionTrackingText.text = string.Concat("X Position: ", posture.transform.position.x.ToString("F3")," Y Position: ", posture.transform.position.y.ToString("F3")," Z Position: ", posture.transform.position.z.ToString("F3"));
        rotationTrackingText.text = string.Concat("X Rotation: ", posture.transform.rotation.x.ToString("F3"), " Y Rotation: ", posture.transform.rotation.y.ToString("F3"), " Z Rotation: ", posture.transform.rotation.z.ToString("F3"));
       
    }


}
