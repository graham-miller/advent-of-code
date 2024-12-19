namespace AdventOfCode2024.Days;

internal static class Inputs
{
    public static Stream ForDay(int day)
    {
        var name = $"{typeof(HasNamespace).Namespace}.Resources.Day{day:00}.txt";

        return Assembly.GetAssembly(typeof(Inputs)).GetManifestResourceStream(name);
    }

    public static IEnumerable<string> ReadLines(this Stream stream)
    {
        using var reader = new StreamReader(stream);
        
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            yield return line;
        }

        
        stream.Dispose();
    }
}

internal class HasNamespace;