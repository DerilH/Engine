using System.Threading;

namespace UrsaEngine.Physics
{
    public static class PhysicsEngine
    {
        public static event BasicEvent OnPhysicsUpdate = () => {};
        private static bool _started = false;
        private static Thread? _physicsThread;
        private static CancellationTokenSource _physicsThreadCancel = new CancellationTokenSource();
        static PhysicsEngine()
        {
        }

        public static void StartPhysics()
        {
            if (_started) throw new System.Exception("Physic simulation already running");
            _started = true;

            _physicsThread = new Thread(() =>
            {
                while (!Engine.instance.IsStopping || _physicsThreadCancel.IsCancellationRequested)
                {
                    Thread.Sleep(20);
                    OnPhysicsUpdate.Invoke();
                }
            });
            _physicsThread.Start();
        }

        public static void Stop()
        {
            _physicsThreadCancel.Cancel();
        }
        private static void CalcPhysics()
        {
            
        }
    }
}