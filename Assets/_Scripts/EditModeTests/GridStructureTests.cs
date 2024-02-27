using NUnit.Framework;
using UnityEngine;

namespace _Scripts.EditModeTests
{
    public class GridStructureTests
    {
        private GridStructure m_Structure;
        [OneTimeSetUp]
        public void Init()
        {
            m_Structure = new GridStructure(3, 100, 100);
        }
        #region GridPositionTests
        // A Test behaves as an ordinary method
        [Test]
        public void CalculateGridPositionPasses()
        {
            Vector3 position = new Vector3(0, 0, 0);
            //Act
            Vector3 returnPosition = m_Structure.CalculateGridPosition(position);
            //Assert
            Assert.AreEqual(Vector3.zero, returnPosition);
        }
 
        [Test]
        public void CalculateGridPositionFloatsPasses()
        {
 
            Vector3 position = new Vector3(2.9f, 0, 2.9f);
            //Act
            Vector3 returnPosition = m_Structure.CalculateGridPosition(position);
            //Assert
            Assert.AreEqual(Vector3.zero, returnPosition);
        }
 
        [Test]
        public void CalculateGridPositionFail()
        {
 
            Vector3 position = new Vector3(3.1f, 0, 0);
            //Act
            Vector3 returnPosition = m_Structure.CalculateGridPosition(position);
            //Assert
            Assert.AreNotEqual(Vector3.zero, returnPosition);
        }
        #endregion
    }
}
