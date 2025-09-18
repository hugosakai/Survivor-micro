//_Scripts/Gameplay/Combat/TeamMarker.cs
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TeamMarker : MonoBehaviour, ITeamProvider
{
    [SerializeField] private int _teamId;
    public int TeamId => _teamId;
}
