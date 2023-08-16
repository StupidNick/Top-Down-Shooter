using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class MeshRenderComponent : MonoBehaviour
{
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private float _fadeInTime;
    [SerializeField] private Transform[] _extremePoints;
    private bool _isVisible = true;

    private Material _material;



    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }


    public void FadeOutObject()
    {
        if (_isVisible == false) return;
        
        _isVisible = false;
        if (gameObject.activeSelf)
        {
            StartCoroutine(FadeOut());
        }
    }
    
    public void FadeInObject()
    {
        if (_isVisible == true) return;

        _isVisible = true;
        StartCoroutine(FadeIn());
    }


    private IEnumerator FadeOut()
    {
        Color color = _material.color;
        for (float alpha = color.a; alpha >= 0; alpha -= _fadeOutTime * Time.deltaTime)
        {
            if (_isVisible == true)
            {
                break;
            }
            color.a = alpha;
            _material.color = color;
            yield return null;
        }
    }
    
    
    private IEnumerator FadeIn()
    {
        Color color = _material.color;
        for (float alpha = color.a; alpha < 1; alpha += _fadeInTime * Time.deltaTime)
        {
            if (_isVisible == false)
            {
                break;
            }
            color.a = alpha;
            _material.color = color;
            yield return null;
        }
    }


    public bool CheckCanSee(Transform startPoint, float FOV, float lengthOfFOV)
    {
        if (!CheckInFOV(startPoint, FOV, lengthOfFOV)) return false;

        foreach (var point in _extremePoints)
        {
            RaycastHit hitInfo = new RaycastHit();
            Physics.Linecast(startPoint.transform.position, point.position, out hitInfo);
            if (hitInfo.collider == null)
            {
                continue;
            }
            if (hitInfo.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }


    private bool CheckInFOV(Transform viewPoint, float FOV, float lengthOfFOV)
    {
        Vector3 distance = transform.position - viewPoint.position;
        if (distance.magnitude > lengthOfFOV) return false;
        
        if (Mathf.Abs(Vector3.SignedAngle(distance, viewPoint.forward, Vector3.up)) > FOV/2) return false;
        return true;
    }


    public bool IsVisible
    {
        get
        {
            return _isVisible;
        }
    }
}
