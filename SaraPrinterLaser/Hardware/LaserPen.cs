using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.Hardware
{
    [Serializable]
    public class LaserPen
    {
        public int nPenNo { get; set; }
        public int nMarkLoop { get; set; }
        public double dMarkSpeed { get; set; }
        public double dPowerRatio { get; set; }
        public double dCurrent { get; set; }
        public int nFreq { get; set; }
        public double dQPulseWidth { get; set; }
        public int nStartTC { get; set; }
        public int nLaserOffTC { get; set; }
        public int nEndTC { get; set; }
        public int nPolyTC { get; set; }
        public double dJumpSpeed { get; set; }
        public int nJumpPosTC { get; set; }
        public int nJumpDistTC { get; set; }
        public double dEndComp { get; set; }
        public double dAccDist { get; set; }
        public double dPointTime { get; set; }
        public bool bPulsePointMode { get; set; }
        public int nPulseNum { get; set; }
        public double dFlySpeed { get; set; }
        public LaserPen()
        {

        }
        public LaserPen(int _nPenNo, int _nMarkLoop, double _dMarkSpeed, double _dPowerRatio, double _dCurrent, int _nFreq, double _dQPulseWidth,
                    int _nStartTC, int _nLaserOffTC, int _nEndTC, int _nPolyTC, double _dJumpSpeed, int _nJumpPosTC,
                     int _nJumpDistTC, double _dEndComp, double _dAccDist, double _dPointTime, bool _bPulsePointMode, int _nPulseNum, double _dFlySpeed)
        {
            nPenNo = _nPenNo;
            nMarkLoop = _nMarkLoop;
            dMarkSpeed = _dMarkSpeed;
            dPowerRatio = _dPowerRatio;
            dCurrent = _dCurrent;
            nFreq = _nFreq;
            nStartTC = _nStartTC;
            nLaserOffTC = _nLaserOffTC;
            nEndTC = _nEndTC;
            nPolyTC = _nPolyTC;
            dJumpSpeed = _dJumpSpeed;
            nJumpPosTC = _nJumpPosTC;
            nJumpDistTC = _nJumpDistTC;
            dEndComp = _dEndComp;
            dAccDist = _dAccDist;
            dPointTime = _dPointTime;
            bPulsePointMode = _bPulsePointMode;
            nPulseNum = _nPulseNum;
            dFlySpeed = _dFlySpeed;
            dQPulseWidth = _dQPulseWidth;
        }
    }
}
