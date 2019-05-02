using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DFS : MonoBehaviour 
{
    public GridCreatorDFS gridCreator;
    public ColorPicker colorPicker;

    private Stack<CubeDFS> frontier;
    private List<CubeDFS> visited;

    public void StartDFS()
    {
        frontier = new Stack<CubeDFS>();
        visited = new List<CubeDFS>();

        gridCreator.AddNeighbours(false);

        StartCoroutine("RunDFS");
    }

    private IEnumerator RunDFS() 
    {
        var start = gridCreator.GetStartCube();

        frontier.Push(start);
        visited.Add(start);

        while (frontier.Count != 0)
        {
            var current = frontier.Pop();

            var neighbours = current.GetNeighbours();

            neighbours.ForEach(delegate(CubeDFS cube)
            {
                if (visited.Contains(cube)) return;

                frontier.Push(cube);
                visited.Add(cube);
                cube.PaintCube(colorPicker.GetVisitedColor());
            });

            yield return new WaitForSeconds(.1f);
        }

        Debug.Log("Achou!");
        GameManager.instance.DFSHasFound();
    }
}
