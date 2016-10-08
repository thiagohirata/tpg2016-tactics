using UnityEngine;
using System.Collections.Generic;

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
    public IDictionary<Vector3, Casa> tabuleiro;

    /// <summary>
    /// As próximas variáveis indicam o status do movimento de personagem atual.
    /// </summary>
    public Personagem personagemSelecionado;
    /// <summary>
    /// Guarda posição inicial do personagem
    /// </summary>
    public Vector3 posicaoOriginal;
    /// <summary>
    /// Guarda posições selecionáveis
    /// </summary>
    public List<Casa> posicoesSelecionaveis;
    /// <summary>
    /// Guarda o comando selecionado
    /// </summary>
    public Comando comandoSelecionado;
    /// <summary>
    /// Guarda posições que podem ser atacadas a partir do ataque selecionado
    /// </summary>
    public List<Casa> alvosSelecionaveis;

    void Awake()
    {
        stateMachine = GetComponent<Animator>();

        if (TacticsEngine.main == null)
        {
            TacticsEngine.main = this;
        } else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //busca no cenário todas as casas (objetos que tem o componente Casa)
        Casa[] casas = FindObjectsOfType<Casa>();

        //Monta o dicionário this.tabuleiro - vai ajudar a achar as casas
        //a partir de uma posição (ex: quero habilitar a casa na posição
        //4, 2)
        //Atenção! Se a casa estiver em uma posição não inteira (ex: 1.0000000001),
        //vai dar errado!
        tabuleiro = new Dictionary<Vector3, Casa>();
        foreach (Casa c in casas)
        {
            if (tabuleiro.ContainsKey(c.transform.position))
            {
                Debug.LogError("Tem duas casas na posição " + c.transform.position);
            } else
            {
                tabuleiro.Add(c.transform.position, c);
            }

        }
    }

    void Update()
    {
        //cancelar operação atual (pode ser, por exemplo, cancelar a seleção de um personagem)
        if (Input.GetButtonDown("Cancel"))
        {
            if (IsCurrentState(STATE_ESCOLHER_MOVIMENTO))
            {
                if (personagemSelecionado != null)
                {
                    personagemSelecionado.transform.FindChild("Indicacao").gameObject.SetActive(false);
                    personagemSelecionado = null;
                }

            }
            stateMachine.SetTrigger("Cancelar");

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
        if (IsCurrentState(STATE_ESCOLHER_PERSONAGEM) && personagem.time == this.timeAtual)
        {
            this.personagemSelecionado = personagem;
            this.posicoesSelecionaveis = new List<Casa>();
            this.posicaoOriginal = personagem.transform.position;

            //TODO: calcular as Casas de destino possíveis para o personagem
            //vou deixar bem simples, por enquanto - pode andar 1 posição

            //lembre que estamos pegando a posição da unity, mas vamos
            //só considerar o pos.x e o pos.z
            Vector3 posicaoDoPersonagem = personagem.transform.position;
            Vector3[] posicoesPossiveis = new Vector3[] { posicaoDoPersonagem + new Vector3(1, 0, 0),
                posicaoDoPersonagem + new Vector3(-1, 0, 0),
                posicaoDoPersonagem + new Vector3(0, 0, 1),
                posicaoDoPersonagem + new Vector3(0, 0, -1)};

            //agora, habilitar as casas das posições possíveis
            foreach (Vector3 posicaoPossivel in posicoesPossiveis)
            {
                if (tabuleiro.ContainsKey(posicaoPossivel))
                {
                    Casa casa = tabuleiro[posicaoPossivel];
                    casa.MarcarComoPosicaoPossivel();
                    posicoesSelecionaveis.Add(casa);
                }
            }

            stateMachine.SetTrigger("PersonagemSelecionado");
        }
    }

    public void VoltarSelecionarMovimento()
    {
        //restaura modo selecionar movimento
        personagemSelecionado.transform.position = posicaoOriginal;
        //agora, habilitar as casas das posições possíveis
        foreach (Casa casa in posicoesSelecionaveis)
        {
            casa.MarcarComoPosicaoPossivel();
        }
    }

    public void SelecionarCasa(Casa casa)
    {
        if (IsCurrentState(STATE_ESCOLHER_MOVIMENTO))
        {
            //"movimenta" o personagem para o destino
            personagemSelecionado.transform.position = casa.transform.position;

            //avança o state machine
            stateMachine.SetTrigger("MovimentoSelecionado");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SelecionarAcao(int indiceAcao)
    {
        stateMachine.SetTrigger("AcaoSelecionada");
    }
}
