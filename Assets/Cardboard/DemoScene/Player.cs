using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {
    public float focusTimeToEat;
    public GameObject tongueObj;


    private static float focusStartTime;

    private GameObject CurrentFocusObject;

    private Animator animator;

	private AudioSource asource;



    // Use this for initialization
    void Start () {
        focusStartTime = -1f;
        animator = GetComponentInChildren<Animator>();
		asource = GetComponent<AudioSource> ();

    }
	
	// Update is called once per frame
	void Update () {
        updateTongueDir();
        updateAI();
         
    }

 
    private void updateTongueDir()
    {
        tongueObj.transform.rotation = Cardboard.SDK.HeadPose.Orientation;
        tongueObj.transform.Rotate(85, 0, 0);
    }

    private void updateAI()
    {
        if (focusStartTime < 0)
            return;

        if (Time.time - focusStartTime > focusTimeToEat)
        {
            Tongue();
            EndFocus();
        }
    }

    public void Tongue()
    {
        focusStartTime = -1f;
		tongueObj.GetComponent<TongueScript>().SetMeshActive(true);
        animator.Play("TongueAttack");
		asource.Play ();


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
