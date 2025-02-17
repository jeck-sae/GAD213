using Pixelplacement;
using System.Collections;
using UnityEngine;

public class MoveRelativeToCursor : MonoBehaviour
{
    [SerializeField] Transform icon;
    [SerializeField] SpriteRenderer[] renderers;

    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField] AnimationCurve positionCurve;
    [SerializeField] AnimationCurve alphaCurve;

    public static float intensity = 1;
    private void Awake()
    {
        renderers = icon.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        var cursorPos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = transform.position.z;

        var dir = (cursorPos - transform.position).normalized;
        var dist = Vector3.Distance(transform.position, cursorPos) * 2;
        

        //selected tile (spaghetti)
        if (UnitManager.Instance.SelectedTile)
        {
            var dist2 = Vector3.Distance(transform.position, UnitManager.Instance.SelectedTile.transform.position);
            if (dist2 < dist)
            {
                dist = dist2;
                dir = (UnitManager.Instance.SelectedTile.transform.position - transform.position).normalized;
            }
        }

        //moving unit (spaghetti)
        if (UnitMovement.Instance.isMoving)
        {
            var dist2 = Vector3.Distance(transform.position, UnitMovement.Instance.transform.position);
            if (dist2 < dist)
            {
                dist = dist2;
                dir = (UnitMovement.Instance.transform.position - transform.position).normalized;
            }
        }
        ////

        icon.transform.localScale = Vector3.one * scaleCurve.Evaluate(dist * intensity);
        
        
        //position
        //icon.transform.localPosition = dir * positionCurve.Evaluate(dist * intensity);
        
        //alpha
        /*float a = alphaCurve.Evaluate(dist);
        foreach(SpriteRenderer renderer in renderers)
        {
            var col = renderer.color;
            col.a = a;
            renderer.color = col;
        }*/
    }



    public static void SetTileOffsetMultiplier(float value, float time)
    {
        if(time == 0)
        {
            intensity = value;
            return;
        }

        //temprorarily using tween instance
        Tween.Instance.StartCoroutine(SetTileOffsetMultiplierCoroutine(value, time));
    }

    static IEnumerator SetTileOffsetMultiplierCoroutine(float value, float time)
    {
        var curve = AnimationCurve.Linear(0, intensity, 1, value);

        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            intensity = curve.Evaluate(currentTime / time);
            yield return null;
        }
        intensity = value;
    }
}