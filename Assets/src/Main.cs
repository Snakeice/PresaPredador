using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
	[SerializeField] public FabricaInimigo fabricaInimigo;
	[SerializeField] private int enemysNum = 4;
	private List<Inimigo> inimigos;

	void Start () {
		inimigos = new List<Inimigo> ();

		do {
			inimigos.Add (fabricaInimigo.criaSobreObj (GameObject.Find ("Chao")));
		} while(inimigos.Count < enemysNum);
		EventBus.Instance.Post (inimigos);
	}
	
	void Update () {
		foreach (Inimigo inimigo in inimigos) {
			Vector3 posicao = inimigo.transform.position;

		}
	}


	public void restart(){
		Debug.Log ("Rec");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
