using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Playwright.Xunit;

namespace AlgorithmVisualiser.Web.IntegrationTests;

public class FirstTest : PageTest
{
  public FirstTest()
  {
  }
  
  [Fact]
  public async Task Test1()
  {
        // Navigate to the counter page
        await Page.GotoAsync("http://localhost:8080");
        
        await Page.ClickAsync("a:has-text('Counter')");
        
        // Verify initial state
        await Expect(Page.Locator("p")).ToContainTextAsync("Current count: 0");
        
        // Click the increment button
        await Page.ClickAsync("button:has-text('Click me')");
        
        // Verify the count increased
        await Expect(Page.Locator("p")).ToContainTextAsync("Current count: 1");
        
        // Click multiple times
        await Page.ClickAsync("button:has-text('Click me')");
        await Page.ClickAsync("button:has-text('Click me')");
        
        await Expect(Page.Locator("p")).ToContainTextAsync("Current count: 3");
  }
}