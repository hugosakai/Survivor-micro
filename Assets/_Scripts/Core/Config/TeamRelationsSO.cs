//_Scripts/Core/Config/TeamRelationsSO.cs
using UnityEngine;

[CreateAssetMenu(fileName = "teamRelations", menuName = "Config/Team Relations")]
public class TeamRelationsSO : ScriptableObject, ITeamRelations
{
    public bool IsFriendly(int a, int b) => a == b;
    public bool IsHostile(int a, int b) => a != b;
    
    public TeamRelation RelationOf(int a, int b)
    {
        if (a == b)
        {
            return TeamRelation.Friendly;
        }
        return TeamRelation.Hostile;
    }
}
