using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Animator animator;
    
    protected bool facingRight;
    
    [SerializeField]
    protected float movementSpeed;

    [SerializeField]
    private GameObject knifePrefab;

    protected bool attack;

    // Start is called before the first frame update
    public virtual void Start()
    {
        facingRight = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1,1,1);
    }

    public virtual void ThrowKnife(int value)
    {
        if(facingRight)
        {
            GameObject temp = (GameObject)Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0,0,-90)));
            temp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            GameObject temp = (GameObject)Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0,0,90)));
            temp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }

}
