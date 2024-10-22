namespace AlgorithmVisualiser.Web.Models;

public class SortNode 
{
    private int _width;
    private int _height;
    private string _style;
    private string? _backgroundStyle;

    public SortNode(int height, int width)
    {
        _height = height;
        _width = width;
        _style = $"height: {_height}px; width: {_width}%;";
    }

    public int Value => _height;

    public string Style => _style + (_backgroundStyle ?? string.Empty);

    public void AddSwapStyle() => _backgroundStyle = " background-color: red;";
    public void RemoveBackgroundStyle() => _backgroundStyle = null;
}