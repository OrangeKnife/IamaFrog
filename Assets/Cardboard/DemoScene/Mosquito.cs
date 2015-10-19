using UnityEngine;
using System.Collections;

public class Mosquito : MonoBehaviour {
    public float circlingSpeed;
    public int difficultyValue = 0;

    private Player playerRef;
    // Use this for initialization
    private Vector3 targetPos;
    private float movingSpeed = 30;
    private int circlingDir = 1;
    

    void Start () {
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        circlingDir = (Random.value > 0.5f ? 1 : -1);
    }
	
	// Update is called once per frame
	void Update () {
        updateTargetPos();
        updateMovement();
    }

    void updateTargetPos()
    {
        Quaternion qot = new Quaternion();
        qot.eulerAngles = new Vector3(0, circlingDir * Time.deltaTime * circlingSpeed, 0);
        targetPos = playerRef.transform.position + qot * (gameObject.transform.position - playerRef.transform.position);
    }

    void updateMovement()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * movingSpeed);
    }

    public void StartFocus()
    {
        if (difficultyValue > 0)
        {
            circlingDir = (Random.value > 0.5f ? 1 : -1);
            difficultyValue--;
        }

        playerRef.StartFocus();
        playerRef.SetCurrentFocusObject(gameObject);
    }

    public void EndFocus()
    {
        playerRef.EndFocus();
    }
}
