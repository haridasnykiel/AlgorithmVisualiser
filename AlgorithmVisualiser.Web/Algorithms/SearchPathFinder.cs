using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Algorithms;

static class SearchPathFinder
{
  internal static async Task Find(
    GraphNode? destinationNode,
    GraphNode? startNode,
    Func<Task> stateChangedAction)
  {
    if (destinationNode == null || startNode == null) return;
    var path = destinationNode;
    var row = startNode.Row;
    var column = startNode.Column;
    while (path.Value != startNode.Value)
    {
      path.SetIsPartOfReturnPath();
      path = path.FindClosest(row, column);
      if (path.IsStart || path.IsDestination) continue;
      path.AddPathNodeStyle();
      await stateChangedAction();
    }
  }
}