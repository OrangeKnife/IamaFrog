using UnityEngine;
using System.Collections;

public class Mosquito : MonoBehaviour {
	public AudioClip scoreSound;
    public float circlingSpeed;
    public int difficultyValue = 0;

    private Player playerRef;
    // Use this for initialization
    private Vector3 targetPos;
    private float movingSpeed = 30;
    private int circlingDir = 1;
    private bool bCought;

    private Selectable selectableScript;
	private AudioSource asource;

    void Start () {
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        circlingDir = (Random.value > 0.5f ? 1 : -1);

        selectableScript = gameObject.GetComponent<Selectable>();
		asource = gameObject.GetComponent<AudioSource> ();
    }
	
	// Update is called once per frame
	void Update () {
        if (bCought)
            return;

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

        selectableScript.selectBy(playerRef.gameObject);

    }

    public void EndFocus()
    {
        playerRef.EndFocus();

        selectableScript.deSelect();
    }

    public void caught(GameObject byTongue)
    {
        bCought = true;
        gameObject.transform.parent = byTongue.transform;
    }

    public void beforeDie()
    {
		asource.clip = scoreSound;
		asource.loop = false;
		asource.Play();
    }

	public void Die(float time)
	{
		StartCoroutine (SelfDestruction (time));
	}

	IEnumerator SelfDestruction(float sec)
	{
		yield return new WaitForSeconds(sec);
		Spawner.currentSpawned --;
		Destroy (gameObject);
	}

	public void tap()
	{
		playerRef.Tongue ();
	}
}
