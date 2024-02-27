using System.Collections;
using System.Collections.Generic;
using _Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridStructureTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GridStructureTestsSimplePasses()
    {
        // Arrange
        GridStructure gridStructure = new(2);
        Vector3 inputPosition = Vector3.zero;
        
        // Act
        Vector3 returnPosition = gridStructure.CalculateGridPosition(inputPosition);
        
        // Assert
        Assert.AreEqual(Vector3.zero, returnPosition);
    }
    
    
}
