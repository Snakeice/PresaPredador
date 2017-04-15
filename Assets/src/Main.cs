using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;
using UnityEngine.SceneManagement;
using System;

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
			if (inimigo.estado == Enums.EstadoEnum.Passeio){
			Vector3 posicao = inimigo.transform.position;
			float [] PosicaoX = new float[inimigos.Count];
			float [] PosicaoZ = new float[inimigos.Count];
			float [] PosicaoY = new float[inimigos.Count];
				for (var i = 0; i < inimigos.Count; i++) {
					PosicaoX [i] = posicao.x;
					PosicaoZ [i] = posicao.z;
					PosicaoY [i] = posicao.y;
				}
				for (var j = 0; j < inimigos.Count; j++) {
					System.Random rnd = new System.Random();
					int QualPosicao;
					QualPosicao = rnd.Next(0, inimigos.Count);
					Vector3 novaPosicao = new Vector3(PosicaoX [QualPosicao], PosicaoZ [j], PosicaoY [j]);
					inimigo.transform.position = novaPosicao;
				}
			}
		}
	}


	public void restart(){
		Debug.Log ("Rec");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
