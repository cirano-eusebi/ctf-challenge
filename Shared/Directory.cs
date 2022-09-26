// See the LICENSE file in the project root for more information.


using System.Text.Json.Serialization;

namespace CFTChallenge.Shared;

public class Entity
{
    public string Path { get; init; }
    public string Name { get; init; }

    [JsonConstructor]
    public Entity(string path, string name)
    {
        Path = path;
        Name = name;
    }

    public Entity(string absolutePath)
    {
        Path = absolutePath;
        Name = System.IO.Path.GetFileName(absolutePath);
    }
}

public class Directory : Entity
{
    public IDictionary<string, File> Files { get; init; }
    public IDictionary<string, string> Directories { get; init; }

    [JsonConstructor]
    public Directory(string path, string name, IDictionary<string, File> files, IDictionary<string, string> directories) : base(path, name)
    {
        Files = files;
        Directories = directories;
    }

    public Directory(string absolutePath) : base(absolutePath)
    {
        Files = System.IO.Directory.EnumerateFiles(absolutePath)
            .ToDictionary(
                name => System.IO.Path.GetFileName(name),
                name => new File(System.IO.Path.GetFullPath(name))
            );

        Directories = System.IO.Directory.EnumerateDirectories(absolutePath, "*",
            new EnumerationOptions { AttributesToSkip = FileAttributes.Temporary })
        .ToDictionary(
            name => System.IO.Path.GetFileName(name),
            name => System.IO.Path.GetFullPath(name)
        );

        // Add parent and current folder.
        Directories.TryAdd(".\\.", absolutePath);
        Directories.TryAdd(".\\..", System.IO.Path.GetFullPath(System.IO.Path.Combine(absolutePath, "..")));
    }
}

public class File : Entity
{
    [JsonIgnore]
    public byte[] Content => System.IO.File.ReadAllBytes(Path);

    [JsonConstructor]
    public File(string path, string name) : base(path, name) { }

    public File(string absolutePath) : base(absolutePath) { }
}
