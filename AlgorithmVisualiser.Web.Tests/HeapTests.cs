using AlgorithmVisualiser.Web.DataStructures;
using FluentAssertions;

namespace AlgorithmVisualiser.Web.Tests;

public class HeapTests
{
    [Fact]
    public void Insert_AddANode_AddsNodeAndUpdatesLength()
    {
        var sut = new Heap<int>(2);

        var res = sut.Insert(2, 2);

        res.Should().BeTrue();
        sut.Peek().Should().Be(2);
        sut.Length.Should().Be(1);
    }

    [Fact]
    public void Insert_AddMultipleNodes_AddsMultipleNodesCorrectly()
    {
        var sut = new Heap<int>(2);

        sut.Insert(2, 2);
        sut.Insert(3, 3);
        sut.Insert(5, 5);

        sut.Peek().Should().Be(5);
        sut.Length.Should().Be(3);
        sut.Capacity.Should().Be(4);
    }

    [Fact]
    public void Insert_AddMultipleNodesOfManyValues_AddsMultipleNodesCorrectly()
    {
        var sut = new Heap<int>(2);

        sut.Insert(2, 2);
        sut.Insert(3, 3);
        sut.Insert(5, 5);
        sut.Insert(4, 4);
        sut.Insert(1, 1);

        sut.Peek().Should().Be(5);
        sut.Length.Should().Be(5);
        sut.Capacity.Should().Be(8);
    }

    [Fact]
    public void Insert_DeleteItemWhereHas2Nodex_ReturnsNodeAtFront()
    {
        var sut = new Heap<int>(2);

        sut.Insert(2, 2);
        sut.Insert(3, 3);

        var result = sut.Delete();

        result.Should().Be(3);
        sut.Peek().Should().Be(2);
        sut.Length.Should().Be(1);
        sut.Capacity.Should().Be(2);
    }

    [Fact]
    public void Insert_DeleteItemWhere3NodesExist_ReturnsNodeAtFront()
    {
        var sut = new Heap<int>(2);

        sut.Insert(2, 2);
        sut.Insert(3, 3);
        sut.Insert(6, 6);

        var result = sut.Delete();

        result.Should().Be(6);
        sut.Peek().Should().Be(3);
        sut.Length.Should().Be(2);
        sut.Capacity.Should().Be(4);
    }
    
    [Fact]
    public void Insert_DeleteItemWhere1NodeExists_ReturnsNodeAtFront()
    {
        var sut = new Heap<int>(2);

        sut.Insert(2, 2);

        var result = sut.Delete();

        result.Should().Be(2);
        sut.Peek().Should().Be(default);
        sut.Length.Should().Be(0);
        sut.Capacity.Should().Be(2);
    }
    
    [Fact]
    public void Insert_DeleteItemWhereDeleteMultiple_ReturnsNodeAtFront()
    {
        var sut = new Heap<int>(2);

        sut.Insert(2, 2);
        sut.Insert(3, 3);
        sut.Insert(5, 5);
        sut.Insert(4, 4);
        sut.Insert(7, 7);
        sut.Insert(10, 10);

        var result = sut.Delete();
        result = sut.Delete();

        result.Should().Be(7);
        sut.Peek().Should().Be(5);
        sut.Length.Should().Be(4);
        sut.Capacity.Should().Be(8);
    }
}