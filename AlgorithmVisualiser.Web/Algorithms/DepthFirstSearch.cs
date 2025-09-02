using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Algorithms;

internal static class DepthFirstSearch
{
  internal static async Task Execute(
    GraphNode[]? nodes,
    GraphNode? startNode,
    GraphNode? destinationNode,
    Func<Task> stateChangedAction,
    CancellationToken ct)
  {
    if (nodes == null) return;

    if (startNode == null || destinationNode == null) return;
    var node = await Dfs(startNode, new HashSet<string>(), destinationNode.Value, stateChangedAction, ct);
    if (node == null)
    {
      return;
    }

    node.AddFoundStyle();
    await stateChangedAction();
  }

  private static async Task<GraphNode?> Dfs(
    GraphNode node,
    HashSet<string> visited,
    string pin,
    Func<Task> stateChangedAction,
    CancellationToken ct)
  {
    if(ct.IsCancellationRequested) return null;
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

      result = await Dfs(edge, visited, pin, stateChangedAction, ct);

      if (result?.Value == pin)
      {
        break;
      }
    }

    return result;
  }
}