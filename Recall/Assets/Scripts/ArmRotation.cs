using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 90;
    public float rotZ;
    Jogador jogador;

    // Update is called once per frame


    private void Start()
    {
        jogador = GetComponentInParent<Jogador>();
    }

    void FixedUpdate() {
        // subtraindo a posição do jogador da posição do mouse
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();     // normalizando o vetor. Significa que toda a soma do vetor será igual a 1


        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;   // Encontrar o ângulo em graus
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

        //print(rotZ);


        if (jogador.lado == Jogador.Lado.DIREITA)
        {
            if (rotZ > 45)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 45 + rotationOffset);
            }

            else if (rotZ < -45)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -45 + rotationOffset);
            }
        }


        if (jogador.lado == Jogador.Lado.ESQUERDA)
        {
            if (rotZ > 0 && rotZ < 135)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 135 + rotationOffset);

            }

            else if (rotZ <= 0 && rotZ > -135)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -135 + rotationOffset);
            }
        }
    }
}
