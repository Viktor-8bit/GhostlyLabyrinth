using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

using AStar;

public class PlayerController : MonoBehaviour, IObject
{
    public Point PlayerPoint;

    public AudioClip SoundClip;
    public AudioSource AudioSource;
    public ParticleSystem ParticleSystem;

    public TMPmanager YouDie;
    public TMPmanager YouWin;

    private MapFabric _mapFabric;

    // почему тут 3 id ?
    private int _id;
    public int id;
    public int Id
    {
        get
        {
            return this.id;
        }
    }
    public float speed;
    public bool ICanMove = true;
    private bool _die = false;



    public void SetPlayerPoint()
    {
        this.PlayerPoint = new Point()
        {
            Id = this.Id,
            x = (int)Math.Ceiling(this.transform.position.x),
            y = (int)Math.Ceiling(this.transform.position.y)
        };
    }

    void Awake()
    {

        YouDie = GameObject.Find("YouDie").GetComponent<TMPmanager>();
        YouWin = GameObject.Find("YouWin").GetComponent<TMPmanager>();

        _mapFabric = GameObject.Find("MapFabric").GetComponent<MapFabric>();

        if (_mapFabric == null)
            throw new Exception("где _mapFabric на сцене ?");
        else
            AwakeAndWriteSelfId();

        if (YouDie == null)
            throw new Exception("где надпись ¬ы погибли ?");

        if (YouWin == null)
            throw new Exception("где надпись ¬ы выиграли ?");
    }

    async void Update()
    {
        int x = (int)Math.Ceiling(this.transform.position.x), y = (int)Math.Ceiling(this.transform.position.y);

        #region "может ли игрок походить"

        if (_mapFabric!.GetId(x: x + 1, y: y) is not 0 or 12     &&
            _mapFabric!.GetId(x: x - 1, y: y) is not 0 or 12     &&
            _mapFabric!.GetId(x: x,     y: y - 1) is not 0 or 12 &&
            _mapFabric!.GetId(x: x,     y: y + 1) is not 0 or 12)
            StartCoroutine(Deth());

        #endregion


        if (Input.anyKeyDown && ICanMove && !_die)
        {
            switch (Input.inputString.ToLower())
            {
                case "ц":
                case "w":   // код дл€ нажати€ клавиши W
                    this._id = _mapFabric!.GetId(new Point() { x = x, y = y + 1, Id = this.Id });
                    if (this._id is 0 or 12)
                    {
                        AudioSource.PlayOneShot(SoundClip);
                        ICanMove = false;
                        await MoveToTargetAsync(new Vector3(x, y + 1));
                    }
                    break;

                case "ф":
                case "a":   // код дл€ нажати€ клавиши A
                    this._id = _mapFabric!.GetId(new Point() { x = x - 1, y = y, Id = this.Id });
                    if (this._id is 0 or 12)
                    {
                        AudioSource.PlayOneShot(SoundClip);
                        ICanMove = false;
                        await MoveToTargetAsync(new Vector3(x - 1, y));
                    }
                    break;

                case "ы":
                case "s":   // код дл€ нажати€ клавиши S
                    this._id = _mapFabric!.GetId(new Point() { x = x, y = y - 1, Id = this.Id });
                    if (this._id is 0 or 12)
                    {
                        AudioSource.PlayOneShot(SoundClip);
                        ICanMove = false;
                        await MoveToTargetAsync(new Vector3(x, y - 1));
                    }
                    break;

                case "в":
                case "d":   // код дл€ нажати€ клавиши D
                    this._id = _mapFabric!.GetId(new Point() { x = x + 1, y = y, Id = this.Id });
                    if (this._id is 0 or 12)
                    {
                        AudioSource.PlayOneShot(SoundClip);
                        ICanMove = false;
                        await MoveToTargetAsync(new Vector3(x + 1, y));
                    }
                    break; 
            }
        }
    }


    async Task MoveToTargetAsync(Vector3 target)
    {
        float x = target.x, y = target.y;
        target.z = transform.position.z;

        int _x = (int)Math.Ceiling(this.transform.position.x), _y = (int)Math.Ceiling(this.transform.position.y);

        // remove past id
        _mapFabric.SetId(new Point() { Id = 0, x = _x, y = _y } );


        while (x != transform.position.x || y != transform.position.y)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            await Task.Yield();
        }

        this.transform.position = new Vector3(
            (float)(Math.Ceiling(this.transform.position.x)),
            (float)(Math.Ceiling(this.transform.position.y))
        );

        if (_mapFabric.GetId(x: (int)Math.Ceiling(this.transform.position.x), y: (int)Math.Ceiling(this.transform.position.y)) == 12)
            StartCoroutine(Win());
        else
        {
            // set new id
            Point point = new Point()
            {
                Id = Id, x = (int)Math.Ceiling(this.transform.position.x),
                y = (int)Math.Ceiling(this.transform.position.y)
            };
            _mapFabric.SetId(point);
            this.PlayerPoint = point;

            // уведомл€ем фабрику о том, что игрок походил и ждем ходов противников
            await _mapFabric.EnemysMakeMove();
            ICanMove = true;
        }
    }

    public IEnumerator Win()
    {
        ICanMove = false;
        YouWin.Activate();
        yield return new WaitForSeconds(2.5f);

        //TODO убрать TODO
        //TODO вернутьс€ в меню выбора уровн€
        SceneManager.LoadScene("Menu");

    }

    public IEnumerator Deth()
    {
        _die = true;
        YouDie.Activate();
        ParticleSystem.Play();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AwakeAndWriteSelfId()
    {
        int x = (int)Math.Ceiling(this.transform.position.x), y = (int)Math.Ceiling(this.transform.position.y);
        Point point = new Point() { Id = this.Id, x = x, y = y };
        _mapFabric!.SetId(point);
    }

    // TODO что это такое ?
    public void DestroyAndRemoveSelfId() { }
}
