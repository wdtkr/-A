using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zako1 : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動するか")] public bool nonVisible;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    #endregion


    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!oc.playerStepOn)
        {
 
        if (sr.isVisible || nonVisible)
        {
            if (checkCollision.isOn)
            {
                rightTleftF = !rightTleftF;
            }

            //行動する
            int xVector = -1;
            if (rightTleftF)
            {
                xVector = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            rb.velocity = new Vector2(xVector * speed, -gravity);
        }
        else
        {
            rb.Sleep();
        }
        }
        else
        {
            if (!isDead)
            {
                anim.Play("enemy_zako1_death");
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
                Destroy(gameObject, 3f);

            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 5));
            }
        }
    }
}
