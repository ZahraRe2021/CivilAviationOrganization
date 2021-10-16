using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    [Serializable]
    public class FullLaserPen
    {
        public string objectName { get; set; }
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

        public int nAlign { get; set; }
        public int ndpi { get; set; }//ok
        public int nminLowGrayPt { get; set; }//ok
        public double dBrightness { get; set; }//ok
        public double dContrast { get; set; }//ok
        public double dSettingPointTime { get; set; }//ok
        public double dRatio { get; set; }
        public int nbmpScanAttr { get; set; }
        public int nbmpttrib { get; set; }
        public bool bHatchFile { get; set; }


        public bool blInvert { get; set; }//ok
        public bool blGray { get; set; }//ok
        public bool blDither { get; set; }//ok
        public bool blBidirectional { get; set; }//ok
        public bool blYscan { get; set; }//ok
        public bool blDisableMarkLowGray { get; set; }//ok
        public bool blPower { get; set; }//ok
        public bool blOffSetPT { get; set; }//ok
        public bool blOptimize { get; set; }//ok
        public bool blDynamic { get; set; }//ok
        public bool blDPIfixedWidth { get; set; }//ok
        public bool blDPIfixedHeight { get; set; }//ok
        public bool blBrightness { get; set; }//ok
        public bool blDrill { get; set; }
        public bool blfixDPI { get; set; }//ok



        public FullLaserPen()
        {

        }
        public FullLaserPen(int _nPenNo, int _nMarkLoop, double _dMarkSpeed, double _dPowerRatio, double _dCurrent, int _nFreq, double _dQPulseWidth,
                    int _nStartTC, int _nLaserOffTC, int _nEndTC, int _nPolyTC, double _dJumpSpeed, int _nJumpPosTC,
                     int _nJumpDistTC, double _dEndComp, double _dAccDist, double _dPointTime, bool _bPulsePointMode, int _nPulseNum, double _dFlySpeed,
            int _nAlign, int _ndpi, int _nminLowGrayPt, int _nbmpScanAttr, int _nbmpttrib, double _dBrightness, double _dContrast, double _dSettingPointTime, double _dRatio, bool _blInvert, bool _blGray, bool _blDither, bool _blBidirectional, bool _blYscan, bool _blDisableMarkLowGray, bool _blPower, bool _blOffSetPT, bool _blOptimize, bool _blDPIfixedWidth, bool _blDPIfixedHeight, bool _blBrightness, bool _blDrill, bool _blfixDPI, bool _blDynamic,bool _bHatchFile)
        {
            nPenNo = _nPenNo;
            nMarkLoop = _nMarkLoop;
            dMarkSpeed = _dMarkSpeed;
            dPowerRatio = _dPowerRatio;
            dCurrent = _dCurrent;
            nFreq = _nFreq;
            dQPulseWidth = _dQPulseWidth;
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

            ndpi = _ndpi;
            nAlign = _nAlign;
            nminLowGrayPt = _nminLowGrayPt;
            nbmpScanAttr = _nbmpScanAttr;
            nbmpttrib = _nbmpttrib;
            dBrightness = _dBrightness;
            dContrast = _dContrast;
            dRatio = _dRatio;
            dSettingPointTime = _dSettingPointTime;
            bHatchFile = _bHatchFile;



            blInvert = _blInvert;
            blGray = _blGray;
            blDither = _blDither;
            blBidirectional = _blBidirectional;
            blYscan = _blYscan;
            blDisableMarkLowGray = _blDisableMarkLowGray;
            blPower = _blPower;
            blOffSetPT = _blOffSetPT;
            blOptimize = _blOptimize;
            blDynamic = _blDynamic;
            blDPIfixedWidth = _blDPIfixedWidth;
            blDPIfixedHeight = _blDPIfixedHeight;
            blBrightness = _blBrightness;
            blDrill = _blDrill;
            blfixDPI = _blfixDPI;



    }
}
}
