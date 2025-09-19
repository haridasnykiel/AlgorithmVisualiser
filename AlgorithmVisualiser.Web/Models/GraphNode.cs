using System.Collections.Immutable;

namespace AlgorithmVisualiser.Web.Models;

public class GraphNode : IEquatable<GraphNode>
{
  private readonly string _value;
  private GraphNode?[] _connections;
  private int _connectionIdx;
  private int _rowIdx;
  private int _columnIdx;
  private string _style;
  private bool _isStart;
  private bool _isDestination;
  private bool _hasVisited;
  private bool _isPartOfReturnPath;
  private const int Maximum = 200;
  private int _weight;

  public GraphNode(string value, int connectionsLength)
  {
    _value = value;
    _connections = new GraphNode[connectionsLength];
    _connectionIdx = 0;
  }

  private static GraphNode Max()
  {
    var g = new GraphNode("-1", 0);
    return g.AddRowIdx(Maximum).AddColumnIdx(Maximum);
  }

  public string Value => _value;
  public int Column => _columnIdx;
  public int Row => _rowIdx;
  public bool IsStart => _isStart;
  public bool IsDestination => _isDestination;
  public string Style => _style;
  public int Weight => _weight;


  public ImmutableArray<GraphNode?> Connections => [.._connections];

  public GraphNode FindClosest(int row, int column)
  {
    GraphNode closest = Max();
    for (var i = 1; i < _connections.Length; i++)
    {
      var next = _connections[i];
      if (next is null || !next._hasVisited || next._isPartOfReturnPath) continue;
      if (closest.Column == column && closest.Row == row) return closest;
      var closestDist = Math.Abs(closest.Column - column) + Math.Abs(closest.Row - row);
      var nextDist = Math.Abs(next.Row - row) + Math.Abs(next.Column - column);
      if (closestDist > nextDist)
      {
        closest = next;
      }
    }

    return closest;
  }

  public void AddConnection(GraphNode node)
  {
    if (_connectionIdx >= _connections.Length)
    {
      var newConnectionsCapacity = _connections.Length + 2;
      var newConnections = new GraphNode[newConnectionsCapacity];
      Array.Copy(_connections, newConnections, _connections.Length);
      _connections = newConnections;
    }

    _connections[_connectionIdx] = node;
    _connectionIdx++;
  }

  public GraphNode AddRowIdx(int rowIdx)
  {
    _rowIdx = rowIdx;
    return this;
  }

  public GraphNode AddColumnIdx(int columnIdx)
  {
    _columnIdx = columnIdx;
    return this;
  }

  public GraphNode SetWeight(int weight)
  {
    _weight = weight;
    return this;
  }

  public void SetStart() => _isStart = true;
  public void SetDestination() => _isDestination = true;
  public void SetVisited() => _hasVisited = true;
  public void SetIsPartOfReturnPath() => _isPartOfReturnPath = true;
  public void AddFoundStyle() => _style = " background-color: red;";
  public void AddStartNodeStyle() => _style = " background-color: aqua;";
  public void AddDestinationNodeStyle() => _style = " background-color: chartreuse;";
  public void AddPathNodeStyle() => _style = " background-color: purple;";
  public void AddVisitedStyle() => _style = " background-color: orange;";

  public void Reset()
  {
    _isStart = false;
    _isDestination = false;
    _hasVisited = false;
    _isPartOfReturnPath = false;
    _style = null;
  }

  public bool Equals(GraphNode? other)
  {
    if (other is null) return false;
    if (ReferenceEquals(this, other)) return true;
    return _value == other._value;
  }

  public override bool Equals(object? obj)
  {
    if (obj is null) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != GetType()) return false;
    return Equals((GraphNode)obj);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(_value);
  }
}