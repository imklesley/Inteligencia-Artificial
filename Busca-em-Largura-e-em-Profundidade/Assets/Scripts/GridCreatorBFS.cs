using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridCreatorBFS : MonoBehaviour 
{
    public ColorPicker colorPicker;

    public CubeBFS[,] cubes;
    public List<CubeBFS> instantiatedCubes;

 
    public int width = 20;
    public int height = 20;

    private static CubeBFS curStartCube;
    private static CubeBFS curEndCube;

    public void Start()
    {
        BuildGrid();
    }

    public void BuildGrid()
    {
        curStartCube = null;
        curEndCube = null;

        cubes = new CubeBFS[width, height];

        var colorDefault = colorPicker.GetObstacleColor();

        foreach (var cube in instantiatedCubes)
        {
            if (cube.type == CubeBFS.Type.Obstacle)
            {
                cube.SetObstacle(colorDefault);

                cube.transform.localScale += new Vector3(0, 1, 0);
            }

            cubes[cube.x, cube.y] = cube;
        }

        //var cubesList = cubes.Cast<CubeBFS>().ToList();

        SetStartCube(cubes[1, 18]);
        SetEndCube(cubes[18, 1]);
    }

    public void AddNeighbours(bool canWalkThroughObstacles = false)
    {
        if (cubes.Length == 0)
        {
            Debug.Log("Nenhum cubo encontrado");

            return;
        }

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++) 
            {
                var curCube = cubes[i,j];

                if (i > 0)
                {
                    curCube.AddNeighbour(cubes[i-1,j], canWalkThroughObstacles);
                }
                if (j > 0)
                {
                    curCube.AddNeighbour(cubes[i,j-1], canWalkThroughObstacles);
                }
                if (i < width - 1)
                {
                    curCube.AddNeighbour(cubes[i+1,j], canWalkThroughObstacles);
                }
                if (j < height - 1)
                {
                    curCube.AddNeighbour(cubes[i,j+1], canWalkThroughObstacles);
                }
            }
        }
    }

    public void SetStartCube(CubeBFS cube)
    {
        curStartCube = cube;

        var render = curStartCube.GetComponent<Renderer>();
        render.material.color = colorPicker.GetStartColor();
    }

    public void SetEndCube(CubeBFS cube)
    {
        curEndCube = cube;

        var render = curEndCube.GetComponent<Renderer>();
        render.material.color = colorPicker.GetEndColor();
    }

    public CubeBFS GetStartCube()
    {
        return curStartCube;
    }

    public CubeBFS GetEndCube()
    {
        return curEndCube;
    }
}
