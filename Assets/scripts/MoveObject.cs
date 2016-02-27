using UnityEngine;
using System.Collections;

public abstract class MoveObject : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected BoxCollider2D bc2d;

  
    
    protected virtual bool AttemptMove(int h, int v)
    {
        //private bool AttemptMove<T>(int h, int v)
        RaycastHit2D hit2D;
        Vector2 startPos = rb2d.position;
        Vector2 endPos = startPos + new Vector2(h, v);
        bc2d.enabled = false;
        hit2D = Physics2D.Linecast(startPos, endPos);
        bc2d.enabled = true;

        if (hit2D.transform == null)
        {
            StartCoroutine(Moveing(endPos));
            return true;
        }
        var t = hit2D.transform.GetComponent<ICanbeFuck>();//此时参数就死写了,只能处理ICanbefuck类,如果写成<T>,则可以继续复写
        //var t = hit2D.transform.GetComponent<T>();
        DoSomething(t);
        return false;


    }

    protected abstract void DoSomething(ICanbeFuck t);//此时参数就死写了,只能处理ICanbefuck类,如果写成<T>,则可以继续复写
    //protected abstract void DoSomething<T>(T t);

    private IEnumerator Moveing(Vector2 endPos)
    {
        float sqrRemainingDistance = (rb2d.position - endPos).sqrMagnitude;
        //获取两点间的距离
        //While that distance is greater than a very small amount (Epsilon, almost zero):
        //当两点间的距离大于一个非常小的数接近于零 
        while (sqrRemainingDistance > float.Epsilon)
        {

            //Find a new position proportionally closer to the end, based on the moveTime
            Vector2 newPostion = Vector2.MoveTowards(rb2d.position, endPos, 10 * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2d.MovePosition(endPos);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (rb2d.position - endPos).sqrMagnitude;
            print(sqrRemainingDistance > float.Epsilon);

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }
}
