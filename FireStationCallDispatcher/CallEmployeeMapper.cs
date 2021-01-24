using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{
    public class CallEmployeeMapper
    {
        protected Dictionary<PriorityLevel, List<Seniority>> mapping;
        public CallEmployeeMapper()
        {
            mapping = new Dictionary<PriorityLevel, List<Seniority>>();
            mapping.Add(PriorityLevel.Low, new List<Seniority> { Seniority.Junior, Seniority.Senior, Seniority.Manager });
            mapping.Add(PriorityLevel.High, new List<Seniority> { Seniority.Manager, Seniority.Director });
        }

        public List<Seniority> GetCompatibleSeniorities(PriorityLevel priorityLevel)
        {
            mapping.TryGetValue(priorityLevel, out List<Seniority> seniority);
            return seniority;
        }
    }
}
