using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float focusTimeToEat;

    private static float focusStartTime;

    private GameObject CurrentFocusObject;

    // Use this for initialization
    void Start () {
        focusStartTime = -1f;
    }
	
	// Update is called once per frame
	void Update () {
        if (focusStartTime < 0)
            return;

        if (Time.time - focusStartTime > focusTimeToEat)
        {
            Tongue();
        }
    }

    public void Tongue()
    {
        focusStartTime = -1f;
        Debug.Log("Player->Tongue");
        if (CurrentFocusObject != null)
            StartFocus();
    }

    public void StartFocus()
    {
        if(focusStartTime == -1f)
            focusStartTime = Time.time;
    }

    public void EndFocus()
    {
        focusStartTime = -1f;
        CurrentFocusObject = null;
    }

    public void SetCurrentFocusObject(GameObject go)
    {
        CurrentFocusObject = go;
    }
}
