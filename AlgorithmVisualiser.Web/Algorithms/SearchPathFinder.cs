using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Algorithms;

static class SearchPathFinder
{
  internal static async Task Find(
    GraphNode? destinationNode,
    GraphNode? startNode,
    Func<Task> stateChangedAction,
    CancellationToken ct)
  {
    if (destinationNode == null || startNode == null) return;
    var node = destinationNode;
    var row = startNode.Row;
    var column = startNode.Column;
    while (node.Value != startNode.Value)
    {
      if(ct.IsCancellationRequested) break;
      node.SetIsPartOfReturnPath();
      node = node.FindClosest(row, column);
      if (node.IsStart || node.IsDestination) continue;
      node.AddPathNodeStyle();
      await stateChangedAction();
    }
  }
}