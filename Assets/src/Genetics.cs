using System;
using System.Collections.Generic;
using UnityEngine;

public class Genetics
	{
	static System.Random rnd = new System.Random();
		public Genetics ()
		{
		}


	public void Mutacao(List<Inimigo> inimigos){
		Debug.Log ("Mutação iniciada..");
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
			}
		}
	}

	private Vector3 GetPosicaoCruzamento(Vector3[] vectors){
		Vector3 sum = Vector3.zero;
		foreach(Vector3 vec in vectors){
			sum += vec;
		}
		return sum / vectors.Length;
	}

	public void Cruzamento(List<Inimigo> inimigos){
		List<Inimigo> ativos = new List<Inimigo> ();
		foreach (Inimigo inimigo in inimigos) {
			if (inimigo.gameObject.activeSelf) {
				ativos.Add (inimigo);
			}
		}
		foreach (Inimigo mae in ativos) {
				int index = rnd.Next (inimigos.Count);
				do {
					index = rnd.Next (inimigos.Count);
				} while(inimigos [index].gameObject.activeSelf);
				Inimigo pai = inimigos [index];
			pai.transform.position = GetPosicaoCruzamento(new Vector3[] {pai.transform.position, mae.transform.position});
			pai.gameObject.SetActive (true);
			inimigos.Remove (mae);
			mae.Destruir ();
		}
	}
}
