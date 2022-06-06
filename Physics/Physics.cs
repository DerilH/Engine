using System.Threading;

namespace UrsaEngine.Physics
{
    public static class PhysicsEngine
    {
        public delegate void OnPhysicsUpdateDelegate();
        public static event OnPhysicsUpdateDelegate OnPhysicsUpdate;
        private static bool _started = false;
        private static Thread? _physicsThread;
        private static CancellationTokenSource _physicsThreadCancel = new CancellationTokenSource();
        static PhysicsEngine()
        {
            OnPhysicsUpdate += () => { };
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