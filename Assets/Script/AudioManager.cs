using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public TextMeshProUGUI textoBotao;

    private bool somLigado = true;

    void Start()
    {
        // Carregar estado guardado
        somLigado = PlayerPrefs.GetInt("Som", 1) == 1;

        AtualizarSom();
    }

    public void ToggleSom()
    {
        somLigado = !somLigado;

        PlayerPrefs.SetInt("Som", somLigado ? 1 : 0);

        AtualizarSom();
    }

    void AtualizarSom()
    {
        AudioListener.volume = somLigado ? 1f : 0f;

        if (textoBotao != null)
        {
            textoBotao.text = somLigado ? "Som: ON" : "Som: OFF";
        }
    }
}