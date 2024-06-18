using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EnemyAbstractDecorator : MonoBehaviour 
{


    public LineRenderer LineRenderer;
    public AudioClip SoundClip;
    public AudioSource AudioSource;
    public MapFabric mapFabric;
    
    public IEnemy Enemy { get; set; } 
    public float Speed { get; set; } = 7f;


    public virtual void ChooseDirection() { }
    public virtual async Task MakeMove() {}

    public void AwakeAndWriteSelf()
    {
        Enemy.AwakeAndWriteSelf(ref this.mapFabric);
        this.mapFabric.Enemys.Add(this);
    }

    public void Awake()
    {
        Enemy = this.gameObject.GetComponent<IEnemy>();
        mapFabric = GameObject.Find("MapFabric").GetComponent<MapFabric>();

        if (Enemy == null)
            throw new Exception("Повесь противника на префаб");

        if (mapFabric == null)
            throw new Exception("где MapFabric на сцене ?");

        AwakeAndWriteSelf();
    }

}
