namespace AlgorithmVisualiser.Web.Models;

public class SortNode 
{
    private int _width;
    private int _height;
    private string _style;

    public SortNode(int height, int width)
    {
        _height = height;
        _width = width;
        _style = $"height: {_height}px; width: {_width}%;";
    }

    public int Value => _height;

    public string Style => _style;

}