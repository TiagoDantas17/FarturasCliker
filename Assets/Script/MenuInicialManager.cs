using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicialManager : MonoBehaviour
{
    [Header("Painel de Definições")]
    public GameObject menuDefinicoes;

    [Header("Menu Principal")]
    public GameObject botaoJogar;
    public GameObject botaoDefinicoes;
    public GameObject botaoSair;
    public GameObject titulo;

    [Header("Sliders")]
    public Slider sliderVolume;
    public Slider sliderBrilho;

    [Header("Overlay de Brilho")]
    public Image overlayBrilho;

    void Start()
    {
        if (menuDefinicoes != null)
        {
            menuDefinicoes.SetActive(false);
        }

        float volumeGuardado = PlayerPrefs.GetFloat("volume", 1f);
        float brilhoGuardado = PlayerPrefs.GetFloat("brilho", 1f);

        if (brilhoGuardado < 0.3f)
        {
            brilhoGuardado = 0.3f;
        }

        if (sliderVolume != null)
        {
            sliderVolume.value = volumeGuardado;
        }

        if (sliderBrilho != null)
        {
            sliderBrilho.value = brilhoGuardado;
        }

        AtualizarVolume(volumeGuardado);
        AtualizarBrilho(brilhoGuardado);
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("Jogo fechado.");
    }

    public void AbrirDefinicoes()
    {
        if (menuDefinicoes != null)
        {
            menuDefinicoes.SetActive(true);
        }

        if (botaoJogar != null) botaoJogar.SetActive(false);
        if (botaoDefinicoes != null) botaoDefinicoes.SetActive(false);
        if (botaoSair != null) botaoSair.SetActive(false);
        if (titulo != null) titulo.SetActive(false);
    }

    public void FecharDefinicoes()
    {
        if (menuDefinicoes != null)
        {
            menuDefinicoes.SetActive(false);
        }

        if (botaoJogar != null) botaoJogar.SetActive(true);
        if (botaoDefinicoes != null) botaoDefinicoes.SetActive(true);
        if (botaoSair != null) botaoSair.SetActive(true);
        if (titulo != null) titulo.SetActive(true);
    }

    public void AtualizarVolume(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("volume", valor);
        PlayerPrefs.Save();
    }

    public void AtualizarBrilho(float valor)
    {
        if (valor < 0.3f)
        {
            valor = 0.3f;
        }

        if (overlayBrilho != null)
        {
            Color cor = overlayBrilho.color;
            cor.a = 1f - valor;
            overlayBrilho.color = cor;
        }

        PlayerPrefs.SetFloat("brilho", valor);
        PlayerPrefs.Save();
    }
}