using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Helpers;

static class NodeHelper
{
  private static Random _rnd;
  private const int MaxSize = 10;

  private static readonly (int Column, int Row)[] Directions =
  [
    (1, 0),
    (0, 1),
    (1, 1),
    (-1, 0),
    (0, -1),
    (-1, -1),
    (1, -1),
    (-1, 1)
  ];

  static NodeHelper()
  {
    _rnd = new Random();
  }

  public static int MaxRow => MaxSize;

  public static SortNode[] Randomise()
  {
    var nodes = new SortNode[_rnd.Next(10, 20)];
    for (int i = 0; i < nodes.Length; i++)
    {
      nodes[i] = new SortNode(_rnd.Next(10, 100), 100 / nodes.Length);
    }

    return nodes;
  }

  public static GraphNode[] Create100GraphNodes()
  {
    var nodes = new GraphNode[100];
    var rowIdx = 0;
    var columnIdx = 0;
    for (int i = 0; i < nodes.Length; i++)
    {
      nodes[i] = new GraphNode((i + 1).ToString(), 3);
      var node = nodes[i];
      node.AddColumnIdx(columnIdx).AddRowIdx(rowIdx).SetWeight(_rnd.Next(1, 10));
      columnIdx++;

      if (columnIdx >= MaxSize)
      {
        rowIdx++;
        columnIdx = 0;
      }
    }

    AddAllConnections(nodes);
    return nodes;
  }

  private static void AddAllConnections(GraphNode[] nodes)
  {
    foreach (var node in nodes)
    {
      foreach (var direction in Directions)
      {
        var nextNodeColumn = node.Column + direction.Column;
        var nextNodeRow = node.Row + direction.Row;

        var nextNode = nodes.FirstOrDefault(n => n.Column == nextNodeColumn && n.Row == nextNodeRow);

        if (nextNode != null)
        {
          node.AddConnection(nextNode);
        }
      }
    }
  }
}