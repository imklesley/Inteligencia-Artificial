using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS : MonoBehaviour 
{
    public GridCreatorBFS gridCreator;
    public ColorPicker colorPicker;

    private Queue<CubeBFS> frontier;
    private List<CubeBFS> visited;

    public void StartBFS()
    {
        frontier = new Queue<CubeBFS>();
        visited = new List<CubeBFS>();

        gridCreator.AddNeighbours(false);

        StartCoroutine("RunBFS");
    }

    private IEnumerator RunBFS() 
    {
        var start = gridCreator.GetStartCube();

        frontier.Enqueue(start);
        visited.Add(start);

        while (frontier.Count != 0)
        {
            var current = frontier.Dequeue();

            var neighbours = current.GetNeighbours();

            neighbours.ForEach(delegate(CubeBFS cube)
            {
                if (visited.Contains(cube)) return;

                frontier.Enqueue(cube);
                visited.Add(cube);
                cube.PaintCube(colorPicker.GetVisitedColor());
            });

            yield return new WaitForSeconds(.1f);
        }

        Debug.Log("Achou!");
        GameManager.instance.BFSHasFound();
    }
}
