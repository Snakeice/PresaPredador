using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;

public class Main : MonoBehaviour {
	[SerializeField] public FabricaInimigo fabricaInimigo;
	public int enemysNum;
	public float enemyVel;
	public float alvVel;

	private List<Inimigo> inimigos;

	void Start () {
		inimigos = new List<Inimigo> ();

		do {
			inimigos.Add (fabricaInimigo.criaSobreObj (GameObject.Find ("Chao")));
		} while(inimigos.Count < enemysNum);
		EventBus.Instance.Post (inimigos);
	}
	
	void Update () {
		//EventBus.Instance.Post (inimigos);
	}

}
