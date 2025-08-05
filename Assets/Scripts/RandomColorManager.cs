using System.Collections;
using UnityEngine;

public class RandomColorManager : MonoBehaviour
{
    public Camera mainCamera;    
    public float changeInterval = 2f;

    void Start()
    {
        StartCoroutine(ChangeBackgroundColor());
    }

    IEnumerator ChangeBackgroundColor()
    {
        while (true)
        {
            mainCamera.backgroundColor = GetRandomColor();
            yield return new WaitForSeconds(changeInterval);
        }
    }

    Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
