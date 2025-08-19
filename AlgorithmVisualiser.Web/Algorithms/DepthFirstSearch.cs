using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Algorithms;

internal static class DepthFirstSearch
{
  internal static async Task Execute(
    GraphNode[]? nodes,
    GraphNode? startNode,
    GraphNode? destinationNode,
    Func<Task> stateChangedAction)
  {
    if (nodes == null) return;

    if (startNode == null || destinationNode == null) return;
    var node = await Dfs(startNode, new HashSet<string>(), destinationNode.Value, stateChangedAction);
    if (node == null)
    {
      return;
    }

    node.AddFoundStyle();
    await stateChangedAction();
    await SearchPathFinder.Find(node, startNode, stateChangedAction);
  }

  private static async Task<GraphNode?> Dfs(
    GraphNode node,
    HashSet<string> visited,
    string pin,
    Func<Task> stateChangedAction)
  {
    if (node.Value == pin) return node;

    visited.Add(node.Value);

    node.SetVisited();
    if (!node.IsStart && !node.IsDestination)
    {
      node.AddVisitedStyle();
      await stateChangedAction();
    }

    GraphNode? result = null;
    foreach (var edge in node.Connections)
    {
      if (edge == null) continue;

      if (visited.Contains(edge.Value)) continue;

      result = await Dfs(edge, visited, pin, stateChangedAction);

      if (result?.Value == pin)
      {
        break;
      }
    }

    return result;
  }
}