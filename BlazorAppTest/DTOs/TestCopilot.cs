using System.Xml.Linq;

namespace BlazorAppTest.DTOs;

public class TestCopilot
{
    public TestCopilot()
    {
    }

    // Write function to return the sum of two numbers
    public int Add(int a, int b)
    {
        return a + b;
    }

    // Write function create signature for string and return string by RSA encryption   
    public string Encrypt(string input)
    {
        return input;
    }


    private void FindImage()
    {
        var doc = XDocument.Load("index.xhml");
        // find all images
        var images = doc.Descendants("img");
        foreach (var image in images)
        {
            // Write the image source to the console
            Console.WriteLine(image.Attribute("src").Value);
        }
    }



}

