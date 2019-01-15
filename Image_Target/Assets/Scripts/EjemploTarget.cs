using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class EjemploTarget : MonoBehaviour , ITrackableEventHandler
{

    protected TrackableBehaviour mTrackableBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //esta funcion se encarga de la deteccion del target
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {

        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            print("Detecta");
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            print("Pierde");
        }
        else
        {
            print("Comenzo");
        }
    }


}
