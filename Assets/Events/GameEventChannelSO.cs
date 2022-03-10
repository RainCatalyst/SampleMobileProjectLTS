using UnityEngine;
using UnityEngine.Events;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "Events/Game Event Channel")]
    public class GameEventChannelSO : ScriptableObject
    {
        public UnityAction OnLevelStarted;
        public UnityAction<bool> OnLevelFinished;
        public UnityAction<Waypoint> OnWaypointSet;
        public UnityAction<Waypoint> OnWaypointCompleted;
        public UnityAction<Waypoint> OnWaypointEntered;
        public UnityAction<Waypoint> OnWaypointExited;

        public void StartLevel() => OnLevelStarted?.Invoke();
        public void FinishLevel(bool won) => OnLevelFinished?.Invoke(won);
        public void CompleteWaypoint(Waypoint waypoint) => OnWaypointCompleted?.Invoke(waypoint);
        public void SetWaypoint(Waypoint waypoint) => OnWaypointSet?.Invoke(waypoint);
        public void EnterWaypoint(Waypoint waypoint) => OnWaypointEntered?.Invoke(waypoint);
        public void ExitWaypoint(Waypoint waypoint) => OnWaypointExited?.Invoke(waypoint);
    }
}