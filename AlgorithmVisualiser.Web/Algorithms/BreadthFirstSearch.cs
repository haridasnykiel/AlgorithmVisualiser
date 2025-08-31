using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Algorithms;

internal static class BreadthFirstSearch
{
  internal static async Task Execute(
    GraphNode[]? nodes,
    GraphNode? startNode,
    GraphNode? destinationNode,
    Func<Task> stateChanged)
  {
    if (nodes == null) return;

    if (startNode == null || destinationNode == null) return;
    var node = await Bfs(startNode, destinationNode.Value, stateChanged);
    if (node == null)
    {
      return;
    }

    node.AddFoundStyle();
    await stateChanged();
  }

  private static async Task<GraphNode?> Bfs(GraphNode startingNode, string pin, Func<Task> stateChanged)
  {
    var visited = new HashSet<string>();
    var queue = new Queue<GraphNode>();

    queue.Enqueue(startingNode);
    startingNode.SetVisited();
    visited.Add(startingNode.Value);

    GraphNode? result = null;
    while (queue.Count > 0)
    {
      var node = queue.Dequeue();
      if (node.Value == pin)
      {
        result = node;
        break;
      }

      foreach (var edge in node.Connections)
      {
        if (edge == null || visited.Contains(edge.Value))
        {
          continue;
        }

        visited.Add(edge.Value);
        queue.Enqueue(edge);
        edge.SetVisited();
        edge.AddVisitedStyle();
        await stateChanged();

        if (edge.Value == pin)
        {
          result = edge;
          break;
        }
      }

      if (result != null) break;
    }

    return result;
  }
}