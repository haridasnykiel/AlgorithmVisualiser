using FluentAssertions;
using Microsoft.Playwright.Xunit;
using Xunit.Abstractions;

namespace AlgorithmVisualiser.Web.IntegrationTests;

public class SortingTests : PageTest
{
  ITestOutputHelper _testOutputHelper;

  public SortingTests(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public async Task HeapSort()
  {
    await Page.GotoAsync("http://localhost:8080");

    await Page.ClickAsync("a:has-text('Sorting')");
    await Expect(Page.Locator("div.graph")).ToBeVisibleAsync(new() { Timeout = 20000 });
    await Page.ClickAsync("button:has-text('Heap Sort')");
    await WaitAndAssertResult();
  }
  [Fact]
  public async Task BubbleSort()
  {
    await Page.GotoAsync("http://localhost:8080");

    await Page.ClickAsync("a:has-text('Sorting')");
    await Expect(Page.Locator("div.graph")).ToBeVisibleAsync(new() { Timeout = 20000 });
    await Page.ClickAsync("button:has-text('Bubble Sort')");
    await WaitAndAssertResult();
  }

  private async Task WaitAndAssertResult()
  {
    await Expect(Page.GetByText("Finish")).ToBeVisibleAsync(new() { Timeout = 20000 });
    var nodes = Page.Locator("div.node");
    int count = await nodes.CountAsync();
    int greatestValSoFar = -1;
    for (int i = 0; i < count; i++)
    {
      var node = nodes.Nth(i);
      var styles = await node.GetAttributeAsync("style");
      styles.Should().Contain("height: ");
      var spIdx = styles.IndexOf(": ");
      var cutdownStyles = styles.Remove(0, spIdx + 1);

      var sIdx = cutdownStyles.IndexOf("px");
      var heightNumStr = cutdownStyles.Remove(sIdx, cutdownStyles.Length - sIdx).Trim();
      var heightNum = int.Parse(heightNumStr);
      if (greatestValSoFar <= heightNum)
      {
        greatestValSoFar = heightNum;
      }
      else
      {
        Assert.Fail("Array not sorted");
      }
    }
  }
}