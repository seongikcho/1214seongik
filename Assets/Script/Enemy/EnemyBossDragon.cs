using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossDragon : EnemyController
{
    public Transform playerPos;
    public Transform AttackBox; 
    public Vector2 AttackBoxSize;
    public Transform traceBox;
    public Vector2 traceBoxSize;
    public GameObject fireBall_L;
    public GameObject fireBall_R;
    public Transform fireBallPos_L;
    public Transform fireBallPos_R;
    public GameObject starFall;
    bool isTrace;
    int Think_var;
    private float coolTime;
    private float currentTime;
    private float shotSpeed;
    Vector2 spawnPos;
    int k;
    int var2;

    void Awake()
    {
        En = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
        CC = GetComponent<CapsuleCollider2D>();
        EnemyCurrentHp = EnemyHP;
        hp = EnemyCurrentHp;
        enemyHpSlider.gameObject.SetActive(false);
        SR.flipX = h == -1; // ������ ���� �̵��� ���缭 �ٲٱ�
        EnemyAtkDmg = EnemyAtk;
        isTrace = false;
        coolTime = 1.2f;
        currentTime = coolTime;
        shotSpeed = 10f;

        k = 0;
        var2 = 0;
    }
    void Update()
    {
        if (Mathf.Abs(En.velocity.x) < 0.3)
        {
            animation.SetBool("isWalk", false);
        }
        else
        {
            animation.SetBool("isWalk", true);
        }
        enemyHpSlider.maxValue = EnemyHP;
        enemyHpSlider.value = EnemyCurrentHp;
        enemyHpSlider.transform.position = hudPos.position;

        if (EnemyCurrentHp != hp && EnemyCurrentHp > 0)
        {
            float dmg = EnemyCurrentHp - hp;
            GameObject hudText = Instantiate(hudDamageText);
            hudText.transform.position = hudPos.position;
            hudText.GetComponent<DamageText>().damage = (int)dmg;
            EnemyCurrentHp = hp;

            ActiveHpSlider();

            if (EnemyCurrentHp <= 0)
            {
                SR.color = new Color(1, 0, 1, 0.5f);
                SR.flipY = true;
                CC.enabled = false;
                Invoke("DeActive", 1);
                Invoke("UnActiveHpSlider", 1);
                StartCoroutine(DropItem(droppercent));
                StopMove();
                player.CurrentExp += EnemyExp;
                Invoke("DropMoney", 0.4f);

            }
            else
            {
                Invoke("UnActiveHpSlider", 5);
                En.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                SR.color = new Color(1, 0.9f, 0.02f, 0.5f);
                Invoke("ReturnEnemyColor", 1);
            }
        }
        Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(traceBox.position, traceBoxSize, 0);
        Collider2D[] collider2Ds_2 = Physics2D.OverlapBoxAll(AttackBox.position, AttackBoxSize, 0);
        foreach (Collider2D CC1 in collider2Ds_1)
        {
            if (CC1.tag == "Player")
            {
                isTrace = true;
                if (playerPos.transform.position.x < transform.position.x)
                {
                    En.velocity = new Vector2(-1 * 2 * speed, En.velocity.y);
                    SR.flipX = true;
                    foreach (Collider2D CC2 in collider2Ds_2)
                    {
                        if (CC2.tag == "Player")
                        {
                            En.velocity = new Vector2(-1 * 0.01f * speed, En.velocity.y);
                            if (currentTime <= 0)
                            {
                                animation.SetTrigger("isAttack");
                                StartCoroutine(FireBallAttack_L(CC2));
                                currentTime = coolTime;                                
                            }
                        }                                         
                    }

                }
                else if (playerPos.transform.position.x > transform.position.x)
                {
                    En.velocity = new Vector2(1 * 2 * speed, En.velocity.y);
                    SR.flipX = false;
                    foreach (Collider2D CC2 in collider2Ds_2)
                    {
                        if (CC2.tag == "Player")
                        {
                            En.velocity = new Vector2(1 * 0.01f * speed, En.velocity.y);
                            if (currentTime <= 0)
                            {
                                animation.SetTrigger("isAttack");
                                StartCoroutine(FireBallAttack_R(CC2));
                                currentTime = coolTime;
                            }
                        }                  
                    }
                }
            }
            else
            {
                isTrace = false;
            }

        }
        if (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
        }

        if(hp < EnemyHP*0.5f)
        {
            if (var2 == 0)
            {
                var2++;
                StartCoroutine(StarFall());
            }
        }
    }

    void FixedUpdate()
    {
        // �̵�
        //En.velocity = new Vector2(h * speed, En.velocity.y);

        //// ���� �̵� �� �ٴ� ����
        //if (En.velocity.x > 0 && isTrace == false)
        //{
        //    Debug.DrawRay(En.position + Vector2.right * 7f, Vector2.down*8f, new Color(0, 1, 0));
        //    RaycastHit2D rayHit = Physics2D.Raycast(En.position + Vector2.right * 7f, Vector2.down * 8f, 8, LayerMask.GetMask("normalfloor", "blockedfloor")); //from jeongik 1204
        //    if (rayHit.collider == null)
        //    {
        //        Turn();
        //    }
        //}

        //// ���� �̵� �� �ٴ� ����
        //else if (En.velocity.x < 0 && isTrace == false)
        //{
        //    Debug.DrawRay(En.position + Vector2.left * 7f, Vector2.down*8f, new Color(0, 1, 0));
        //    RaycastHit2D rayHit = Physics2D.Raycast(En.position + Vector2.left * 7f, Vector2.down * 8f, 8, LayerMask.GetMask("normalfloor", "blockedfloor")); //from jeongik 1204
        //    if (rayHit.collider == null)
        //    {
        //        Turn();
        //    }
        //}

    }

    //void Think()
    //{
    //    if (isTrace == false)
    //    {
    //        // ������ �ð� ���� �ൿ ���� ��ȭ (���� ���� ����)
    //        h = Random.Range(-1, 2);
    //        float thinktime = Random.Range(3f, 6f);
    //        Invoke("Think", thinktime); // thinktime���� think ��ȣ�� ȸ��

    //        if (h != 0)
    //        {
    //            SR.flipX = h == -1;
    //        }
    //    }
    //}

    //void Turn()
    //{

    //    // �̵� ���� ��ȭ
    //    h *= (-1);
    //    SR.flipX = h == 1;

    //    CancelInvoke(); // �̵� �ٲٸ� �ð� �ʱ�ȭ
    //    Invoke("Think", 5);
    //}
    IEnumerator Trace()
    {       
        Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(traceBox.position, traceBoxSize, 0);
        Collider2D[] collider2Ds_2 = Physics2D.OverlapBoxAll(AttackBox.position, AttackBoxSize, 0);
        foreach (Collider2D CC1 in collider2Ds_1)
        {
            if (CC1.tag == "Player")
            {
                isTrace = true;
                if (playerPos.transform.position.x < transform.position.x) 
                {
                    En.velocity = new Vector2(-1 * 2 * speed, En.velocity.y);
                    SR.flipX = true;
                    foreach (Collider2D CC2 in collider2Ds_2)
                    {
                        if (CC2.tag == "Player")
                        {
                            En.velocity = new Vector2(-1 * 0.01f * speed, En.velocity.y);
                            animation.SetBool("isMeleeAttack",true);
                            yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            animation.SetBool("isMeleeAttack", false);
                        }
                    }
                    
                }
                else if (playerPos.transform.position.x > transform.position.x)
                {
                    En.velocity = new Vector2(1 * 2 * speed, En.velocity.y);
                    SR.flipX = false;
                    foreach (Collider2D CC2 in collider2Ds_2)
                    {
                        if (CC2.tag == "Player")
                        {
                            En.velocity = new Vector2(1 * 0.01f * speed, En.velocity.y);
                            animation.SetBool("isMeleeAttack", true);
                            yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            animation.SetBool("isMeleeAttack", false);
                        }
                    }
                }
            }
            else
            {
                isTrace = false;             
            }

        }
        //yield return new WaitForSeconds(5f);
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(traceBox.position,traceBoxSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(AttackBox.position, AttackBoxSize);
    }

  IEnumerator FireBallAttack_L(Collider2D collider)
    {
        yield return new WaitForSeconds(0.1f);
        Vector2 newbulletPos = collider.transform.position - fireBallPos_L.position;
        newbulletPos = newbulletPos.normalized;
        GameObject fireBallcopy = Instantiate(fireBall_L, fireBallPos_L.position, transform.rotation);
        Rigidbody2D fireBallRigid = fireBallcopy.GetComponent<Rigidbody2D>();
        fireBallRigid.velocity = Mathf.Abs(shotSpeed) * newbulletPos * 1.5f;
        yield break;
    }
    IEnumerator FireBallAttack_R(Collider2D collider)
    {
        yield return new WaitForSeconds(0.1f);
        Vector2 newbulletPos = collider.transform.position - fireBallPos_R.position;
        newbulletPos = newbulletPos.normalized;
        GameObject fireBallcopy = Instantiate(fireBall_R, fireBallPos_R.position, transform.rotation);
        Rigidbody2D fireBallRigid = fireBallcopy.GetComponent<Rigidbody2D>();
        fireBallRigid.velocity = Mathf.Abs(shotSpeed) * newbulletPos * 1.5f;
        yield break;
    }
    IEnumerator StarFall()
    {
        yield return new WaitForSeconds(0.5f);

        while (k < 15)
        {
            GetRandomPosition();
            GameObject starFallCopy = Instantiate(starFall, spawnPos, transform.rotation);
            k++;
            yield return new WaitForSeconds(0.5f);
        }
        
        //Rigidbody2D fireBallRigid = fireBallcopy.GetComponent<Rigidbody2D>();
        //fireBallRigid.velocity = Mathf.Abs(shotSpeed) * newbulletPos * 1.5f;
        yield break;
    }
    private Vector3 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;
        Vector2 size = traceBoxSize;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y+(4.5f) + Random.Range(-size.y / 2f, size.y / 2f);


        spawnPos = new Vector2(posX, posY);
        return spawnPos;
    }
}