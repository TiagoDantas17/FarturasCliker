using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // DINHEIRO
    public float dinheiro = 0f;

    // CLIQUE
    public float valorClique = 1f;
    public float custoUpgrade = 50f;

    // AUTO CLICKER
    public float valorAuto = 0f;
    public float proximoAumentoAuto = 0.1f;
    public float custoAuto = 25f;

    // TEXTOS
    public TextMeshProUGUI textoDinheiro;
    public TextMeshProUGUI textoUpgrade;
    public TextMeshProUGUI textoAuto;

    // MENU
    public GameObject menuUpgrades;

    private bool autoAtivo = false;

    void Start()
    {
        valorAuto = 0f;
        proximoAumentoAuto = 0.1f;

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
            valorClique += 1f;
            custoUpgrade *= 2.0f;
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
            proximoAumentoAuto += 0.1f;

            if (!autoAtivo)
            {
                autoAtivo = true;
                StartCoroutine(ProducaoAutomatica());
            }

            custoAuto *= 1.8f;
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

    // ATUALIZAR TEXTOS
    void AtualizarTexto()
    {
        textoDinheiro.text = "Dinheiro: " + dinheiro.ToString("F1") + "€";

        textoUpgrade.text =
            "Upgrade Clique\nCusto: " + custoUpgrade.ToString("F1") +
            "€\n+" + valorClique.ToString("F1") + " por clique";

        textoAuto.text =
            "Auto Fritadeira\nCusto: " + custoAuto.ToString("F1") +
            "€\n+" + proximoAumentoAuto.ToString("F1") + "/seg";
    }
}