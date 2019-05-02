using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridCreatorDFS : MonoBehaviour 
{
    public ColorPicker colorPicker;

    public CubeDFS[,] cubes;
    public List<CubeDFS> instantiatedCubes;

    public int width = 20;
    public int height = 20;

    private static CubeDFS curStartCube;
    private static CubeDFS curEndCube;

    public void Start()
    {
        BuildGrid();
    }

    public void BuildGrid()
    {
        curStartCube = null;
        curEndCube = null;

        cubes = new CubeDFS[width, height];

        var colorDefault = colorPicker.GetObstacleColor();

        foreach (var cube in instantiatedCubes)
        {
            if (cube.type == CubeDFS.Type.Obstacle)
            {
                cube.SetObstacle(colorDefault);

                cube.transform.localScale += new Vector3(0, 1, 0);
            }

            cubes[cube.x, cube.y] = cube;
        }

        //var cubesList = cubes.Cast<CubeDFS>().ToList();

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

    public void SetStartCube(CubeDFS cube)
    {
        curStartCube = cube;

        var render = curStartCube.GetComponent<Renderer>();
        render.material.color = colorPicker.GetStartColor();
    }

    public void SetEndCube(CubeDFS cube)
    {
        curEndCube = cube;

        var render = curEndCube.GetComponent<Renderer>();
        render.material.color = colorPicker.GetEndColor();
    }

    public CubeDFS GetStartCube()
    {
        return curStartCube;
    }

    public CubeDFS GetEndCube()
    {
        return curEndCube;
    }
}
