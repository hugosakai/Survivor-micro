public enum TeamRelation { Friendly, Hostile, Neutral }
public interface ITeamRelations
{
    // Ainda sem uso para o estado "Neutral"
    TeamRelation RelationOf(int a, int b);
    bool IsFriendly(int a, int b);
    bool IsHostile(int a, int b);
}
