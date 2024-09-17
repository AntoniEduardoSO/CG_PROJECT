using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform luzDirecional;
    [SerializeField][Tooltip("Duracao do dia em segundos")] private int duracaoDoDia;
    [SerializeField] private TextMeshProUGUI horarioText;

    [SerializeField] private Material skyboxMaterial;  // Adicione aqui o material do Skybox

    private float segundos;
    private float multiplicador;

    // Start is called before the first frame update
    void Start()
    {
        multiplicador = 86400 / duracaoDoDia;

        if (skyboxMaterial == null)
        {
            skyboxMaterial = RenderSettings.skybox;
        }
    }

    // Update is called once per frame
    void Update()
    {
        segundos += Time.deltaTime * multiplicador;

        if (segundos >= 86400)
        {
            segundos = 0;
        }

        ProcessarCeu();
        CalcularHorario();
    }

    private void ProcessarCeu()
    {
        float rotacaoX = Mathf.Lerp(-90, 270, segundos / 86400);
        luzDirecional.rotation = Quaternion.Euler(rotacaoX, 0, 0);

        if (rotacaoX > 90 && rotacaoX < 270)
        {
            luzDirecional.GetComponent<Light>().intensity = Mathf.Lerp(1, 0, (rotacaoX - 90) / 180);
        }
        else
        {
            luzDirecional.GetComponent<Light>().intensity = Mathf.Lerp(0, 1, (rotacaoX + 90) / 180);
        }

        // Ajustar luz ambiente (dia para noite)
        if (rotacaoX > 90 && rotacaoX < 270)
        {
            RenderSettings.ambientLight = Color.Lerp(Color.white, Color.black, (rotacaoX - 90) / 180);
        }
        else
        {
            RenderSettings.ambientLight = Color.Lerp(Color.black, Color.white, (rotacaoX + 90) / 180);
        }

        // Ajustar exposição do Skybox
        // if (skyboxMaterial.HasProperty("_Exposure"))
        // {
        //     float exposure = Mathf.Lerp(0.0035f, 0.0001f, segundos / 86400);  // Transição de exposição
        //     skyboxMaterial.SetFloat("_Exposure", exposure);
        // }
    }

    private void CalcularHorario()
    {
        horarioText.text = TimeSpan.FromSeconds(segundos).ToString(@"hh\:mm");
    }
}
