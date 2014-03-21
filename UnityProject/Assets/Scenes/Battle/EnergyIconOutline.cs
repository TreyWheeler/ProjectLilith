using UnityEngine;
using System.Collections;
using System;

public class EnergyIconOutline : MonoBehaviour
{
    private static string defaultName = "Outline";

    public int Diameter = 115;
    public int Depth = 5;
    public Color Color = new Color(1, 0, 0);    
    public float DegreeStart = 0;
    public float DegreeEnd = 360;
    public bool Clockwise = true;
    public Func<float> ProgressDelegate;
    public string Name = defaultName;

    private UISprite _sprite;
    private float _totalPerimeterPercentage;

    void Start()
    {
        GameObject outline = new GameObject();
        outline.transform.parent = this.gameObject.transform;
        outline.transform.localPosition = Vector3.zero;
        outline.name = Name;

        _sprite = outline.EnsureComponent<UISprite>();
        _sprite.atlas = Resources.Load<GameObject>("GUI/Atlas").GetComponent<UIAtlas>();
        _sprite.spriteName = "Radiance";
        _sprite.type = UISprite.Type.Filled;
        _sprite.fillDirection = UISprite.FillDirection.Radial360;
        _sprite.invert = Clockwise;
        _sprite.transform.localScale = new Vector3(1, 1, 1);
        _sprite.width = Diameter;
        _sprite.height = Diameter;
        _sprite.depth = Depth;
        _sprite.color = Color;

        _totalPerimeterPercentage = (DegreeEnd - DegreeStart) / 360f;
    }

    void Update()
    {
        float progressToNextTick = ProgressDelegate();
        _sprite.fillAmount = progressToNextTick * _totalPerimeterPercentage;
        _sprite.transform.rotation = Quaternion.identity;
        if (Clockwise)
        {
            _sprite.transform.eulerAngles = new Vector3(0, 0, 360f - DegreeStart);
        }
        else
        {
            _sprite.transform.eulerAngles = new Vector3(0, 0, DegreeStart);
        }
    }


}
