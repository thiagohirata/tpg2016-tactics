using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Casa : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Ver MarcarcomoPosicaoPossivel
    /// </summary>
    private bool posicaoPossivel;

	// Use this for initialization
	void Start () {
	    
	}

    IEnumerator VoltarAoEstadoInativo () {
        yield return new WaitForEndOfFrame();

        while (posicaoPossivel) { 
            //uma vez é marcado como posição possível,
            //fica verificando a cada frame se ela ainda
            //é uma posição possível - e desabilita quando não
            //não for mais (ex: o status da state machine não é
            //mais selecionar movimento)
            if(!TacticsEngine.main.IsCurrentState(TacticsEngine.STATE_ESCOLHER_MOVIMENTO))
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        //mudou de estado - não é mais uma posição possível
        posicaoPossivel = false;
        transform.FindChild("IndicacaoSelecionavel").gameObject.SetActive(false);
        transform.FindChild("Indicacao").gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        GetComponent<MostraIndicacaoOnMouseHover>().enabled = false;
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
        GetComponent<Collider>().enabled = true; //com collider desabilitado por padrão, OnPointerClick não é chamado
        StartCoroutine(VoltarAoEstadoInativo());
    }


    /// <summary>
    /// Chamado quando clica na casa (ver https://docs.unity3d.com/Manual/SupportedEvents.html)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        TacticsEngine.main.SelecionarCasa(this);
    }
}
