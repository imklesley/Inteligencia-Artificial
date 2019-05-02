using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using felixro;

public class PathFinder : MonoBehaviour 
{
    //public GridCreator gridCreator;
    //public ColorPicker colorPicker;

    //private PriorityQueue<Cube> frontier;
    //private Dictionary<Cube, Cube> cameFrom;
    //private Dictionary<Cube, int> costSoFar;
    //private List<Cube> path;

    //public static PathFinder instance;

    //private void Awake()
    //{
    //    instance = this;
    //}

    //public void StartBFS()
    //{
    //    frontier = new PriorityQueue<Cube>();
    //    cameFrom = new Dictionary<Cube, Cube>();
    //    costSoFar = new Dictionary<Cube, int>();

    //    gridCreator.AddNeighbours(false);

    //    RunBFS();
    //}
        
    //public void StopBFS()
    //{
    //    StopAllCoroutines();
    //}

    //private void RunBFS()
    //{
    //    var start = gridCreator.GetStartCube();
    //    var end = gridCreator.GetEndCube();

    //    frontier.Enqueue(start);
    //    cameFrom.Add(start, null);
    //    costSoFar.Add(start, 0);

    //    Cube current = null;

    //    while (frontier.GetCount() != 0)
    //    {
    //        current = frontier.Dequeue();

    //        if (current == end)
    //            break;

    //        var neighbours = current.GetNeighbours();

    //        neighbours.ForEach(delegate(Cube next)
    //        {
    //            var newCost = costSoFar[current] + next.GetWeight();

    //            if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
    //            {
    //                costSoFar[next] = newCost;

    //                var priority = newCost + Heuristic(end, next);

    //                next.SetWeight(priority);

    //                frontier.Enqueue(next);
    //                cameFrom[next] = current;
    //            }
    //        });
    //    }

    //    CalculatePath();
    //}

    //private void CalculatePath()
    //{
    //    var start = gridCreator.GetStartCube();
    //    var current = gridCreator.GetEndCube();
    //    path = new List<Cube> {current};


    //    while (current != start)
    //    {
    //        current = cameFrom[current];
    //        path.Add(current);
    //        current.PaintCube(colorPicker.GetVisitedColor());
    //    }

    //    path.Reverse();

    //    Debug.Log("Achou sim!");

    //    GameManager.instance.dfsHasFound = true;

    //    StartCoroutine("FollowPath");
    //}

    //private static int Heuristic(Cube goal, Cube next)
    //{
    //    var goalX = goal.transform.position.x;
    //    var goalY = goal.transform.position.y;

    //    var nextX = next.transform.position.x;
    //    var nextY = next.transform.position.y;

    //    return (int)(Mathf.Abs(goalX - nextX) + Mathf.Abs(goalY - nextY));
    //}

    //private IEnumerator FollowPath()
    //{
    //    foreach (var cube in path)
    //    {
    //        gridCreator.SetStartCube(cube);

    //        yield return new WaitForSeconds(.2f);
    //    }
    //}
}
