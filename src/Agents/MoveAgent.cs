using System.Collections.Concurrent;
using TrafficSimParallel.Types;
using TrafficSimParallel.Models;

namespace TrafficSimParallel.Methods
{
    public class AgentHandler
    {
        public static void MoveAgentLogic(Agent agent, int MapWidth, ConcurrentDictionary<Position, TrafficLight> _trafficLights, ConcurrentDictionary<Position, int> _globalGrid)
        {
            Thread.Sleep(1);

            int nextX = (agent.CurrentPos.X + 1) % MapWidth;
            Position proposedPos = new Position(nextX, agent.CurrentPos.Y);

            if (_trafficLights.TryGetValue(proposedPos, out var light))
            {
                if (light.GetState() == LightState.Red)
                {
                    return;
                }
            }

            if (_globalGrid.TryAdd(proposedPos, agent.Id))
            {
                Position oldPos = agent.CurrentPos;
                _globalGrid.TryRemove(oldPos, out _);
                agent.CurrentPos = proposedPos;
            }
        }
    }
}
