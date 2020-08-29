using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HurtPlayerOnHit : MonoBehaviour
{
    public float minVelocityToHurt;

    void OnCollisionEnter2D(Collision2D coll)
    {
        var rigidbody = GetComponent<Rigidbody2D>();

        Health playerHealth = coll.gameObject.GetComponent<Health>();
        if (playerHealth == null)
            return;
        var otherRigidbody = coll.rigidbody;

        var maxVelocity = Mathf.Max(Mathf.Abs(rigidbody.velocity.x), Mathf.Abs(rigidbody.velocity.y),
            Mathf.Abs(otherRigidbody.velocity.x), Mathf.Abs(otherRigidbody.velocity.y));
        //TODO: подумать, как правильно обрабатывать сценарий, что если очень близко объекты, чтобы не дамажили друг друга
        if (//rigidbody.Distance(otherRigidbody.gameObject.GetComponent<Collider2D>()).distance >= minDistanceToHurt &&
            (maxVelocity == Mathf.Abs(rigidbody.velocity.x) || maxVelocity == Mathf.Abs(rigidbody.velocity.y)))
        {
            if (maxVelocity < minVelocityToHurt)
                Debug.Log("not enough velocity to hurt");

            // Случай, когда наш текущий объект имеет бОльшую скорость, учитываем только его, 
            // чтобы расчеты не проводились повторно
            //Debug.Log("rigidbody.velocity:" + rigidbody.velocity);
            //Debug.Log("otherRigidbody.velocity:" + otherRigidbody.velocity);

            //Debug.Log("otherRigidbody.velocity:" + otherRigidbody.velocity);

            var damage = 0f;
            float directionCoeff;
            float otherVelocity;
            if (maxVelocity == Mathf.Abs(rigidbody.velocity.x))
            {
                otherVelocity = otherRigidbody.velocity.x;
                directionCoeff = GetDirectionCoeff(rigidbody.velocity.x, otherVelocity);
            }
            else
            {
                otherVelocity = otherRigidbody.velocity.y;
                directionCoeff = GetDirectionCoeff(rigidbody.velocity.y, otherVelocity);
            }
            damage = maxVelocity + directionCoeff * Mathf.Abs(otherVelocity);
            //Debug.Log(damage);
            playerHealth.Hurt(damage);
        }

        //var maxVelocity = Mathf.Max(Mathf.Abs(rigidbody.velocity.x), Mathf.Abs(rigidbody.velocity.y),
        //    Mathf.Abs(otherRigidbody.velocity.x), Mathf.Abs(otherRigidbody.velocity.y));
        ////TODO: подумать, как правильно обрабатывать сценарий, что если очень близко объекты, чтобы не дамажили друг друга
        //if (//rigidbody.Distance(otherRigidbody.gameObject.GetComponent<Collider2D>()).distance >= minDistanceToHurt &&
        //    (maxVelocity == Mathf.Abs(rigidbody.velocity.x) || maxVelocity == Mathf.Abs(rigidbody.velocity.y)))
        //{
        //    if (rigidbody.Distance(otherRigidbody.gameObject.GetComponent<Collider2D>()).distance >= minDistanceToHurt)
        //        ;//Debug.Log("too close to hurt");

        //    // Случай, когда наш текущий объект имеет бОльшую скорость, учитываем только его, 
        //    // чтобы расчеты не проводились повторно
        //    //Debug.Log("rigidbody.velocity:" + rigidbody.velocity);
        //    //Debug.Log("otherRigidbody.velocity:" + otherRigidbody.velocity);

        //    Debug.Log("otherRigidbody.velocity:" + otherRigidbody.velocity);

        //    var damage = 0f;
        //    if (maxVelocity == Mathf.Abs(rigidbody.velocity.x))
        //        damage = maxVelocity - Mathf.Abs(otherRigidbody.velocity.x);
        //    else
        //        damage = maxVelocity - Mathf.Abs(otherRigidbody.velocity.y);
        //    //Debug.Log(damage);
        //    playerHealth.Hurt(damage);
        //}
    }

    // Учитываем, что если игроки движутся в одну сторону, то столкновение менее критично, чем если в разные
    private float GetDirectionCoeff(float velocity1, float velocity2)
    {
        return Mathf.Sign(velocity1) == Mathf.Sign(velocity2) ? -1 : 1;
    }
}
