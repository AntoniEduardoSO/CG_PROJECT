using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInspector : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 3f;
    public Text infoText;  // Texto para exibir informações na aba
    public GameObject panel;  // Painel da aba

    public FirstPersonController firstPersonController;  // Referência ao FirstPersonController

    private GameObject currentObject;

    void Start()
    {
        // Inicialmente, desative o painel
        panel.SetActive(false);
    }

    void Update()
    {
        // Verifica se o jogador está em modo de inspeção
        if (firstPersonController != null && firstPersonController.isInspecting)
        {

            if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionRange))
            {
                if (hit.transform.CompareTag("Inspectable"))
                {
                    ShowPanel(hit.transform.gameObject);
                }
            }
        }
        else if (currentObject != null)
        {
            HidePanel();  // Garante que o painel seja escondido se não estiver em inspeção
        }
    }

    private void ShowPanel(GameObject obj)
    {
        if (currentObject != obj)
        {
            currentObject = obj;
            panel.SetActive(true);  // Mostre o painel

            ShowDetails(currentObject.name);
        }
    }

    private void ShowDetails(string name)
    {
        if(name.ToLower().Contains("donut"))
        {
            infoText.text = "<size=60><b>Donut do Marcelo!</b></size>\n\n";
            infoText.text += "<i><color=red>Tomar cuidado para não comer o Donut do Marcelo.</color></i>\n";
            infoText.text += "Dizem as más línguas que quem comer seu donut terá 10 anos de sofrimento!";
        } else{
            infoText.text = "<size=60> MissingNo</size>\n\n";
            infoText.text += "Calma la paizão, não era nem para você ver interagir com isso, vai tomar ban";
        }
    }

    private void HidePanel()
    {
        panel.SetActive(false);  // Oculte o painel
        currentObject = null;
    }
}
