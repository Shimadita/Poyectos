using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparar : MonoBehaviour
{
    public GameObject bola;
    float timeAux;

    public float valorMax;
    public float valorMin;
    public int numDados;
    public int funcSelect;
    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        timeAux = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeDif = Time.time - timeAux;

        //if (Input.GetKeyDown(KeyCode.Space) && (timeDif > 0.5f))
        if (timeDif > 2f)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z+0.5f);
            GameObject clon = Instantiate(bola, pos, Quaternion.identity) as GameObject;
            clon.SetActive(true);
            rb = clon.GetComponent<Rigidbody2D>();

            Vector3 direccion = new Vector3(SelectFunction(funcSelect), 0f, 0f);
            rb.AddForce(direccion);

            timeAux = Time.time;
            
        }
	}

	float SelectFunction(float f)
	{
		float fuerzaFinal = 0;
		switch (f)
		{
			case 1:
				P01FuerzaFija(valorMax);
				break;
			case 2:
				P02RandomRange(valorMin, ValorMax);
				break;
			case 3:
				P03RandomDosDados(valorMin, valorMax);
				break;
			case 4:
				P04RandomVariosDados(numDados, valorMin, valorMax);
				break;
			case 5:
				P05MaxDados();
				break;
			case 6:
				P06DescatarMinDados();
				break;
			case 7:
				P07DescatarMinYVolverATirar();
				break;
			case 8:
				P08DescatarMaxYVolverATirar(dados);
				break;
		}
		return fuerzaFinal;
	}

	//impulso con valor fijo (500)
	float P01FuerzaFija()
	{
		return 500.0f;

	}
	//impulso aleatorio en un rango de valores definidos [200,500]
	float P02RandomRange(float min, float max)
	{
		return Random.Range(min, max);
	}

	// Sea la suma de dos valores aleatorios, de modo que su valor esté en el rango[0, 500]
	float P03RandomDosDados(float minimo, float maximo)
	{
		float suma = 0;
		float sumaMax = maximo / 2;

		float valor1 = Random.Range(minimo, sumaMax);
		floar valor2 = Random.Range(minimo, sumaMax);

	}

	// Sea la suma de varios dados (éste será una variable que podamos modificar). De modo que el valor de la suma esté en el rango[0, 500]
	float P04RandomVariosDados(int dados, float minimo, float maximo)
	{
		float suma = 0.0f;
		for (int contador = 0; contador < dados; contador++)
		{
			suma = suma + Random.Range(minimo, maximo / 2);
		}

		if (suma > 500) suma = 500;
		return suma;
	}

	// Se lanzan varios dados y se obtiene el mayor de ellos
	float P05MaxDados(int dados, float maximo)
	{
		float mayor = 0.0f;
		for (int contador = 0; contador < dados; contador++)
		{
			float actual = P03RandomDosDados(maximo);
			if (actual > mayor) mayor = actual;
		}
		return mayor;
	}

	// El valor del impulso será la suma de n dados.Se lanzarán n+1 dados y se descarta el menor de ellos
	// array

	float P06DescatarMinDados(int dados)
	{
		float maximo = 500.0f / dados;
		float menor = 500.0f;
		float resultado = 0.0f;
		for (int contador = 0; contador <= dados; contador++)
		{
			float actual = Random.Range(0.0f, maximo);
			if (actual < menor) menor = actual;
			resultado += actual;
		}
		resultado -= menor;
		return resultado;
	}

	// El valor del impulso será la suma de n dados. Se lanzarán n dados, se descartará el menor y se volverá a tirar éste
	float P07DescatarMinYVolverATirar(int dados)
	{
		float maximo = 500.0f / dados;
		float menor = 500.0f;
		float resultado = 0.0f;
		for (int contador = 0; contador < dados; contador++)
		{
			float actual = Random.Range(0.0f, maximo);
			if (actual < menor) menor = actual;
			resultado += actual;
		}
		resultado -= menor;
		menor = Random.Range(0.0f, maximo);
		resultado = resultado + menor;
		return resultado;
	}

	//El valor del impulso será la suma de n dados. Se lanzarán n+1 dados y se descarta el mayor de ellos
	float P08DescatarMaxYVolverATirar(int dados)
	{
		float maximo = 500.0f / dados;
		float mayor = 0.0f;
		float resultado = 0.0f;
		float actual = 0.0f;
		for (int contador = 0; contador <= dados; contador++)
		{
			actual = Random.Range(0.0f, maximo);
			if (actual > mayor) mayor = actual;
			resultado += actual;
		}

		resultado -= mayor;
		return resultado;
	}
}