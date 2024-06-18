using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using AStar;

public class SimpleEnemyDecorator : EnemyAbstractDecorator
{
    public Vector3? Direcection;


#region Этап инициализации
    
    void Start() => ChooseDirection();
    void Awake() => base.Awake();
    

    public override void ChooseDirection()
    {
        Direcection = Enemy.ChooseDirection(this.mapFabric);

        if (Direcection != null)
        {
            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, (Vector3)Direcection);
        }
    }
#endregion

#region "Этап перемещения"
    public override async Task MakeMove()
    {

        if (Direcection == null)  
            ChooseDirection();


        int dx = (int)Math.Ceiling(Direcection!.Value.x), dy = (int)Math.Ceiling(Direcection!.Value.y);

        int id = mapFabric.GetId(x: dx, y: dy);

        if (Direcection != null && id is 0 or 6)
        {
            LineRenderer.enabled = false;

            int x = (int)Math.Ceiling(this.transform.position.x), y = (int)Math.Ceiling(this.transform.position.y);
 

            // очистить клетку первой позиции
            mapFabric.SetId(new Point() { Id = 0, x = x, y = y });


            while (Direcection.Value.x != transform.position.x || Direcection.Value.y != transform.position.y)
            {
                float step = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, (Vector3)Direcection, step);
                await Task.Yield();
            }

            AudioSource.PlayOneShot(SoundClip);

            x = (int)Math.Ceiling(this.transform.position.x);
            y = (int)Math.Ceiling(this.transform.position.y);

            this.transform.position = new Vector3((float)x, (float)y);

            Point thisPoint = new Point() { Id = Enemy.Id, x = x, y = y };

            // проверить столкнулись ли мы с игроком 
            if (mapFabric.GetId(thisPoint) == 6) 
                StartCoroutine(mapFabric.PlayerControllerlayer.Deth());

            // поставить метку на новую позицию
            mapFabric.SetId(thisPoint); 

            ChooseDirection();
        }
    }
#endregion

}
