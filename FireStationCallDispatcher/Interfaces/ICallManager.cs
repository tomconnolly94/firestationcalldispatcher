namespace FireStationCallDispatcher.Interfaces
{
    public interface ICallManager
    {
        int GetCompletedCallCount();
        bool HasUnhandledCalls();
        Call GetNextCall();
        void ReAddCall(Call call);
    }
}
