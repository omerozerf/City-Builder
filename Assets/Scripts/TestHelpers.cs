using NSubstitute;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public static class TestHelpers
{
    public static StructureRepository CreateStructureRepositoryContainingRoad()
    {
        StructureRepository structureRepository = Substitute.For<StructureRepository>();
        CollectionSO collection = new CollectionSO();
        RoadStructureSO road = new RoadStructureSO();
        road._buildingName = "Road";
        road._prefab = GetAGameObjectWithMaterial();
        collection._roadStructure = road;
        structureRepository._modelDataCollection = collection;
        return structureRepository;
    }

    public static GameObject GetAGameObjectWithMaterial()
    {
        GameObject roadChild = new GameObject("Road", typeof(MeshRenderer));
        var renderer = roadChild.GetComponent<MeshRenderer>();
        var materialBlue = Resources.Load("BlueMaterial", typeof(Material)) as Material;
        renderer.material = materialBlue;
        GameObject roadPrefab = new GameObject("Road");
        roadChild.transform.SetParent(roadPrefab.transform);
        return roadPrefab;
    }
}
