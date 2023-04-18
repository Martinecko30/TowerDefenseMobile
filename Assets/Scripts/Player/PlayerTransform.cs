using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerTransform : MonoBehaviour
{
    private List<GameObject> avaliableObjects = new List<GameObject>();
    private int index;

    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        avaliableObjects.Clear();
        foreach(var checkObject in GameObject.FindGameObjectsWithTag("Transformant"))
            if(checkObject.activeSelf && Vector3.Distance(transform.position, checkObject.transform.position) < 10f)
                avaliableObjects.Add(checkObject);

        FilterList();

        if(avaliableObjects.Count <= 0)
            return;

        if (index >= avaliableObjects.Count)
            index = 0;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Destroy(transform.GetChild(0).gameObject);
            var gameObject = FindClosest();
            transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }

    public void TransformPlayer()
    {
        Destroy(transform.GetChild(0).gameObject);

        CreateChild(avaliableObjects[index]);

        index++;

        outline.enabled = false;
        outline.enabled = true;
    }

    private List<GameObject> FilterList()
    {
        List<GameObject> filteredList = new List<GameObject>();
        for (int i = 0; i < avaliableObjects.Count; i++)
        {
            if (avaliableObjects[i].name == "chair")
            {
                var temp = avaliableObjects[i];
                break;
            }
        }
        return filteredList;
    }

    private GameObject CreateChild(GameObject gameObject)
    {
        var child = Instantiate(gameObject);
        child.transform.SetParent(transform);
        child.transform.position = transform.position;
        child.transform.rotation = transform.rotation;

        return child;
    }

    private GameObject FindClosest()
    {
        GameObject closestGameObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject go in avaliableObjects)
        {
            float distance = Vector3.Distance(transform.position, go.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGameObject = go;
            }
        }

        return closestGameObject;
    }
}
