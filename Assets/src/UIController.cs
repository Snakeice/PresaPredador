using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.dp;
using Enums;
public class UIController : MonoBehaviour {
	[SerializeField]
	private GameObject gameOver;
	[SerializeField]
	private GameObject wins;
	[SerializeField]
	private Button reiniciar;
	private EstadoDoJogoEnum estadoDoJogo = EstadoDoJogoEnum.rodando;
	// Use this for initialization
	void Start () {
		EventBus.Instance.Register (this);

	}


	[Kakaroto]
	public void AtalizarEstadoDoJogo(EstadoDoJogoEnum est){
		this.estadoDoJogo = est;
		MostrarTexto ();
	}

	void OnDestroy(){
		EventBus.Instance.Unregister (this);
	}

	private void MostrarTexto(){
		switch (estadoDoJogo) {
		case EstadoDoJogoEnum.gameOver:
			gameOver.SetActive (true);
			reiniciar.gameObject.SetActive (true);
			break;
		case EstadoDoJogoEnum.wins:
			wins.SetActive (true);
			reiniciar.gameObject.SetActive (true);
			break;
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
