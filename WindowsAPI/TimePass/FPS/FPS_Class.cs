using System.Threading;

namespace WindowsAPI.TimePass.FPS
{

    /// <summary>
    /// 計算FPS
    /// </summary>
    public class FPS_Class : TimePass_Class
    {
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="limit">限制frame數</param>
        public FPS_Class(uint limit = uint.MaxValue)
        {
            if (limit == 0)
            {
                return;
            }

            FpsLimit = limit;

            FpsWorkTime = 1000 / FpsLimit;
        }

        /// <summary>
        /// 一秒=1000ms
        /// </summary>
        private const uint OneSecond = 1000;
        /// <summary>
        /// 設定FPS限制
        /// </summary>
        private uint FpsLimit = 0;
        /// <summary>
        /// WorkTime
        /// </summary>
        private uint FpsWorkTime = 0;

        /// <summary>
        /// 計算frames
        /// </summary>
        private uint fpsCount = 0;
        /// <summary>
        /// 紀錄FPS值
        /// </summary>
        private uint fpsRecord = 0;
        /// <summary>
        /// 供外部讀取FPS值
        /// </summary>
        public uint Fps
        {
            get
            {
                return fpsRecord;
            }
        }

        /// <summary>
        /// FPS值是否更新
        /// </summary>
        public bool bUpdateFps
        {
            private set;
            get;
        }

        /// <summary>
        /// 工作迴圈
        /// </summary>
        new public void Work()
        {
            base.Work();

            if (base.PassTime < FpsWorkTime)
            {

                uint sleepTime = FpsWorkTime - base.PassTime;
                int s = (int)sleepTime;
                Thread.Sleep(s);

                //不加base.Work()會不準
                base.Work();
            }

            fpsCount++;

            if (TotalPassTime >= OneSecond)
            {
                fpsRecord = fpsCount;
                fpsCount = 0;
                TotalPassTime -= OneSecond;

                if (!bUpdateFps)
                {
                    bUpdateFps = true;
                    return;
                }
            }

            if (bUpdateFps)
            {
                bUpdateFps = false;
            }
        }
    }

}
