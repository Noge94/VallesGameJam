using UnityEngine;

namespace Enemies
{
    public class MovingUfoController : UfoController
    {
        private float speed = 3f;
        private int direction = 1;
        
        protected override void Update()
        {
            Move();
            base.Update();
        }

        private void Move()
        {
            SetXPosition(transform.position.x+speed*direction*Time.deltaTime);
            if (Mathf.Abs(transform.position.x) > Configuration.SCREEN_LIMIT-0.5f)
            {
                direction *= -1;
            }
        }
        
        
        private void SetXPosition(float xPosition)
        {
            Vector3 position = transform.position;
            position.x = xPosition;
            transform.position = position;
        }
    }
}