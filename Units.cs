using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public bool selected;
    GameMaster gm;
    public List<Tile> tiles;
    public double tileSpeed;
    public float attackRange;
    public bool hasMoved;
    public bool hasAttacked;
    public int Player;
    public int Turn;
    public int Health;
    public int defence;
    public int curHealth;
    public int attackDamage;
    public Transform attackPoint;
    public bool attackable;


    public float moveSpeed;

    private void Start()
    {
        defence = defence / 100;
        Turn = 1;
        gm = FindObjectOfType<GameMaster>();
        curHealth = Health;
    }

    private void OnMouseDown()
    {

        if (Turn % 2 == 1 && Player == 1)
        {


            if (selected == true)
            {
                selected = false;
                gm.selectedUnit = null;
                gm.ResetTiles();
            }
            else
            {


                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }

                selected = true;
                gm.selectedUnit = this;

                gm.ResetTiles();
                GetWalkableTiles();
                GetAttackableTiles();


            }

        }
        if (Turn % 2 == 0 && Player == 2)
        {


            if (selected == true)
            {
                selected = false;
                gm.selectedUnit = null;
                gm.ResetTiles();
            }
            else
            {


                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }

                selected = true;
                gm.selectedUnit = this;

                gm.ResetTiles();
                GetWalkableTiles();
                GetAttackableTiles();

            }

        }

    }

    void GetWalkableTiles()
    {

        if (hasMoved == true) {
            return;
        }

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            {
                if (tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }

    }
    void GetAttackableTiles()
    {

        if (hasAttacked == true)
        {
            return;
        }

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= attackRange)
            {
                tile.HighlightAttack();

            }
        }

    }

    public void Move(Transform movePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(movePos));
    }
    

    public void TakeDamage(int Damage)
    {
        curHealth -= Damage;
        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void isAttackable()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        
        if (hitEnemies.Length > 0)
        {

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.GetComponent<Units>())
                {
                    if (enemy.GetComponent<Units>().Player != Player)
                    {
                        bool isAttackable = true;
                        enemy.GetComponent<Units>().attackable = isAttackable;
                        if (Input.GetKeyDown(KeyCode.A) && hasAttacked == false)
                        {
                            enemy.GetComponent<Units>().TakeDamage(attackDamage);
                            hasAttacked = true;
                        }
                    }
                }


            }
        }
        else
            GetComponent<Units>().attackable = false;
    }

    IEnumerator StartMovement(Transform movePos)
    {


        float startTime = Time.time;
        float currentTime = Time.time;

        int posx = (int)transform.position.x;
        int moveposx = (int)movePos.position.x;

        int posy = (int)transform.position.y;
        int moveposy = (int)movePos.position.y;


        Debug.Log(transform.position);
        Debug.Log(movePos);

        while (posx != moveposx)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(movePos.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            //Debug.Log(transform.position);

            posx = (int)transform.position.x;
            moveposx = (int)movePos.position.x;
            yield return null;

        }
        transform.position = new Vector3(movePos.position.x, transform.position.y, transform.position.z);



        while (posy != moveposy)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, movePos.position.y), moveSpeed * Time.deltaTime);
            //Debug.Log(transform.position);

            posy = (int)transform.position.y;
            moveposy = (int)movePos.position.y;
            yield return null;

        }
        transform.position = new Vector3(transform.position.x, movePos.position.y, transform.position.z);



        hasMoved = true;

    }

    void Update()
    {
        isAttackable();

        EndTurn();

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void EndTurn()
    {
     if (Input.GetKeyDown(KeyCode.Space))
        {
            Turn += 1;
            hasMoved = false;
            hasAttacked = false;
        }
    }



}