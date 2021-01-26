using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FireStationCallDispatcher
{

    public enum Seniority
    {
        [EnumMember(Value = "Junior")]
        Junior,
        [EnumMember(Value = "Senior")]
        Senior,
        [EnumMember(Value = "Manager")]
        Manager,
        [EnumMember(Value = "Director")]
        Director
    }

    public class Employee
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Seniority Seniority{ get; set; }
        public Call Call { get; private set; }

        public bool IsFree { get { return Call == null; } }

        public string Name { get; set; }
        public int Id { get; set; }

        public Employee(Seniority seniority, string name, int id)
        {
            Seniority = seniority;
            Name = name;
            Id = id;
        }

        public void AssignCall(Call call)
        {
            this.Call = call;
            Logger.InfoLog($"Call {call.CallId} with {call.CallPriority} priority has been assigned to a {Seniority} employee ({Name}).");
        }

        public void FinishCall()
        {
            Call = null;
        }


        public bool CanHandleCall(Call call)
        {
            List<Seniority> highSenorities = CallEmployeeMapper.GetCompatibleSeniorities(call.CallPriority);

            // check if, after call escalation, the employee can still handle this call
            if (!highSenorities.Contains(Seniority))
            {
                Logger.InfoLog($"Employee assigned to call {call.CallId} is not senior enough to handle the call after priority escalation. The call will be returned to the queue for re-processing.");
                return false;
            }
            return true;
        }
    }
}
