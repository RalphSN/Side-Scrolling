using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttacker
{
    [SerializeField]
    private GameObject fireballPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float attackCooldown = 3f;

    [SerializeField]
    private int attackPower = 1;
    private float lastAttackTime = -999f;
    private Animator animator;
    public int AttackPower
    {
        get { return attackPower; }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(int facingDirection)
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        lastAttackTime = Time.time;
        animator.SetTrigger("attack");
        GameObject fireballObj = Instantiate(
            fireballPrefab,
            firePoint.position,
            Quaternion.identity
        );
        Fireball fireball = fireballObj.GetComponent<Fireball>();
        fireball.SetPower(attackPower);
        fireball.SetDirection(facingDirection);
    }
}
