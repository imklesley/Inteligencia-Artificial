using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KMeans : MonoBehaviour
{
    #region Atributos Unity

    public GameObject IrisDataPrefab;

    public List<Centroid> centroidsList = new List<Centroid>();

    public int instancesAmount = 150;

    #endregion

    #region Algoritmos

    // Algoritmo do KMeans
    public void AlgoritmoKMeans(List<IrisData> instances, int numInstances, int numClusters, int loops)
    {
        // Centróides/Clusters
        List<Centroid> centroids = new List<Centroid>();

        CriarCentroidesIniciais(centroids, numClusters);

        // Faça um loop aqui para melhorar o clustering.
        for (int j = 0; j < loops; j++)
        {
            // Obtem a instância de cada centroide
            foreach (var instance in instances)
                instance.Centroid = ObterCentroideMaisProximo(centroids, numClusters, instance);

            // Avalia o Clustering atual
            // AvaliarClustering(instances, 150);

            // Move o centraid para o novo cluster médio
            MoverCentroidsParaMedia(centroids, numClusters, instances, numInstances);
        }
    }

    // Cria os centróides aleatórios no início da execução
    public void CriarCentroidesIniciais(List<Centroid> centroids, int numClusters)
    {
        for (int i = 0; i < numClusters; i++)
        {
            centroids.Add(new Centroid
            {
                SepalLength = ((RandomNumbers.NextNumber() % 20) + 40) / 10.00, // 4-6
                SepalWidth = ((RandomNumbers.NextNumber() % 20) + 20) / 10.00,  // 2-4
                PetalLength = ((RandomNumbers.NextNumber() % 70) + 10) / 10.00, // 1-8
                PetalWidth = ((RandomNumbers.NextNumber() % 10)) / 10.00        // 0-1
            });
        }
    }

    // Obtêm o centróide mais próximo
    public int ObterCentroideMaisProximo(List<Centroid> centroids, int numClusters, IrisData data)
    {
        double minDistance;
        int nearestcentroid;

        // Inicializa valores assumindo o centróide 0 como mais próximo
        minDistance = CalcularDistanciaEuclidiana(centroids[0], data);
        nearestcentroid = 0;

        for (int i = 1; i < numClusters; i++)
        {
            double distance = CalcularDistanciaEuclidiana(centroids[i], data);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestcentroid = i;
            }
        }

        return nearestcentroid;
    }

    public void MoverCentroidsParaMedia(List<Centroid> centroids, int numClusters, List<IrisData> instances, int numInstances)
    {
        // Obtêm a média real dos atributos de cada cluster.
        // O loop mantém a capacidade de escolher um número variável de clusters.

        for (int i = 0; i < numClusters; i++)
        {
            double sepalLengthMean = 0.0;
            double sepalWidthMean = 0.0;
            double petalLengthMean = 0.0;
            double petalWidthMean = 0.0;
            int numInstancesInCluster = 0;

            for (int j = 0; j < numInstances; j++)
            {
                if (instances[j].Centroid == i)
                {
                    sepalLengthMean += instances[j].SepalLength;
                    sepalWidthMean += instances[j].SepalWidth;
                    petalLengthMean += instances[j].PetalLength;
                    petalWidthMean += instances[j].PetalWidth;
                    numInstancesInCluster += 1;
                }
            }

            centroids[i].SepalLength = sepalLengthMean / numInstancesInCluster;
            centroids[i].SepalWidth = sepalWidthMean / numInstancesInCluster;
            centroids[i].PetalLength = petalLengthMean / numInstancesInCluster;
            centroids[i].PetalWidth = petalWidthMean / numInstancesInCluster;
        }

        centroidsList = centroids;
    }

    // Cálculo da Distância Euclidiana
    public double CalcularDistanciaEuclidiana(Centroid centroid, IrisData data)
    {
        double a = data.SepalLength - centroid.SepalLength;
        double b = data.SepalWidth - centroid.SepalWidth;
        double c = data.PetalLength - centroid.PetalLength;
        double d = data.PetalWidth - centroid.PetalWidth;

        return Math.Sqrt((a * a) + (b * b) + (c * c) + (d * d));
    }

    public void AvaliarClustering(List<IrisData> datas, int numInstances)
    {
        int irisSetosa0 = 0;
        int irisSetosa1 = 0;
        int irisSetosa2 = 0;

        int irisVersiColor0 = 0;
        int irisVersiColor1 = 0;
        int irisVersiColor2 = 0;

        int irisVirginica0 = 0;
        int irisVirginica1 = 0;
        int irisVirginica2 = 0;

        // Resultados codificados para uma avaliação mais rápida
        for (int i = 0; i < numInstances; i++)
        {

            if (datas[i].Family == "Iris-setosa" && datas[i].Centroid == 0)
                irisSetosa0 += 1;

            if (datas[i].Family == "Iris-setosa" && datas[i].Centroid == 1)
                irisSetosa1 += 1;

            if (datas[i].Family == "Iris-setosa" && datas[i].Centroid == 2)
                irisSetosa2 += 1;

            if (datas[i].Family == "Iris-versicolor" && datas[i].Centroid == 0)
                irisVersiColor0 += 1;

            if (datas[i].Family == "Iris-versicolor" && datas[i].Centroid == 1)
                irisVersiColor1 += 1;

            if (datas[i].Family == "Iris-versicolor" && datas[i].Centroid == 2)
                irisVersiColor2 += 1;

            if (datas[i].Family == "Iris-virginica" && datas[i].Centroid == 0)
                irisVirginica0 += 1;

            if (datas[i].Family == "Iris-virginica" && datas[i].Centroid == 1)
                irisVirginica1 += 1;

            if (datas[i].Family == "Iris-virginica" && datas[i].Centroid == 2)
                irisVirginica2 += 1;

        }
    }

    #endregion

    public void Executar()
    {
        // Lista de Instâncias
        var instances = new List<IrisData>();

        // Lê o arquivo e preenche a lista
        int counter = 0;
        string line;

        var path = Environment.CurrentDirectory + "\\Assets\\Data\\iris.data";

        var file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            var iris = line.ToString().Split(',');

            var instance = new IrisData
            {
                SepalLength = Convert.ToDouble(iris[0].Replace('.', ',')),
                SepalWidth = Convert.ToDouble(iris[1].Replace('.', ',')),
                PetalLength = Convert.ToDouble(iris[2].Replace('.', ',')),
                PetalWidth = Convert.ToDouble(iris[3].Replace('.', ',')),
                Family = iris[4]
            };

            instances.Add(instance);

            counter++;
        }

        file.Close();

        // Executa o KMeans
        AlgoritmoKMeans(instances, instancesAmount, 3, 100);

        // Escreve o arquivo final com os resultados
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        using (StreamWriter outputFile = new StreamWriter(Environment.CurrentDirectory + "\\Assets\\Data\\results.txt"))
        {
            // Iris
            foreach (var instance in instances)
            {
                outputFile.WriteLine($"{instance.SepalLength.ToString("N1")}    " +
                                     $"{instance.SepalWidth.ToString("N1")}    " +
                                     $"{instance.PetalLength.ToString("N1")}    " +
                                     $"{instance.PetalWidth.ToString("N1")}    " +
                                     $"{instance.Centroid}  " +
                                     $"{instance.Family}   ");
            }

            // Clusters
            var clusterId = 0;
            foreach (var cluster in centroidsList)
            {
                outputFile.WriteLine($"ID = {clusterId}:    " +
                                     $"{cluster.SepalLength.ToString("N1")}    " +
                                     $"{cluster.SepalWidth.ToString("N1")}    " +
                                     $"{cluster.PetalLength.ToString("N1")}    " +
                                     $"{cluster.PetalWidth.ToString("N1")}    ");

                clusterId++;
            }
        }

        // Instancia objetos
        foreach (var instance in instances)
        {
            var newPrefab = new GameObject();

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                // Cena da Sépala
                newPrefab = Instantiate(IrisDataPrefab, new Vector3((float)instance.SepalLength * 1.5f, (float)instance.SepalWidth * 1.5f, 0), Quaternion.identity);
            } else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                // Cena da Pétala
                newPrefab = Instantiate(IrisDataPrefab, new Vector3((float)instance.PetalLength * 1.5f, (float)instance.PetalWidth * 1.5f, 0), Quaternion.identity);
            }

            if (instance.Centroid == 0)
            {
                newPrefab.GetComponent<MeshRenderer>().material.color = Color.red;
            } else if (instance.Centroid == 1)
            {
                newPrefab.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else if (instance.Centroid == 2)
            {
                newPrefab.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }

        foreach (var centroid in centroidsList)
        {
            var newPrefab = new GameObject();

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                // Cena da Sépala
                newPrefab = Instantiate(IrisDataPrefab, new Vector3((float)centroid.SepalLength * 1.5f, (float)centroid.SepalWidth * 1.5f, 0), Quaternion.identity);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                // Cena da Pétala
                newPrefab = Instantiate(IrisDataPrefab, new Vector3((float)centroid.PetalLength * 1.5f, (float)centroid.PetalWidth * 1.5f, 0), Quaternion.identity);
            }

            newPrefab.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }
}
