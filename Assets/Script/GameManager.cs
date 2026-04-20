using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // DINHEIRO
    public int dinheiro = 0;

    // CLIQUE BASE
    public int valorClique = 1;
    public int custoUpgrade = 10;

    // AUTO BASE
    public int valorAuto = 0;
    public int proximoAumentoAuto = 1;
    public int custoAuto = 25;

    // ÓLEO
    public int custoOleo = 40;
    public int bonusOleo = 2;

    // AJUDANTE
    public int custoAjudante = 60;
    public int bonusAjudante = 2;

    // BANCA MAIOR
    public int custoBancaMaior = 120;
    public int bonusBancaMaior = 5;

    // SEGUNDA FRITADEIRA
    public int custoSegundaFritadeira = 150;
    public int bonusSegundaFritadeira = 5;

    // PUBLICIDADE
    public int custoPublicidade = 200;
    public float bonusPublicidade = 1.2f;

    // RECEITA ESPECIAL
    public int custoReceitaEspecial = 300;
    public float bonusReceitaEspecial = 1.5f;

    // FESTA POPULAR
    public int custoFestaPopular = 400;
    public float multiplicadorFestaPopular = 2f;
    public float duracaoFestaPopular = 10f;

    // MULTIPLICADOR GLOBAL
    private float multiplicadorGlobal = 1f;
    private bool festaAtiva = false;

    // TEXTOS
    public TextMeshProUGUI textoDinheiro;
    public TextMeshProUGUI textoUpgrade;
    public TextMeshProUGUI textoAuto;
    public TextMeshProUGUI textoOleo;
    public TextMeshProUGUI textoAjudante;
    public TextMeshProUGUI textoBancaMaior;
    public TextMeshProUGUI textoSegundaFritadeira;
    public TextMeshProUGUI textoPublicidade;
    public TextMeshProUGUI textoReceitaEspecial;
    public TextMeshProUGUI textoFestaPopular;
    public TextMeshProUGUI textoObjetivo;

    // MENU
    public GameObject menuUpgrades;

    private bool autoAtivo = false;

    void Start()
    {
        valorAuto = 0;
        proximoAumentoAuto = 1;

        if (menuUpgrades != null)
        {
            menuUpgrades.SetActive(false);
        }

        AtualizarTexto();
    }

    // CLICAR NA FARTURA
    public void Clicar()
    {
        dinheiro += Mathf.RoundToInt(valorClique * multiplicadorGlobal);
        AtualizarTexto();
    }

    // UPGRADE BASE - MASSA
    public void ComprarUpgrade()
    {
        if (dinheiro >= custoUpgrade)
        {
            dinheiro -= custoUpgrade;
            valorClique += 1;
            custoUpgrade *= 2;
            AtualizarTexto();
        }
    }

    // AUTO BASE - FRITADEIRA
    public void ComprarAuto()
    {
        if (dinheiro >= custoAuto)
        {
            dinheiro -= custoAuto;

            valorAuto += proximoAumentoAuto;
            proximoAumentoAuto += 1;

            if (!autoAtivo)
            {
                autoAtivo = true;
                StartCoroutine(ProducaoAutomatica());
            }

            custoAuto = Mathf.RoundToInt(custoAuto * 1.8f);
            AtualizarTexto();
        }
    }

    // ÓLEO MELHOR
    public void ComprarOleo()
    {
        if (dinheiro >= custoOleo)
        {
            dinheiro -= custoOleo;
            valorClique += bonusOleo;
            custoOleo = Mathf.RoundToInt(custoOleo * 2.2f);
            AtualizarTexto();
        }
    }

    // AJUDANTE
    public void ComprarAjudante()
    {
        if (dinheiro >= custoAjudante)
        {
            dinheiro -= custoAjudante;
            valorAuto += bonusAjudante;

            if (!autoAtivo)
            {
                autoAtivo = true;
                StartCoroutine(ProducaoAutomatica());
            }

            custoAjudante = Mathf.RoundToInt(custoAjudante * 2.2f);
            AtualizarTexto();
        }
    }

    // BANCA MAIOR
    public void ComprarBancaMaior()
    {
        if (dinheiro >= custoBancaMaior)
        {
            dinheiro -= custoBancaMaior;
            valorClique += bonusBancaMaior;
            custoBancaMaior = Mathf.RoundToInt(custoBancaMaior * 2.5f);
            AtualizarTexto();
        }
    }

    // SEGUNDA FRITADEIRA
    public void ComprarSegundaFritadeira()
    {
        if (dinheiro >= custoSegundaFritadeira)
        {
            dinheiro -= custoSegundaFritadeira;
            valorAuto += bonusSegundaFritadeira;

            if (!autoAtivo)
            {
                autoAtivo = true;
                StartCoroutine(ProducaoAutomatica());
            }

            custoSegundaFritadeira = Mathf.RoundToInt(custoSegundaFritadeira * 2.5f);
            AtualizarTexto();
        }
    }

    // PUBLICIDADE NA FEIRA
    public void ComprarPublicidade()
    {
        if (dinheiro >= custoPublicidade)
        {
            dinheiro -= custoPublicidade;
            multiplicadorGlobal *= bonusPublicidade;
            custoPublicidade = Mathf.RoundToInt(custoPublicidade * 2.8f);
            AtualizarTexto();
        }
    }

    // RECEITA ESPECIAL
    public void ComprarReceitaEspecial()
    {
        if (dinheiro >= custoReceitaEspecial)
        {
            dinheiro -= custoReceitaEspecial;
            multiplicadorGlobal *= bonusReceitaEspecial;
            custoReceitaEspecial = Mathf.RoundToInt(custoReceitaEspecial * 3f);
            AtualizarTexto();
        }
    }

    // FESTA POPULAR
    public void ComprarFestaPopular()
    {
        if (dinheiro >= custoFestaPopular && !festaAtiva)
        {
            dinheiro -= custoFestaPopular;
            StartCoroutine(AtivarFestaPopular());
            custoFestaPopular = Mathf.RoundToInt(custoFestaPopular * 3.5f);
            AtualizarTexto();
        }
    }

    IEnumerator AtivarFestaPopular()
    {
        festaAtiva = true;
        multiplicadorGlobal *= multiplicadorFestaPopular;
        AtualizarTexto();

        yield return new WaitForSeconds(duracaoFestaPopular);

        multiplicadorGlobal /= multiplicadorFestaPopular;
        festaAtiva = false;
        AtualizarTexto();
    }

    // PRODUÇÃO AUTOMÁTICA
    IEnumerator ProducaoAutomatica()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            dinheiro += Mathf.RoundToInt(valorAuto * multiplicadorGlobal);
            AtualizarTexto();
        }
    }

    // ABRIR MENU
    public void AbrirMenuUpgrades()
    {
        if (menuUpgrades != null)
        {
            menuUpgrades.SetActive(true);
        }
    }

    // FECHAR MENU
    public void FecharMenuUpgrades()
    {
        if (menuUpgrades != null)
        {
            menuUpgrades.SetActive(false);
        }
    }

    // FORMATAR DINHEIRO
    string FormatarDinheiro(int valor)
    {
        if (valor < 100)
        {
            return valor + " c";
        }
        else
        {
            float euros = valor / 100f;

            if (valor % 100 == 0)
            {
                if (euros == 1)
                {
                    return euros.ToString("F0") + " euro";
                }
                else
                {
                    return euros.ToString("F0") + " euros";
                }
            }
            else
            {
                return euros.ToString("F2") + " euros";
            }
        }
    }

    // ATUALIZAR UI
    void AtualizarTexto()
    {
        textoDinheiro.text = "Dinheiro: " + FormatarDinheiro(dinheiro);

        if (textoUpgrade != null)
        {
            textoUpgrade.text =
                "Massa\nCusto: " + FormatarDinheiro(custoUpgrade) +
                "\n+1 c/click";
        }

        if (textoAuto != null)
        {
            textoAuto.text =
                "Fritadeira\nCusto: " + FormatarDinheiro(custoAuto) +
                "\n+" + proximoAumentoAuto + " c/seg";
        }

        if (textoOleo != null)
        {
            textoOleo.text =
                "Óleo Melhor\nCusto: " + FormatarDinheiro(custoOleo) +
                "\n+2 c/click";
        }

        if (textoAjudante != null)
        {
            textoAjudante.text =
                "Ajudante\nCusto: " + FormatarDinheiro(custoAjudante) +
                "\n+2 c/seg";
        }

        if (textoBancaMaior != null)
        {
            textoBancaMaior.text =
                "Banca Maior\nCusto: " + FormatarDinheiro(custoBancaMaior) +
                "\n+5 c/click";
        }

        if (textoSegundaFritadeira != null)
        {
            textoSegundaFritadeira.text =
                "2ª Fritadeira\nCusto: " + FormatarDinheiro(custoSegundaFritadeira) +
                "\n+5 c/seg";
        }

        if (textoPublicidade != null)
        {
            textoPublicidade.text =
                "Publicidade\nCusto: " + FormatarDinheiro(custoPublicidade) +
                "\n+20% global";
        }

        if (textoReceitaEspecial != null)
        {
            textoReceitaEspecial.text =
                "Receita Especial\nCusto: " + FormatarDinheiro(custoReceitaEspecial) +
                "\n+50% global";
        }

        if (textoFestaPopular != null)
        {
            if (festaAtiva)
            {
                textoFestaPopular.text =
                    "Festa Popular\nATIVA!";
            }
            else
            {
                textoFestaPopular.text =
                    "Festa Popular\nCusto: " + FormatarDinheiro(custoFestaPopular) +
                    "\nX2 por 10s";
            }
        }

        if (textoObjetivo != null)
        {
            textoObjetivo.text = "Objetivo: " + FormatarDinheiro(dinheiro);
        }
    }
}