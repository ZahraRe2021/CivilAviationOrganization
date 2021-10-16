using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace SaraPrinterLaser.Hardware
{
    public static class Laser
    {
        public static LaserConfigClass myconfig;
        
        public const int BMPSCAN_INVERT = 0x0001;//bitmap scan inverse
        public const int BMPSCAN_GRAY = 0x0002;//bitmap gray
        public const int BMPSCAN_LIGHT = 0x0004;//bitmap brightness
        public const int BMPSCAN_DITHER = 0x0010;//bitmap network node pr
        public const int BMPSCAN_BIDIR = 0x1000;// bidirectional Scan
        public const int BMPSCAN_YDIR = 0x2000;// YScan
        public const int BMPSCAN_DRILL = 0x4000;//marking point model
        public const int BMPSCAN_POWER = 0x8000;//adjust power
        public const int BMPSCAN_OFFSETPT = 0x0100;// Interlaced dislocation
        public const int BMPSCAN_OPTIMIZE = 0x0200;//optimize model qy 20

        public const int BMPATTRIB_DYNFILE = 0x1000;//dynamic file
        public const int BMPATTRIB_IMPORTFIXED_WIDTH = 0x2000;////the width of the import fixed file
        public const int BMPATTRIB_IMPORTFIXED_HEIGHT = 0x4000;// the height of the import fixed file
        public const int BMPATTRIB_IMPORTFIXED_DPI = 0x8000;//fixed DPI


        internal static StatusClass GetLaserBoardDescription(LmcErrCode ResponseStatus)
        {
            StatusClass ReturnStatus = new StatusClass()
            {
                ResponseReturnStatus = StatusClass.ResponseStatus.Fail,
                ReturnDescription = ""
            };
            if (ResponseStatus == LmcErrCode.LMC1_ERR_SUCCESS)
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Ok;
                ReturnStatus.ReturnDescription = StatusClass.Message_SucsessOpration;
            }
            else
            {
                switch (ResponseStatus)
                {
                    case LmcErrCode.LMC1_ERR_EZCADRUN:
                        ReturnStatus.ReturnDescription = StatusClass.Error_ApplicationDuplicateRun;
                        break;
                    case LmcErrCode.LMC1_ERR_NOFINDCFGFILE:
                        ReturnStatus.ReturnDescription = StatusClass.Error_CFGFileNotFound;
                        break;
                    case LmcErrCode.LMC1_ERR_FAILEDOPEN:
                        ReturnStatus.ReturnDescription = StatusClass.Error_FailedOpen;
                        break;
                    case LmcErrCode.LMC1_ERR_NODEVICE:
                        ReturnStatus.ReturnDescription = StatusClass.Error_NoDevice;
                        break;
                    case LmcErrCode.LMC1_ERR_HARDVER:
                        ReturnStatus.ReturnDescription = StatusClass.Error_Hardver;
                        break;
                    case LmcErrCode.LMC1_ERR_DEVCFG:
                        ReturnStatus.ReturnDescription = StatusClass.Error_DevCFG;
                        break;
                    case LmcErrCode.LMC1_ERR_STOPSIGNAL:
                        ReturnStatus.ReturnDescription = StatusClass.Error_StopSignal;
                        break;
                    case LmcErrCode.LMC1_ERR_USERSTOP:
                        ReturnStatus.ReturnDescription = StatusClass.Error_UserStop;
                        break;
                    case LmcErrCode.LMC1_ERR_UNKNOW:
                        ReturnStatus.ReturnDescription = StatusClass.Error_Unknown;
                        break;
                    case LmcErrCode.LMC1_ERR_OUTTIME:
                        ReturnStatus.ReturnDescription = StatusClass.Error_OUTTIME;
                        break;
                    case LmcErrCode.LMC1_ERR_NOINITIAL:
                        ReturnStatus.ReturnDescription = StatusClass.Error_NoInit;
                        break;
                    case LmcErrCode.LMC1_ERR_READFILE:
                        ReturnStatus.ReturnDescription = StatusClass.Error_ReadFile;
                        break;
                    case LmcErrCode.LMC1_ERR_OWENWNDNULL:
                        ReturnStatus.ReturnDescription = StatusClass.Error_OWENWNDNULL;
                        break;
                    case LmcErrCode.LMC1_ERR_NOFINDFONT:
                        ReturnStatus.ReturnDescription = StatusClass.Error_NOFINDFONT;
                        break;
                    case LmcErrCode.LMC1_ERR_PENNO:
                        ReturnStatus.ReturnDescription = StatusClass.Error_PENNO;
                        break;
                    case LmcErrCode.LMC1_ERR_NOTTEXT:
                        ReturnStatus.ReturnDescription = StatusClass.Error_NOTTEXT;
                        break;
                    case LmcErrCode.LMC1_ERR_SAVEFILE:
                        ReturnStatus.ReturnDescription = StatusClass.Error_SAVEFILE;
                        break;
                    case LmcErrCode.LMC1_ERR_NOFINDENT:
                        ReturnStatus.ReturnDescription = StatusClass.Error_NOFINDENT;
                        break;
                    case LmcErrCode.LMC1_ERR_STATUE:
                        ReturnStatus.ReturnDescription = StatusClass.Error_STATUE;
                        break;
                    case LmcErrCode.LMC1_ERR_PARAM:
                        ReturnStatus.ReturnDescription = StatusClass.Error_PARAM;
                        break;
                    case LmcErrCode.LMC1_ERR_INITFAIL:
                        ReturnStatus.ReturnDescription = StatusClass.Error_InitFailed;
                        break;
                    default:
                        ReturnStatus.ReturnDescription = StatusClass.Error_Unknown;
                        break;
                }
            }
            return ReturnStatus;

        }

        public static StatusClass DeviceInitialize(string StartupDirectory)
        {
            IntPtr mes = new IntPtr();
            LmcErrCode ResponseStatus = Initialize(StartupDirectory, false, mes);
            return GetLaserBoardDescription(ResponseStatus);
        }

        public enum LmcErrCode
        {
            LMC1_ERR_SUCCESS = 0,
            LMC1_ERR_EZCADRUN = 1,
            LMC1_ERR_NOFINDCFGFILE = 2,
            LMC1_ERR_FAILEDOPEN = 3,
            LMC1_ERR_NODEVICE = 4,
            LMC1_ERR_HARDVER = 5,
            LMC1_ERR_DEVCFG = 6,
            LMC1_ERR_STOPSIGNAL = 7,
            LMC1_ERR_USERSTOP = 8,
            LMC1_ERR_UNKNOW = 9,
            LMC1_ERR_OUTTIME = 10,
            LMC1_ERR_NOINITIAL = 11,
            LMC1_ERR_READFILE = 12,
            LMC1_ERR_OWENWNDNULL = 13,
            LMC1_ERR_NOFINDFONT = 14,
            LMC1_ERR_PENNO = 15,
            LMC1_ERR_NOTTEXT = 16,
            LMC1_ERR_SAVEFILE = 17,
            LMC1_ERR_NOFINDENT = 18,
            LMC1_ERR_STATUE = 19,
            LMC1_ERR_PARAM = 20,
            LMC1_ERR_INITFAIL = 21
        }
        public enum BARCODETYPE
        {
            BARCODETYPE_39 = 0,
            BARCODETYPE_93 = 1,
            BARCODETYPE_128A = 2,
            BARCODETYPE_128B = 3,
            BARCODETYPE_128C = 4,
            BARCODETYPE_128OPT = 5,
            BARCODETYPE_EAN128A = 6,
            BARCODETYPE_EAN128B = 7,
            BARCODETYPE_EAN128C = 8,
            BARCODETYPE_EAN13 = 9,
            BARCODETYPE_EAN8 = 10,
            BARCODETYPE_UPCA = 11,
            BARCODETYPE_UPCE = 12,
            BARCODETYPE_25 = 13,
            BARCODETYPE_INTER25 = 14,
            BARCODETYPE_CODABAR = 15,
            BARCODETYPE_PDF417 = 16,
            BARCODETYPE_DATAMTX = 17,
            BARCODETYPE_USERDEF = 18,
            BARCODETYPE_QRCODE = 19,
            BARCODETYPE_MICROQRCODE = 20

        };
        public enum DATAMTX_SIZEMODE
        {
            DATAMTX_SIZEMODE_SMALLEST = 0,
            DATAMTX_SIZEMODE_10X10 = 1,
            DATAMTX_SIZEMODE_12X12 = 2,
            DATAMTX_SIZEMODE_14X14 = 3,
            DATAMTX_SIZEMODE_16X16 = 4,
            DATAMTX_SIZEMODE_18X18 = 5,
            DATAMTX_SIZEMODE_20X20 = 6,
            DATAMTX_SIZEMODE_22X22 = 7,
            DATAMTX_SIZEMODE_24X24 = 8,
            DATAMTX_SIZEMODE_26X26 = 9,
            DATAMTX_SIZEMODE_32X32 = 10,
            DATAMTX_SIZEMODE_36X36 = 11,
            DATAMTX_SIZEMODE_40X40 = 12,
            DATAMTX_SIZEMODE_44X44 = 13,
            DATAMTX_SIZEMODE_48X48 = 14,
            DATAMTX_SIZEMODE_52X52 = 15,
            DATAMTX_SIZEMODE_64X64 = 16,
            DATAMTX_SIZEMODE_72X72 = 17,
            DATAMTX_SIZEMODE_80X80 = 18,
            DATAMTX_SIZEMODE_88X88 = 19,
            DATAMTX_SIZEMODE_96X96 = 20,
            DATAMTX_SIZEMODE_104X104 = 21,
            DATAMTX_SIZEMODE_120X120 = 22,
            DATAMTX_SIZEMODE_132X132 = 23,
            DATAMTX_SIZEMODE_144X144 = 24,
            DATAMTX_SIZEMODE_8X18 = 25,
            DATAMTX_SIZEMODE_8X32 = 26,
            DATAMTX_SIZEMODE_12X26 = 27,
            DATAMTX_SIZEMODE_12X36 = 28,
            DATAMTX_SIZEMODE_16X36 = 29,
            DATAMTX_SIZEMODE_16X48 = 30,
        }

        /// <summary>
        /// 1
        /// Initialize the dll
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_Initial", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode Initialize(string PathName, bool bTestMode, IntPtr window);
        /// <summary>
        /// 2
        /// Cloase the dll
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_Close", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseWindow();
        /// <summary>
        /// 3 
        /// Load ezd file and clear old database;
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_LoadEzdFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode LoadEzdFile(string FileName);
        /// <summary>
        ///4 Mark database one time bool Fly means fly marking
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_Mark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Mark(bool Fly);
        /// <summary>
        ///5
        /// Change the text by entity name
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_ChangeTextByName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode ChangeTextByName(string EntName, string NewText);

        /// <summary>
        /// 6
        /// Mark special entity one time
        /// EntName is the name of entity
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkEntity", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode MarkEntity(string EntName);
        /// <summary>
        /// 7
        /// Read current input port
        /// data is input value
        /// Bit0 is In 0, Bit0=1 means In0 is high level,Bit0=0 means In0 is low level.
        ///  Bit1 is In 1, Bit1=1 means In1 is high level,Bit1=0 means In1 is low level.
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_ReadPort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode ReadPort(ref ushort data);
        /// <summary>
        /// 8
        /// Set current output port
        /// data is output value
        /// Bit0 is Out 0, Bit0=1 means Out0 is high level,Bit0=0 means Out0 is low level.
        ///  Bit1 is Out 1, Bit1=1 means Out1 is high level,Bit1=0 means Out1 is low level.
        /// </summary>  
        [DllImport("MarkEzd", EntryPoint = "lmc1_WritePort", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode WritePort(ushort data);
        /// <summary>
        /// 9
        /// Set hatch param, when add entity with enable hatch, will use these param
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetHatchParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode SetHatchParam(bool bEnableContour,
                          int bEnableHatch1,
                          int nPenNo1,
                          int nHatchAttrib1,
                          double dHatchEdgeDist1,
                          double dHatchLineDist1,
                          double dHatchStartOffset1,
                          double dHatchEndOffset1,
                          double dHatchAngle1,
                          int bEnableHatch2,
                          int nPenNo2,
                          int nHatchAttrib2,
                          double dHatchEdgeDist2,
                          double dHatchLineDist2,
                          double dHatchStartOffset2,
                          double dHatchEndOffset2,
                          double dHatchAngle2);

        /// <summary>
        /// 10
        /// Set current font param, when add text to the library will use these param
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetFontParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode SetFontParam(string strFontName, double dCharHeight, double dCharWidth, double dCharAngle, double dCharSpace, double dLineSpace, bool bEqualCharWidth);
        /// <summary>
        /// 11
        /// nPenNo 0-255
        /// nMarkLoop marking time 
        /// dMarkSpeed  mm/s or inch/mm
        /// dPowerRatio power 0-100%
        /// dCurrent current A
        /// nFreq frequency Hz
        /// nQPulseWidth Qpulse width us
        /// nStartTC  us
        /// nLaserOffTC  us
        /// nEndTC   us
        /// nPolyTC us
        /// dJumpSpeed  mm/s or inch/mm 
        /// nJumpPosTC  us
        /// nJumpDistTC  us
        /// dEndComp mm or inch 
        /// dAccDist  mm or inch
        /// dPointTime ms
        /// bPulsePointMode  true is enable
        /// nPulseNum 
        /// dFlySpeed 
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode GetPenParam(int nPenNo,
                     ref int nMarkLoop,
                     ref double dMarkSpeed,
                     ref double dPowerRatio,
                     ref double dCurrent,
                     ref int nFreq,
                     ref double dQPulseWidth,
                     ref int nStartTC,
                     ref int nLaserOffTC,
                     ref int nEndTC,
                     ref int nPolyTC,
                     ref double dJumpSpeed,
                     ref int nJumpPosTC,
                     ref int nJumpDistTC,
                     ref double dEndComp,
                     ref double dAccDist,
                     ref double dPointTime,
                     ref bool bPulsePointMode,
                     ref int nPulseNum,
                     ref double dFlySpeed);

        /// <summary>
        /// 12
        /// Set pen parameter
        /// nPenNo 0-255
        /// nMarkLoop marking time 
        /// dMarkSpeed  mm/s or inch/mm
        /// dPowerRatio power 0-100%
        /// dCurrent current A
        /// nFreq frequency Hz
        /// nQPulseWidth Qpulse width us
        /// nStartTC  us
        /// nLaserOffTC  us
        /// nEndTC   us
        /// nPolyTC us
        /// dJumpSpeed  mm/s or inch/mm 
        /// nJumpPosTC  us
        /// nJumpDistTC  us
        /// dEndComp mm or inch 
        /// dAccDist  mm or inch
        /// dPointTime ms
        /// bPulsePointMode  true is enable
        /// nPulseNum 
        /// dFlySpeed 
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetPenParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode SetPenParam(int nPenNo,
                             int nMarkLoop,
                             double dMarkSpeed,
                             double dPowerRatio,
                             double dCurrent,
                             int nFreq,
                             double dQPulseWidth,
                             int nStartTC,
                             int nLaserOffTC,
                             int nEndTC,
                             int nPolyTC,
                             double dJumpSpeed,
                             int nJumpPosTC,
                             int nJumpDistTC,
                             double dEndComp,
                             double dAccDist,
                             double dPointTime,
                             bool bPulsePointMode,
                             int nPulseNum,
                             double dFlySpeed);


        /// <summary>
        ///13
        /// Clear all entity in library
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_ClearEntLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode ClearLibAllEntity();

        /// <summary>
        /// 14
        /// Add text to library
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddTextToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode AddTextToLib(string text, string EntName, double dPosX, double dPosY, double dPosZ, int nAlign, double dTextRotateAngle, int nPenNo, int bHatchText);

        /// <summary>
        ///15
        /// Add a curve entity
        /// Notice PtBuf must be 2 dimension arry, for example double[5,2],double[n,2]
        /// ptNum is the seconde dimension of PtBuf, for example PtBuf is double [5,2],ptNum=5
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddCurveToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode AddCurveToLib([MarshalAs(UnmanagedType.LPArray)] double[,] PtBuf, int ptNum, string strEntName, int nPenNo, int bHatch);

        /// <summary>
        /// 16
        /// Add File to library
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AddFileToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode AddFileToLib(string strFileName, string strEntName, double dPosX, double dPosY, double dPosZ, int nAlign, double dRatio, int nPenNo, bool bHatchFile);

        /// <summary>
        ///17
        /// Add barcode to the library
        /// Notice that double[] dBarWidthScale and dSpaceWidthScale must be 4 bit
        /// <summary> 

        [DllImport("MarkEzd", EntryPoint = "lmc1_AddBarCodeToLib", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode AddBarCodeToLib(string strText,
            string strEntName,
            double dPosX,
            double dPosY,
            double dPosZ,
            int nAlign,
            int nPenNo,
            int bHatchText,
            BARCODETYPE nBarcodeType,
            ushort wBarCodeAttrib,
            double dHeight,
            double dNarrowWidth,
            [MarshalAs(UnmanagedType.LPArray)] double[] dBarWidthScale,
            [MarshalAs(UnmanagedType.LPArray)] double[] dSpaceWidthScale,
              double dMidCharSpaceScale,
            double dQuietLeftScale,
            double dQuietMidScale,
            double dQuietRightScale,
            double dQuietTopScale,
            double dQuietBottomScale,
            int nRow,
            int nCol,
            int nCheckLevel,
           DATAMTX_SIZEMODE nSizeMode,
            double dTextHeight,
            double dTextWidth,
            double dTextOffsetX,
            double dTextOffsetY,
            double dTextSpace,
            double dDiameter,
            string TextFontName);

        /// <summary>
        /// 18
        /// rotate all the entity in the library
        /// dCenx rotate center x coordinate
        /// dCeny rotate center u coordinate
        /// dAngel rotate angle, unit is radians(absolute value)
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetRotateParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRotateParam(double dCenx, double dCeny, double dAngle);

        /// <summary>
        ///19
        /// axis move to goal position
        /// GoalPos goal postion, mm or inch
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AxisMoveTo", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode AxisMoveTo(int axis, double GoalPos);

        /// <summary>
        /// 20
        /// axis go home
        /// axis = 0/1
        /// GoalPos goal postion
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_AxisCorrectOrigin", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode AxisGoHome(int axis);

        /// <summary>
        /// 21
        /// Get current coordinate of axis
        /// axis = 0 or axis =1
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetAxisCoor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern double GetAxisCoor(int axis);

        /// <summary>
        /// 22
        /// Reset and enable axis
        /// ***Before use axis function, must call this function first to initialize axis***
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_Reset", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode ResetAxis(bool bEnAxis1, bool bEnAxis2);
        public struct FontRecord
        {
            public string fontname;
            public uint fontattrib;
        }

        /// <summary>
        /// 23
        /// Get all the font name and attribute
        /// </summary> 
        public static bool GetAllFontRecord(ref FontRecord[] fonts)
        {
            int fontnum = 0;
            if (GetFontRecordCount(ref fontnum) != LmcErrCode.LMC1_ERR_SUCCESS)
            {
                return false;
            }
            if (fontnum == 0)
            {
                return true;
            }

            fonts = new FontRecord[fontnum];
            StringBuilder str = new StringBuilder("", 255);
            uint fontAttrib = 0;
            for (int i = 0; i < fontnum; i++)
            {
                GetFontRecordByIndex(i, str, ref fontAttrib);
                fonts[i].fontname = str.ToString(); ;
                fonts[i].fontattrib = fontAttrib;
            }
            return true;
        }
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetFontRecordCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode GetFontRecordCount(ref int fontCount);


        /// <summary>
        /// 24
        ///Save current data to specail file
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_SaveEntLibToFile", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode SaveEntLibToFile(string strFileName);


        /// <summary>
        /// 25
        /// Get specail entity's size
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntSize", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode GetEntSize(string strEntName, ref double dMinx, ref double dMiny, ref double dMaxx, ref double dMaxy, ref double dz);

        /// <summary>
        /// 26
        /// Move special name entity
        /// dMoveX x direction distance
        /// dMoveY y direction distance
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_MoveEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode MoveEnt(string strEntName, double dMovex, double dMovey);

        /// <summary>
        /// 27
        /// Red preveiw one time
        /// </summary>   
        [DllImport("MarkEzd", EntryPoint = "lmc1_RedLightMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode RedMark();

        /// <summary>
        /// 28
        /// Mark point
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Delay"></param>
        /// <param name="Pen"></param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_MarkPoint", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode MarkPoint(double X, double Y, double Delay, int Pen);

        /// <summary>
        /// 29
        /// Get current coordinate of galvo
        /// </summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetCurCoor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode GetCurCoor(ref double x, ref double y);

        /// <summary>
        ///30
        /// Get entity total number
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntityCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern ushort GetEntityCount();

        /// <summary>
        /// 31
        /// Get specail index object's name
        /// </summary>  
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetEntityName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern LmcErrCode Lmc1_GetEntityNameByIndex(int nEntityIndex, StringBuilder entname);

        /// <summary>
        /// 32
        /// <param name="strEntName">entity name</param>
        /// <param name="strImageFileName">image file path</param>
        /// <param name="nBmpAttrib">bmp param</param>
        /// <param name="nScanAttrib">scan param</param>
        /// <param name="dBrightness">brightness[-1,1]</param>
        /// <param name="dContrast">contrast[-1,1]</param>
        /// <param name="dPointTime">point time</param>
        /// <param name="nImportDpi">DPI</param>
        /// <param name="bDisableMarkLowGrayPt">if enable mark low gray point</param>
        /// <param name="nMinLowGrayPt">low gray poitn value</param>
        /// </summary>  
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetBitmapEntParam2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode SetBitmapEntParam(string strEntName,
                                                             string strImageFileName,
                                                             int nBmpAttrib,
                                                             int nScanAttrib,
                                                             double dBrightness,
                                                             double dContrast,
                                                             double dPointTime,
                                                             int nImportDpi,
                                                             bool bDisableMarkLowGrayPt,
                                                             int nMinLowGrayPt
                                                              );
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetBitmapEntParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode GetBitmapEntParam3(string strEntName,
                                                       ref double dDpiX,
                                                       ref double dDpiY,
                                                      byte[] bGrayScaleBuf);

        [DllImport("MarkEzd", EntryPoint = "lmc1_SetBitmapEntParam3", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode SetBitmapEntParam3(string strEntName,
                                                         double dDpiX,
                                                         double dDpiY,
                                                         [MarshalAs(UnmanagedType.LPArray)] byte[] bGrayScaleBuf);

        /// <summary>
        /// 33
        /// Get the specail index font name and attribute
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetFontRecord", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode GetFontRecordByIndex(int fontIndex, StringBuilder fontName, ref uint fontAttrib);


        public static string GetEntityNameByIndex(int nEntityIndex)
        {
            StringBuilder str = new StringBuilder("", 255);
            Lmc1_GetEntityNameByIndex(nEntityIndex, str);
            return str.ToString();
        }

        /// <summary>
        /// 34
        /// Get current database preview picture
        /// </summary>  
        [DllImport("MarkEzd", EntryPoint = "lmc1_GetPrevBitmap2", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurPrevBitmap(int bmpwidth, int bmpheight);


        public static Image GetCurPreviewImage(int bmpwidth, int bmpheight, ref StatusClass ReturnStatus)
        {
            try
            {
                IntPtr pBmp = GetCurPrevBitmap(bmpwidth, bmpheight);
                Image img = Image.FromHbitmap(pBmp);
                return img;
            }
            catch (Exception ex)
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_ImageGenerationFail+'\n'+ ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 35
        /// Stop MARK
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_StopMark", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Stop();


        /// <summary>
        /// 36
        /// Change the scale of entity in the library
        /// dCenx,dCeny are the center coordinate
        /// dScaleX The scale of x direction
        /// dScaleY The scale of y direction
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_ScaleEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode ScaleEnt(string strEntName, double dCenx, double dCeny, double dScaleX, double dScaleY);

        /// <summary>
        /// 37
        /// Split all the entity in the library by speical rectangle size. 
        /// (x1,y1) is the split box's left down corner coordinate.(x2,y2) is the right up corner coordiante of the split rectangle. 
        /// </summary> 
        public struct SplitBox
        {
            public double x1;
            public double y1;
            public double x2;
            public double y2;
            public void Build(double dx1, double dy1, double dx2, double dy2)
            {
                x1 = dx1; x2 = dx2; y1 = dy1; y2 = dy2;
            }
        }
        [DllImport("MarkEzd", EntryPoint = "lmc1_SplitAllCurveEntByBox", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SplitAllCurveEntByBox(SplitBox[] pBoxs, int nBox, double dBoxErr);


        /// <summary>
        /// get the current bitmap attribute
        ///  </summary>
        /// <param name="strEntName">the name of the bitmap </param>
        /// <param name="strImageFileName">the path of the bitmap</param>
        /// <param name="nBmpAttrib">bitmap attribute</param>
        /// <param name="nScanAttrib">scan attribute</param>
        /// <param name="dBrightness">set brightness[-1,1] </param>
        /// <param name="dContrast">set the contrast [-1,1] </param>
        /// <param name="dPointTime">set the marking point time </param>
        /// <param name="nImportDpi">DPI</param>
        /// <returns></returns>


        [DllImport("MarkEzd", EntryPoint = "lmc1_GetBitmapEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode GetBmpEntParam(string strEntName,
                                                       StringBuilder BMPname,
                                                       ref int nBmpAttrib,
                                                       ref int nScanAttrib,
                                                       ref double dBrightness,
                                                       ref double dContrast,
                                                       ref double dPointTime,
                                                       ref int nImportDpi
                                                       );

        /// <summary>
        /// set the bitmap entity attribute
        /// </summary>
        /// <param name="strEntName">the name of the bitmap </param>
        /// <param name="strImageFileName">the path of the bitmap </param>
        /// <param name="nBmpAttrib"> bitmap attribute</param>
        /// <param name="nScanAttrib">scan attribute</param>
        /// <param name="dBrightness">set brightness[-1,1] </param>
        /// <param name="dContrast">set contrast [-1,1] </param>
        /// <param name="dPointTime">set the marking time </param>
        /// <param name="nImportDpi">DPI</param>
        /// <returns></returns>
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetBitmapEntParam", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode SetBmpEntParam(string strEntName,
                                                       string strImageFileName,
                                                       int nBmpAttrib,
                                                       int nScanAttrib,
                                                       double dBrightness,
                                                       double dContrast,
                                                       double dPointTime,
                                                       int nImportDpi
                                                       );

        /// <summary>
        /// 38
        /// hatch special entity
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_HatchEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int HatchEnt(string EntnName, string HatchEntName);

        /// <summary>
        /// delete entity
        /// </summary> 
        [DllImport("MarkEzd", EntryPoint = "lmc1_DeleteEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode DeleteEnt(string strEntName);

        ///<summary>
        ///copy and rename ent
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_CopyEnt", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern LmcErrCode CopyEnt(string strEntName, string strNewEntName);

        ///<summary>
        ///set ent pen
        ///<summary>
        [DllImport("MarkEzd", EntryPoint = "lmc1_SetPenNumberName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetPenNumberName(int nPenNo, string strEntName);
    }
}