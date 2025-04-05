namespace Game.Scripts.Tech
{
    public static class MoveAPI
    {
        public const string MOVE_DIRECTION = "MoveDirection";
        public const string FORWARD_DIRECTION = "ForwardDirection";

    }
    
    public static class LifeAPI
    {
        public const string TAKE_DAMAGE_ACTION = "TakeDamage";
        public const string IS_DEAD = "IsDead";
        public const string DIE_ACTION = "DieEvent";
        public const string DIE_EVENT = "DieEventAnimation";
        public const string HEALTH = "Health";
    }
    
    public static class ShootAPI
    {
        public const string SHOOT_REQUEST = "ShootRequest";
    }

    public static class EnemyAPI
    {
        public const string TARGET = "Target";
    }

    public static class PhysicsAPI
    {
        public const string COLLIDE_EVENT = "CollideEvent";
    }

    public static class TechAPI
    {
        public const string RESET_ACTION = "Reset";
    }
}