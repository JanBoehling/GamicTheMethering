internal interface IEnemy
{
    public int Health { get; set; }

    public void TakeDamage(float damage);
}