using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;
using UnityEngine.SceneManagement;
using System;
using Enums;

public class Main : MonoBehaviour {
	[SerializeField] public FabricaInimigo fabricaInimigo;
	[SerializeField] private int enemysNum = 4;
	[SerializeField] private int TempoMutacao = 10;
	private List<Inimigo> inimigos;
	private Genetics gen;
	private float contTempo = 0;
	private int countCruz = 0;
	private bool Caca = false;

	void Start () {
		inimigos = new List<Inimigo> ();
		gen = new Genetics ();
		inimigos = fabricaInimigo.CriarInimigos (GameObject.Find ("Chao"), enemysNum * 10);
		fabricaInimigo.ManterAtivo (inimigos, enemysNum);
		EventBus.Instance.Register (this);
		EventBus.Instance.Post (inimigos);
	}
	
	void Update () {
		contTempo += Time.deltaTime;
		if ((contTempo >= TempoMutacao) && !Caca) {
			if (countCruz > TempoMutacao) {
				gen.Cruzamento (inimigos);
			} else {
				gen.Mutacao (inimigos);
				countCruz++;
			}
			contTempo = 0;
		}
	}
	[Kakaroto]
	public void AtualizaTag(EstadoEnum estado){
		this.Caca = estado == EstadoEnum.Caca || estado == EstadoEnum.cacaOK;
	}

	[Kakaroto]
	public void StopGame(EstadoDoJogoEnum estado){
		if (estado != EstadoDoJogoEnum.rodando) {
			foreach (GameObject ga in GameObject.FindGameObjectsWithTag("Inimigo")) {
				DestroyImmediate (ga);
			}
		}
	}



	public void restart(){
		Debug.Log ("Rec");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
