using UnityEngine;

public class MoveRelativeToCursor : MonoBehaviour
{
    [SerializeField] Transform icon;
    [SerializeField] SpriteRenderer[] renderers;

    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField] AnimationCurve positionCurve;
    [SerializeField] AnimationCurve alphaCurve;


    private void Awake()
    {
        renderers = icon.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        var cursorPos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = transform.position.z;

        var dir = (cursorPos - transform.position).normalized;
        var dist = Vector3.Distance(transform.position, cursorPos);

        icon.transform.localScale = Vector3.one * scaleCurve.Evaluate(dist);
        icon.transform.localPosition = dir * positionCurve.Evaluate(dist);

        /*float a = alphaCurve.Evaluate(dist);
        foreach(SpriteRenderer renderer in renderers)
        {
            var col = renderer.color;
            col.a = a;
            renderer.color = col;
        }*/
    }
}