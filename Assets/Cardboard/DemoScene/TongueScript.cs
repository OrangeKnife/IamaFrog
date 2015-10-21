using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TongueScript : MonoBehaviour {
    private List<Mosquito> mosquitoes = new List<Mosquito>();
	public Text scoreText;

	private int currentScore = 0;
	private MeshRenderer mr;
	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer> ();
		mr.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        Mosquito aMosquito = other.gameObject.GetComponent<Mosquito>();
        if (aMosquito != null)
        {
            aMosquito.caught(gameObject);
            mosquitoes.Add(aMosquito);
        }

    }

    public void eatMosquitoes()
    {
		StartCoroutine (eatOneByOne ());
    }

	IEnumerator eatOneByOne()
	{
		for (int i = mosquitoes.Count - 1; i >= 0; i--) {
			yield return new WaitForSeconds(0.2f);
			mosquitoes [i].beforeDie ();
			mosquitoes [i].Die (.5f);
			currentScore++;
			//scoreText.text = "SCORE:" + currentScore.ToString();
			
		}
		
		mosquitoes.Clear ();

		mr.enabled = false;
	}

	public void SetMeshActive(bool active)
	{
		mr.enabled = active;
	}
}
