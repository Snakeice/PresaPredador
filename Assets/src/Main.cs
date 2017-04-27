using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;
using UnityEngine.SceneManagement;
using System;

public class Main : MonoBehaviour {
	[SerializeField] public FabricaInimigo fabricaInimigo;
	[SerializeField] private int enemysNum = 4;
	[SerializeField] private float tempoMutacao = 4;
	private List<Inimigo> inimigos;

	void Start () {
		inimigos = new List<Inimigo> ();

		do {
			inimigos.Add (fabricaInimigo.criaSobreObj (GameObject.Find ("Chao")));
		} while(inimigos.Count < enemysNum);
		EventBus.Instance.Post (inimigos);
	}

	float contadorTempo = 0;
	
	void Update () {
		

			contadorTempo += Time.deltaTime;
			if (contadorTempo >= tempoMutacao) {
		foreach (Inimigo inimigo in inimigos) {			
				float[] PosicaoX = new float[inimigos.Count];
				float[] PosicaoZ = new float[inimigos.Count];
				float[] PosicaoY = new float[inimigos.Count];
				if (inimigo.estado == Enums.EstadoEnum.Passeio) {
					for (var i = 0; i < inimigos.Count; i++) {
						Vector3 posicao = inimigos [i].transform.position;
						PosicaoX [i] = posicao.x;
						PosicaoY [i] = posicao.y;
						PosicaoZ [i] = posicao.z;
					}
							System.Random rnd = new System.Random ();
							int QualInimigo;
							int QualXZ;
							int QualInimigoTroca;
							QualXZ = rnd.Next (0, 1);
							QualInimigo = rnd.Next (0, inimigos.Count);
							QualInimigoTroca = rnd.Next (0, inimigos.Count);
							if (QualXZ == 0) {
								Vector3 novaPosicao = new Vector3 (PosicaoX [QualInimigoTroca], PosicaoY [QualInimigo], PosicaoZ [QualInimigo]);	
								inimigos[QualInimigo].transform.position = novaPosicao;
							} else {
								Vector3 novaPosicao = new Vector3 (PosicaoX [QualInimigo], PosicaoY [QualInimigo], PosicaoZ [QualInimigoTroca]);
								inimigos [QualInimigo].transform.position = novaPosicao;
							}
						contadorTempo = 0;					
				}
			}
		}
	}


	public void restart(){
		Debug.Log ("Rec");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
