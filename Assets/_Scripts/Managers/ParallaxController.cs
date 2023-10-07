using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    #region Variables
    [field: Header("Layers Settings")]
    [field: SerializeField] private List<Transform> _layers;
    [field: SerializeField] private List<Vector2> _parallaxScales;
    [field: SerializeField] private bool[] _mirrorLayers;

    [field: Header("Layers Data")]
    private List<Vector2> _layerSizes;
    private int _layerCount = 0;
    private List<Vector2> _initialPositions;
    #endregion

    private void Awake()
    {
        SetupLayers();
        SetupDuplicateLayers();
    }

    private void SetupLayers() //Checks how many layers there are and sets the size and initial position for each one.
    {
        _initialPositions = new List<Vector2>();
        _layerSizes = new List<Vector2>();
        for (int i = 0; i < _layers.Count; i++)
        {
            _layerCount++;
            _initialPositions.Add(_layers[i].position); //Gets the initial position of each layer.
            _layerSizes.Add(_layers[i].GetComponent<SpriteRenderer>().bounds.size); // Gets the size of each sprite.
        }
    }

    private void SetupDuplicateLayers() //Checks if there are any layers to duplicate and sets them up.
    {
        for (int i = 0; i < _layerCount; i++)
        {
            if (_mirrorLayers[i] == true)
            {
                //Gets the layer set for mirroring and the corresponding data and duplicates it.
                Transform mirroredLayer = Instantiate(_layers[i]);
                mirroredLayer.SetParent(this.transform);
                Vector2 duplicatedScale = _parallaxScales[i];
                Vector2 duplicatedSize = _layerSizes[i];

                // Use ternary operators for setting the initial position based on the parallax scale
                Vector2 duplicatedInitialPosition = _initialPositions[i];
                duplicatedInitialPosition.x += (duplicatedScale.x < 0) ? duplicatedSize.x : (duplicatedScale.x > 0) ? -duplicatedSize.x : 0;
                duplicatedInitialPosition.y += (duplicatedScale.y < 0) ? duplicatedSize.y : (duplicatedScale.y > 0) ? -duplicatedSize.y : 0;

                //Adds the layer and the corresponding data to the lists.
                _layers.Add(mirroredLayer);
                _parallaxScales.Add(duplicatedScale);
                _layerSizes.Add(duplicatedSize);
                _initialPositions.Add(duplicatedInitialPosition);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < _layers.Count; i++) //Goes through the list of layers and modifies the position according to the parallax scale.
        {
            float horParallax = Time.deltaTime * _parallaxScales[i].x;
            float verParallax = Time.deltaTime * _parallaxScales[i].y;

            float newXPosition = _layers[i].position.x + horParallax;
            float newYPosition = _layers[i].position.y + verParallax;

            //Checks whether the layer has travelled as long as its own length and sets back to the initial position.
            if (Mathf.Abs(newXPosition - _initialPositions[i].x) >= _layerSizes[i].x) newXPosition = _initialPositions[i].x;
            if (Mathf.Abs(newYPosition - _initialPositions[i].y) >= _layerSizes[i].y) newYPosition = _initialPositions[i].y;

            //Sets the new position.
            Vector2 targetPosition = new(newXPosition, newYPosition);
            _layers[i].position = targetPosition;
        }
    }

}


