using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergyIconWheel : MonoBehaviour
{
    private PositionButtons _positionButtons;
    private string _iconName = "notch";
    private int dimensions2D = 30;
    public List<GameObject> icons = new List<GameObject>();
    public IEnergy energy;
    public bool constant;

    void Awake()
    {
        _positionButtons = this.EnsureComponent<PositionButtons>();
        _positionButtons.FixedStep = 20;
        _positionButtons.startAngle = 315;
        _positionButtons.endAngle = 45;
        _positionButtons.radius = (this.EnsureComponent<UITexture>().width / 2) + (dimensions2D / 2);
    }
    void Start()
    {
        int maxEnergy = energy.GetMaxEnergy();
        _positionButtons.Clear();
        for (int i = 0; i < maxEnergy; i++)
        {
            GameObject go = _positionButtons.Add(_iconName);
            UITexture texture = go.EnsureComponent<UITexture>();
            texture.width = dimensions2D;
            texture.height = dimensions2D;
            icons.Add(go);
        }
        _positionButtons.Layout();
    }
    void Update()
    {
        if (!constant)
        {
            int currentEnergy = energy.GetCurrentEnergy();
            for (int i = 0; i < icons.Count; i++)
            {
                icons[i].SetActive(i < currentEnergy);
            }
        }
    }
}
