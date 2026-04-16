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

    // AUTO FRITADEIRA
    public int valorAuto = 0;
    public int proximoAumentoAuto = 1;
    public int custoAuto = 25;

    // TEXTOS
    public TextMeshProUGUI textoDinheiro;
    public TextMeshProUGUI textoUpgrade;
    public TextMeshProUGUI textoAuto;
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

    // CONVERTER CÊNTIMOS PARA TEXTO
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
                return euros.ToString("F0") + " euro";
            }
            else
            {
                return euros.ToString("F2") + " euros";
            }
        }
    }

    // ATUALIZAR TEXTOS
    void AtualizarTexto()
    {
        textoDinheiro.text = "Dinheiro: " + FormatarDinheiro(dinheiro);

        textoUpgrade.text =
            "Upgrade Clique\nCusto: " + FormatarDinheiro(custoUpgrade) +
            "\n+" + valorClique + " c por clique";

        textoAuto.text =
            "Auto Fritadeira\nCusto: " + FormatarDinheiro(custoAuto) +
            "\n+" + proximoAumentoAuto + " c/seg";

        if (textoObjetivo != null)
        {
            textoObjetivo.text = "Objetivo: " + FormatarDinheiro(dinheiro);
        }
    }
}