using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using AStar;
using JetBrains.Annotations;

#pragma warning disable CS4014
public class MapFabric : MonoBehaviour
{
    [CanBeNull] public DebugLogger Logger;

    public PlayerController PlayerControllerlayer;

    public List<GameObject> MapObjects = new List<GameObject>();

    public List<EnemyAbstractDecorator> Enemys = new List<EnemyAbstractDecorator>();

    private Coardinates Coardinates { get; } = new Coardinates();

    public Coardinates CalculatedCoardinates;
    private DistanceCalculator DistanceCalculator { get; set; }

    public void Start()
    {
        if (Logger != null)
            Logger.MakeSliceAndSend(this.Coardinates); // передаем ссылку на карту
        else
            Debug.Log("точно не нужно повесить дебаггер ?");

        Enemys.Sort((x, y) => y.Enemy.MoveCount.CompareTo(x.Enemy.MoveCount));


        string deb = "";
        
        foreach (var enemy in Enemys)
            deb += $" {enemy.Enemy} ";
        
        Debug.Log(deb);
    }

    public void Awake()
    {
        PlayerControllerlayer.SetPlayerPoint();
        this.DistanceCalculator = new DistanceCalculator(map: this.Coardinates);
        CalculatedCoardinates = this.DistanceCalculator.CalculateMap(
            playerPoint: PlayerControllerlayer.PlayerPoint
        );
    }


    public async Task EnemysMakeMove()
    {

        CalculatedCoardinates = this.DistanceCalculator.CalculateMap(
            playerPoint: PlayerControllerlayer.PlayerPoint
        );

        for (int i=0; i < Enemys.Count; i++)
        {
            if (i < Enemys.Count - 2)
            {
                if (Enemys[i + 1].Enemy.MoveCount < Enemys[i].Enemy.MoveCount)
                    await Enemys[i].MakeMove();
                else
                    Enemys[i].MakeMove();
            }
            else
            {
                await Enemys[i].MakeMove();
            }
        }

        if (Logger != null)
            Logger.MakeSliceAndSend(this.Coardinates); // CalculatedCoardinates
    }

    public void SetId(Point point)
    {
        this.Coardinates.SetValue(point);
    }

    public int GetId(Point point)
    {
        return this.Coardinates.GetValue(point);
    }

    public int GetId(int x, int y)
    {
        return this.Coardinates.GetValue(new Point() {x=x, y=y});
    }

}
