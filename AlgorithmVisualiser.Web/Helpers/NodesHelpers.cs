using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Helpers;

static class NodesHelpers
{
    private static Random _rnd;
    static NodesHelpers()
    {
        _rnd = new Random();
    }

    public static SortNode[] Randomise()
    {
        var nodes = new SortNode[_rnd.Next(10, 80)];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new SortNode(_rnd.Next(10, 100), 100 / nodes.Length);
        }
        return nodes;
    }
    
    public static GraphNode[] Create100GraphNodes()
    {
        var nodes = new GraphNode[100];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new GraphNode((i + 1).ToString(), 3);
        }
        return nodes;
    }
}