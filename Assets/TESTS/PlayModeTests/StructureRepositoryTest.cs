using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class StructureRepositoryTest
    {
        StructureRepository repository;

        [OneTimeSetUp]
        public void Init()
        {
            CollectionSO collection = new CollectionSO();
            var road = new RoadStructureSO();
            road._buildingName = "Road";
            var facility = new SingleFacilitySO();
            facility._buildingName = "PowerPlant";
            var zone = new ZoneStructureSO();
            zone._buildingName = "Commercial";
            collection._roadStructure = road;
            collection._singleStructureList = new List<SingleStructureBaseSO>();
            collection._singleStructureList.Add(facility);
            collection._zonesList = new List<ZoneStructureSO>();
            collection._zonesList.Add(zone);
            GameObject testObject = new GameObject();
            repository = testObject.AddComponent<StructureRepository>();
            repository._modelDataCollection = collection;
        }

        [UnityTest]
        public IEnumerator StructureRepositoryTestZoneListQuantityPasses()
        {
            //yield return new WaitForEndOfFrame();
            //yield return new WaitForEndOfFrame();
            int quantity = repository.GetZoneNames().Count;
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(1, quantity);
        }
        [UnityTest]
        public IEnumerator StructureRepositoryTestZoneListNamePasses()
        {
            string name = repository.GetZoneNames()[0];
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("Commercial", name);
        }

        [UnityTest]
        public IEnumerator StructureRepositoryTestSingleStructureListQuantityPasses()
        {
            //yield return new WaitForEndOfFrame();
            //yield return new WaitForEndOfFrame();
            int quantity = repository.GetSingleStructureNames().Count;
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(1, quantity);
        }
        [UnityTest]
        public IEnumerator StructureRepositoryTestSingleStructureListNamePasses()
        {
            string name = repository.GetSingleStructureNames()[0];
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("PowerPlant", name);
        }

        [UnityTest]
        public IEnumerator StructureRepositoryTestRoadListNamePasses()
        {
            string name = repository.GetRoadStructureName();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("Road", name);
        }
    }
}
