// _Scripts/Core/Data/DamageSource.cs
public enum DamageSource
{
    Player = 0,
    Enemy = 1,
    Environment = 2,  // ex.: armadilhas, fogo no cenário
    Neutral = 3,    // ex.: dano compartilhado ou sistemas que não pertencem a ninguém
}