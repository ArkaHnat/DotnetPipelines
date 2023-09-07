﻿using ModularPipelines.Extensions;
using ModularPipelines.FileSystem;

namespace ModularPipelines.UnitTests.Extensions;

public class FolderExtensionsTests
{
    [Test]
    public void EnumerablePaths()
    {
        var Folders = new List<Folder>
        {
            new Folder(Path.Combine(Environment.CurrentDirectory, "Folder1")),
            new Folder(Path.Combine(Environment.CurrentDirectory, "Folder2"))
        }.AsEnumerable();

        var paths = Folders.AsPaths();
        
        Assert.That(paths, Is.AssignableTo<IEnumerable<string>>());
        Assert.That(paths, Is.Not.AssignableTo<List<string>>());
        Assert.That(paths, Is.EquivalentTo(new List<string>
        {
            Path.Combine(Environment.CurrentDirectory, "Folder1"),
            Path.Combine(Environment.CurrentDirectory, "Folder2")
        }));
    }
    
    [Test]
    public void ListPaths()
    {
        var Folders = new List<Folder>
        {
            new Folder(Path.Combine(Environment.CurrentDirectory, "Folder1")),
            new Folder(Path.Combine(Environment.CurrentDirectory, "Folder2"))
        };

        var paths = Folders.AsPaths();
        
        Assert.That(paths, Is.AssignableTo<IEnumerable<string>>());
        Assert.That(paths, Is.AssignableTo<List<string>>());
        Assert.That(paths, Is.EquivalentTo(new List<string>
        {
            Path.Combine(Environment.CurrentDirectory, "Folder1"),
            Path.Combine(Environment.CurrentDirectory, "Folder2")
        }));
    }
}