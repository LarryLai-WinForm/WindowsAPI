namespace WindowsAPI.TimePass
{
    using static dlls.winmm;

    /// <summary>
    /// 計時用
    /// </summary>
    public class TimePass_Class
    {

        /// <summary>
        /// 建構式
        /// </summary>
        public TimePass_Class()
        {
            Reset();
        }


        /// <summary>
        /// 記錄上次時間
        /// </summary>
        public uint LastTime
        {
            private set;
            get;
        }

        /// <summary>
        /// 經過時間
        /// </summary>
        public uint PassTime
        {
            private set;
            get;
        }

        /// <summary>
        /// 累積經過時間
        /// </summary>
        public uint TotalPassTime
        {
            set;
            get;
        }


        /// <summary>
        /// 工作迴圈
        /// </summary>
        public void Work()
        {
            uint CurrentTime = GetCurrentTime();

            if (LastTime != 0)
            {
                if (CurrentTime < LastTime)
                {
                    PassTime = (uint.MaxValue - LastTime + CurrentTime);
                }
                else
                {
                    PassTime = (CurrentTime - LastTime);
                }

                TotalPassTime += PassTime;
            }

            LastTime = CurrentTime;
        }

        /// <summary>
        /// 取得開機至今經過的ms
        /// </summary>
        /// <returns>ms</returns>
        private uint GetCurrentTime()
        {
            uint time;

            //GetTickCount效能快一點點,約快1/500ms
            //但GetTickCount會有10~35ms時間跳躍,timeGetTime為1ms
            //先求準以掌握效能

            timeBeginPeriod(1);
            time = timeGetTime();
            //WindowsApi.timeEndPeriod(15);

            //time = WindowsApi.GetTickCount();

            return time;
        }

        /// <summary>
        /// 重置計時
        /// </summary>
        public void Reset()
        {
            LastTime = GetCurrentTime();
            PassTime = 0;
            TotalPassTime = 0;
        }

    }

}
