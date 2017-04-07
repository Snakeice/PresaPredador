 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils.dp;

public class FabricaInimigo : MonoBehaviour{
	private Vector3 localDeConstrucao;
	public GameObject inimigo;

	public void cria ()	{
		Instantiate (inimigo, localDeConstrucao, Quaternion.identity);
	}

	public Inimigo criaSobreObj (GameObject go)	{

		Bounds bounds = go.GetComponent<MeshFilter>().mesh.bounds;
		float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
		float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;
		Vector3 newVec = new Vector3(Random.Range (minX, -minX), 0.8f, Random.Range (minZ, -minZ));


		GameObject enemyObj = Instantiate (inimigo, newVec, Quaternion.identity);
		return enemyObj.GetComponent<Inimigo>();
	}

	public void SetLocalDeConstrucao(Vector3 localDeConstrucao)	{
		this.localDeConstrucao = localDeConstrucao;
	}

}
