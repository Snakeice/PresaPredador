using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.dp;
using Enums;
public class FimDeJogoController : MonoBehaviour {
	[SerializeField]
	private GameObject gameOver;
	[SerializeField]
	private GameObject wins;
	private EstadoDoJogoEnum estadoDoJogo;
	// Use this for initialization
	void Start () {
		EventBus.Instance.Register (this);
	}


	[Kakaroto]
	public void AtalizarEstadoDoJogo(EstadoDoJogoEnum est){
		this.estadoDoJogo = est;
	}

	void OnDestroy(){
		EventBus.Instance.Unregister (this);
	}
	private void MostrarTexto(){
		switch (estadoDoJogo) {
		case EstadoDoJogoEnum.gameOver:
			gameOver.SetActive( true);
		case EstadoDoJogoEnum.wins:
			wins.SetActive (true);
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
