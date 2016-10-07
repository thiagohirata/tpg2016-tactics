using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class Personagem : MonoBehaviour, IPointerClickHandler {
    public TacticsEngine.Time time;
    private TacticsEngine tacticsEngine;
    private MostraIndicacaoOnMouseHover mostraIndicacaoOnMouseHover;
    public ClassePersonagem classePersonagem;

    // Use this for initialization
    void Start () {
        this.tacticsEngine = TacticsEngine.main;
        this.mostraIndicacaoOnMouseHover = GetComponent<MostraIndicacaoOnMouseHover>();
    }
	
	// Update is called once per frame
	void Update () {
        mostraIndicacaoOnMouseHover.enabled  = 
            tacticsEngine.IsCurrentState(TacticsEngine.STATE_ESCOLHER_PERSONAGEM) && this.time == tacticsEngine.timeAtual;

    }

    /// <summary>
    /// Chamado quando clica no personagem (ver https://docs.unity3d.com/Manual/SupportedEvents.html)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        tacticsEngine.SelecionarPersonagem(this);
    }
}
