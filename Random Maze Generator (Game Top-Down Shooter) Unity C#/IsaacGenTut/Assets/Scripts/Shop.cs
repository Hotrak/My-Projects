using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public bool isBomb;
	public bool isShilt;
    public bool isFrize;
    public bool isLifeStill;

    private GameObject KeyHelp;
    PlayerControls player;
    Inventar inventar;
    GameMenu mianMenu;

	void Start () {

        KeyHelp = Resources.Load("E") as GameObject;
        player = FindObjectOfType<PlayerControls>();
        inventar = FindObjectOfType<Inventar>();
        mianMenu = FindObjectOfType<GameMenu>();
        CheckPlayer();

    }

    void CheckPlayer()
    {
        if (isFrize && player.isFrize)
            gameObject.SetActive(false);
        if (isLifeStill && player.isLifeStill)
            gameObject.SetActive(false);
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E)&& isActice)
        {
            if (isBomb)
            {
                if (inventar.slizeCount >= 5)
                {
                    inventar.boombCount++;
                    inventar.slizeCount -= 5;
                }

            }
            else if (isShilt)
            {
                if (inventar.slizeCount >= 5)
                {
                    inventar.shiltCount++;
                    inventar.slizeCount-=5;
                }
            }
            else if (isFrize)
            {
                if (inventar.slizeCount >= 30)
                {
                    player.isFrize = true;
                    inventar.slizeCount -= 30;
                    this.gameObject.SetActive(false);
                    Destroy(go);
                    mianMenu.CloseShopMessage();


                }
            }
            else if (isLifeStill)
            {
                if (inventar.slizeCount >= 50)
                {
                    player.isLifeStill = true;
                    inventar.slizeCount -= 50;
                    this.gameObject.SetActive(false);
                    Destroy(go);
                    mianMenu.CloseShopMessage();

                }
            }
        }

	}
    GameObject go;
    bool isActice;
   
    //bool isStay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            go = Instantiate(KeyHelp,new Vector2(transform.position.x, transform.position.y-2f),Quaternion.identity);

            isActice = true;
          
            if (isFrize)
            {
                mianMenu.ShowShopMessage("3AMoro3ka", "Позволяет заморозить врага  на короткое время");
                
            } else if (isLifeStill)
            {
                mianMenu.ShowShopMessage("REMONT", "За убийство врага восcтанавливается здоровье");

            }
            else if (isBomb)
            {
                mianMenu.ShowShopMessage("BOOMBA", "Наносит урон врагам и ломает ящики в небольшом радиусе");

            }
            else if (isShilt)
            {
                mianMenu.ShowShopMessage("CTEROUD", "Полностью восстанавливает здоровье и улучшает персонажа на 20 секунд");

            }




        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(go);
            isActice = false;
            mianMenu.CloseShopMessage();
        }
    }
    

}
