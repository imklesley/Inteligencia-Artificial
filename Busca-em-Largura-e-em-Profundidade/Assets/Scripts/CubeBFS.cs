using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using felixro;

public class CubeBFS : MonoBehaviour , IComparable<CubeBFS>
{
    public int x;
    public int y;

    private int weight;
    private bool isObstacle = false;

    private List<CubeBFS> neighbours;
    private Renderer objRenderer;

    public enum Type
    {
        Start,
        End,
        Path,
        Obstacle
    }

    public Type type = Type.Path;

    private void Awake()
    {
        weight = 1;

        neighbours = new List<CubeBFS>();
        objRenderer = GetComponent<Renderer>();
    }

    public void PaintCube(Color color)
    {
        if (objRenderer != null) objRenderer.material.color = color;
    }

    public void AddNeighbour(CubeBFS cube, bool canWalkThroughObstacles)
    {
        if (!cube.isObstacle || canWalkThroughObstacles)
        {
            neighbours.Add(cube);
        }
    }

    public List<CubeBFS> GetNeighbours()
    {
        return neighbours;
    }

    public void SetObstacle(Color color, int newWeight = 1)
    {
        weight = newWeight;
        isObstacle = true;

        PaintCube(color);
    }

    public bool IsObstacle()
    {
        return isObstacle;
    }

    public int GetWeight()
    {
        return weight;
    }

    public void SetWeight(int newWeight)
    {
        weight = newWeight;
    }

    public int CompareTo(CubeBFS obj) 
    {
        if (obj == null)
            throw new ArgumentNullException();

        var otherWeight = ((CubeBFS)obj).GetWeight();

        if (weight == otherWeight)
            return 0;

        if (weight > otherWeight)
            return 1;

        return -1;
    }
}
