using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TongueScript : MonoBehaviour {

    private List<Mosquito> mosquitoes = new List<Mosquito>();
	// Use this for initialization
	void Start () {
	
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
        for(int i = mosquitoes.Count - 1; i >= 0; i--)
        {
            mosquitoes[i].beforeDie();
            Destroy(mosquitoes[i].gameObject);
        }

        mosquitoes.Clear();
    }
}
