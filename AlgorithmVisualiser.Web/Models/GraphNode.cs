using System.Collections.Immutable;

namespace AlgorithmVisualiser.Web.Models;

public class GraphNode
{
    private string _value;
    private GraphNode[] _connections;
    private int _connectionIdx;
    private int _romIdx;
    private int _columnIdx;
    private string _style;
    
    public GraphNode(string value, int connectionsLength)
    {
        _value = value;
        _connections = new GraphNode[connectionsLength];
        _connectionIdx = 0;
    }

    public string Value => _value;
    public int Column => _columnIdx;
    public int Row => _romIdx;

    public ImmutableArray<GraphNode?> Connections => [.._connections];

    public void AddConnection(GraphNode node)
    {
        if (_connectionIdx >= _connections.Length)
        {
            var newConnectionsCapacity = _connections.Length + _connections.Length;
            var newConnections = new GraphNode[newConnectionsCapacity];
            Array.Copy(_connections, newConnections, _connections.Length);
            _connections = newConnections;
        }
        _connections[_connectionIdx] = node;
        _connectionIdx++;
    }

    public GraphNode AddRowIdx(int rowIdx)
    {
        _romIdx = rowIdx;
        return this;
    }
    
    public GraphNode AddColumnIdx(int columnIdx)
    {
        _columnIdx = columnIdx;
        return this;
    }

    public void AddFoundStyle() => _style = " background-color: red;";
    public void AddVisitedStyle() => _style = " background-color: orange;";
    public void RemoveBackgroundStyle() => _style = null;

    public string Style => _style;
}