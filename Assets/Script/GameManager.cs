using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // DINHEIRO EM CÊNTIMOS
    public int dinheiro = 0;

    // CLIQUE
    public int valorClique = 1;
    public int custoUpgrade = 10;

    // AUTO
    public int valorAuto = 0;
    public int proximoAumentoAuto = 1;
    public int custoAuto = 25;

    // NOVO UPGRADE: ÓLEO MELHOR
    public int custoOleo = 40;
    public int bonusOleo = 2;

    // NOVO UPGRADE: AJUDANTE
    public int custoAjudante = 60;
    public int bonusAjudante = 2;

    // TEXTOS
    public TextMeshProUGUI textoDinheiro;
    public TextMeshProUGUI textoUpgrade;
    public TextMeshProUGUI textoAuto;
    public TextMeshProUGUI textoOleo;
    public TextMeshProUGUI textoAjudante;
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
        dinheiro += valorClique;
        AtualizarTexto();
    }

    // UPGRADE DO CLIQUE
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

    // AUTO FRITADEIRA
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

    // PRODUÇÃO AUTOMÁTICA
    IEnumerator ProducaoAutomatica()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            dinheiro += valorAuto;
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
                    return euros.ToString("F0") + " euro";
                else
                    return euros.ToString("F0") + " euros";
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

        textoUpgrade.text =
            "Melhorar Massa\nCusto: " + FormatarDinheiro(custoUpgrade) +
            "\n+1 c por clique";

        textoAuto.text =
            "Auto Fritadeira\nCusto: " + FormatarDinheiro(custoAuto) +
            "\n+" + proximoAumentoAuto + " c/seg";

        if (textoOleo != null)
        {
            textoOleo.text =
                "Óleo Melhor\nCusto: " + FormatarDinheiro(custoOleo) +
                "\n+2 c por clique";
        }

        if (textoAjudante != null)
        {
            textoAjudante.text =
                "Ajudante\nCusto: " + FormatarDinheiro(custoAjudante) +
                "\n+2 c/seg";
        }

        if (textoObjetivo != null)
        {
            textoObjetivo.text = "Objetivo: " + FormatarDinheiro(dinheiro);
        }
    }
}