using System;
using System.Threading.Tasks;
using AStar;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ComplexEnemyDecorator : EnemyAbstractDecorator
{

    private int _moveCount;
    Vector3[]? Direcection;


#region Этап инициализации

    void Start()
    {
        _moveCount = Enemy.MoveCount;
        ChooseDirection();
    }

    void Awake()
    {
        base.Awake();
    }

    public override void ChooseDirection()
    {
        Direcection = new Vector3[_moveCount];
     
        for (int i = 0; i < _moveCount; i++)
        {
            var temp = Enemy.ChooseDirection(this.mapFabric);

            if (temp == null)
            {
                Direcection = null;
                break;
            }
            else
                Direcection[i] = (Vector3)temp;
        }

        if (Direcection != null)
        {
            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0, this.transform.position);
            for (int i=0; i<Direcection.Length; i++)
                LineRenderer.SetPosition(i+1, (Vector3)Direcection[i]);
        }
    }

#endregion


#region "Этап перемещения"


    public override async Task MakeMove()
    {
        ChooseDirection();

        if (Direcection != null)
        {
            int x = (int)Math.Ceiling(this.transform.position.x), y = (int)Math.Ceiling(this.transform.position.y);
            mapFabric.SetId(new Point() { Id = 0, x = x, y = y });

            for (int i = 0; i < Direcection.Length; i++)
            {
                await this.Move(Direcection[i]);
            }


            x = (int)Math.Ceiling(this.transform.position.x);
            y = (int)Math.Ceiling(this.transform.position.y);

            Point thisPoint = new Point() { Id = Enemy.Id, x = x, y = y };
            mapFabric.SetId(thisPoint);

            ChooseDirection();
        }
    }

    public async Task Move(Vector3 direction)
    {
        int dx = (int)Math.Ceiling(direction.x), dy = (int)Math.Ceiling(direction.y);
        int id = mapFabric.GetId(x: dx, y: dy);

        if (id is 0 or 6)
        {
            LineRenderer.enabled = false;

            int x = (int)Math.Ceiling(this.transform.position.x), y = (int)Math.Ceiling(this.transform.position.y);


            // очистить клетку первой позиции
            mapFabric.SetId(new Point() { Id = 0, x = x, y = y });


            while (direction.x != transform.position.x || direction.y != transform.position.y)
            {
                float step = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, direction, step);
                await Task.Yield();
            }

            AudioSource.PlayOneShot(SoundClip);

            x = (int)Math.Ceiling(this.transform.position.x);
            y = (int)Math.Ceiling(this.transform.position.y);

            this.transform.position = new Vector3((float)x, (float)y);

            Point thisPoint = new Point() { Id = Enemy.Id, x = x, y = y };

            // проверить столкнулись ли мы с игроком 
            if (mapFabric.GetId(thisPoint) == 6)
            {
                StartCoroutine(mapFabric.PlayerControllerlayer.Deth());
                Direcection = null;
            }
        }
    }

#endregion


}
