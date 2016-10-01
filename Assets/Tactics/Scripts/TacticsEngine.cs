using UnityEngine;
using System.Collections;

public class TacticsEngine : MonoBehaviour {
    public const string STATE_ESCOLHER_PERSONAGEM = "EscolherPersonagem";
    public const string STATE_ESCOLHER_MOVIMENTO = "EscolherMovimento";
    public enum Time
    {
        Jogador,
        NPCInimigo
    }

    public static TacticsEngine main;
    private Animator stateMachine;
    public Time timeAtual;


    void Awake()
    {
        stateMachine = GetComponent<Animator>();

        if(TacticsEngine.main == null)
        {
            TacticsEngine.main = this;
        } else
        {
            Destroy(this);
        }
    }
    
    /// <summary>
    /// Usar este método para determinar qual o estado atual da máquina de estados
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool IsCurrentState(string state) 
    {
        return stateMachine.GetCurrentAnimatorStateInfo(0).IsTag(state);
    }

    /// <summary>
    /// Chamar este método após o clique em um personagem
    /// </summary>
    /// <param name="personagem"></param>
    public void SelecionarPersonagem(Personagem personagem)
    {
        if(IsCurrentState(STATE_ESCOLHER_PERSONAGEM) && personagem.time == this.timeAtual)
        {
            stateMachine.SetTrigger("PersonagemSelecionado");

            //TODO: calcular as Casas de destino possíveis para o personagem

        }
    }
}
