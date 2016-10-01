using UnityEngine;
using System.Collections;

public class Casa : MonoBehaviour {
    /// <summary>
    /// Ver MarcarcomoPosicaoPossivel
    /// </summary>
    private bool posicaoPossivel;

	// Use this for initialization
	void Start () {
	    
	}
	
	/// <summary>
    /// Esta solução parece que funciona, mas não funciona!
    /// </summary>
	void Update () {
        if(posicaoPossivel)
        {
            //uma vez é marcado como posição possível,
            //fica verificando a cada frame se ela ainda
            //é uma posição possível - e desabilita quando não
            //não for mais (ex: o status da state machine não é
            //mais selecionar movimento)
            if(!TacticsEngine.main.IsCurrentState(TacticsEngine.STATE_ESCOLHER_MOVIMENTO))
            {
                //mudou de estado - não é mais uma posição possível
                posicaoPossivel = false;
                transform.FindChild("IndicacaoSelecionavel").gameObject.SetActive(false);
                GetComponent<MostraIndicacaoOnMouseHover>().enabled = false;
            }
        }
	}

    /// <summary>
    /// Chamado por TacticsEngine quando determinar que esta casa é
    /// uma das posições de destino possíveis para o personagem
    /// </summary>
    public void MarcarComoPosicaoPossivel()
    {
        posicaoPossivel = true;
        transform.FindChild("IndicacaoSelecionavel").gameObject.SetActive(true);
        GetComponent<MostraIndicacaoOnMouseHover>().enabled = true;
    }
}
