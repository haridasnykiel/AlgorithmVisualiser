using System.Reflection.Metadata.Ecma335;
using AlgorithmVisualiser.Web.Models;

namespace AlgorithmVisualiser.Web.Extensions;

static class SortNodesHelpers
{
    private static Random _rnd;
    static SortNodesHelpers()
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
}