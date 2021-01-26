using System.Collections.Generic;

namespace FireStationCallDispatcher
{
    public static class CallEmployeeMapper
    {
        private static readonly Dictionary<PriorityLevel, List<Seniority>> mapping = new Dictionary<PriorityLevel, List<Seniority>>
        {
            { PriorityLevel.Low, new List<Seniority> { Seniority.Junior, Seniority.Senior, Seniority.Manager } },
            { PriorityLevel.High, new List<Seniority> { Seniority.Manager, Seniority.Director } }
        };

        public static List<Seniority> GetCompatibleSeniorities(PriorityLevel priorityLevel)
        {
            mapping.TryGetValue(priorityLevel, out List<Seniority> seniority);
            return seniority;
        }
    }
}
