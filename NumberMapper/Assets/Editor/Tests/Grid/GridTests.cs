using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class GridTests
{
    [TestCase(6, 8, true)]
    [TestCase(1, 12, true)]
    public void GeneratesGrid_PositiveNumbers_SuccessfullyCreatesGrid(int x, int y, bool expected)
    {
        Grid grid = new Grid();
        bool result = grid.Generate(x, y);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(-3, 4, false)]
    [TestCase(0, 0, false)]
    [TestCase(0, 12, false)]
    public void GeneratesGrid_NegativeNumbers_FailsToCreateGrid(int x, int y, bool expected)
    {
        Grid grid = new Grid();
        bool result = grid.Generate(x, y);
        Assert.That(result, Is.EqualTo(expected));
    }
        
}
